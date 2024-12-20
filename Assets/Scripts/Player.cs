using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class Player : MonoBehaviour
{
    //Input
    private InputActionAsset playerInputActions;
    private InputActionMap playerInputMap;
    private InputAction movement;
    private InputAction cameraInput;
    private InputAction attack;
    private InputAction interact;
    private CharacterController controller;



    //movement
    [SerializeField] float maxSpeed = 2.0f;
    [SerializeField] float jumpForce = 10f;
    //[SerializeField] Rigidbody rb;
    private bool isGrounded;
    public float gravity = -9.81f;


    //camera
    [SerializeField] Camera cam;
    [SerializeField] float lookSensitivity = 1.0f;
    private float xRotation = 0f;

    //attack
    Vector3 attackDirection = Vector3.zero;
    [SerializeField] LineRenderer lineRenderer;
    //the weapon the player is currently holding
    WeaponType currentWeapon;
    GameObject equippedWeapon;
    Transform weaponLocation;
    Vector3 weaponLocationOriginal;

    Vector2 cameraDirection = Vector2.zero;
    Vector3 movementDirection = Vector3.zero;

    int health = 100;


    // Start is called before the first frame update
    void Start()
    {
    }
    private void Awake()
    {
        playerInputActions = this.GetComponent<PlayerInput>().actions;
        playerInputMap = playerInputActions.FindActionMap("Player");
        //rb = this.GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        weaponLocation= GetComponent<Transform>();
        currentWeapon = GetComponentInChildren<WeaponType>();
        weaponLocation = gameObject.transform.GetChild(0).GetChild(0);
        //saves the original weapon location in local space
        weaponLocationOriginal = weaponLocation.localPosition;
    }

    private void Update()
    {
        Look();
        Move();
    }
    private void OnEnable()
    {
        movement = playerInputMap.FindAction("Move");
        movement.Enable();
        cameraInput = playerInputMap.FindAction("Mouse");
        cameraInput.Enable();
        attack = playerInputMap.FindAction("Attack");
        attack.Enable();

        interact= playerInputMap.FindAction("Interact");
        interact.Enable();
        //when the player first presses the attack button, the player will start the attack method, and when the player releases the attack button, the player will start a different method
        playerInputMap.FindAction("Attack").started += context => Attack();
        playerInputMap.FindAction("Attack").canceled += context => StopAttack();
        playerInputMap.FindAction("Jump").started += Jump;
        playerInputMap.FindAction("Interact").started += context => Interact();
        //playerInputMap.FindAction("Menu").started += QuitGame;
        playerInputMap.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        movement.Disable();
        cameraInput.Disable();
        //playerInputMap.FindAction("Jump").started -= Jump;
        //playerInputMap.FindAction("Menu").started -= QuitGame;
        playerInputMap.Disable();
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (controller.isGrounded)
        {
            movementDirection += Vector3.up * jumpForce;
        }
    }

    void Look()
    {
        Vector2 looking = GetPlayerLook();
        float lookX = looking.x * lookSensitivity * Time.deltaTime;
        float lookY = looking.y * lookSensitivity * Time.deltaTime;
        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);


        transform.Rotate(Vector3.up * lookX);
    }

    void Move()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && movementDirection.y < 0)
        {
            movementDirection.y = -2f;
        }

        Vector2 movement = GetPlayerMovement();
        Vector3 move = transform.right * movement.x + transform.forward * movement.y;
        controller.Move(move * maxSpeed * Time.deltaTime);

        movementDirection.y += gravity * Time.deltaTime;
        controller.Move(movementDirection * Time.deltaTime);
    }

    public Vector2 GetPlayerLook()
    {
        return cameraInput.ReadValue<Vector2>();
    }

    public Vector2 GetPlayerMovement()
    {
        return movement.ReadValue<Vector2>();
    }

    void Attack()
    {
        //moves the weapon location slightly forward to simulate an attack, converting from world space to local space
        weaponLocation.position = weaponLocation.position + weaponLocation.forward * 0.5f;
    }

    void StopAttack()
    {
        //moves the weapon location back to its original position, converting from world space to local space
        weaponLocation.localPosition = weaponLocationOriginal;
    }

    void Interact()
    {
        //changes the weapon type to what the player is looking at
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Weapon")
            {
                currentWeapon = hit.collider.gameObject.GetComponent<WeaponType>();
                equippedWeapon = hit.collider.gameObject;
                Debug.Log("Weapon type: " + currentWeapon.weaponType);
            }
        }
    }

    public void Hurt(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player has died :(");
    }
}
