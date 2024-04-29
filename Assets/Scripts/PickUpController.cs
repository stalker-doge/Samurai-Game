using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{

    public WeaponType weaponScript;
    public Rigidbody rb;
    public CapsuleCollider coll;
    public Transform player, weaponContainer, fpsCam;


    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;


    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        transform.SetParent(weaponContainer);
        transform.localPosition = Vector3.zero;
        transform.localScale = new Vector3(0.2f,0.5f,0.2f);
        transform.localRotation = Quaternion.Euler(90f,0f,0f);
        rb.isKinematic = true;
        coll.isTrigger = true;
        weaponScript.enabled = true;
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        transform.SetParent(null);
        rb.isKinematic = false;
        coll.isTrigger = false;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random)); 

        weaponScript.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();

        if (equipped) transform.localPosition = Vector3.zero;
        if (equipped) transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
    }

    private void Start()
    {
        coll = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        weaponScript = GetComponent<WeaponType>();
        if(!equipped) 
        {
            weaponScript.enabled=false;
            rb.isKinematic=false;
            coll.isTrigger=false;
        }
        if(equipped)
        {
            weaponScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
            PickUp();

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            Debug.Log("Hit Enemy");
            collision.collider.gameObject.GetComponent<EnemyBody>().DamagePart(this.GetComponent<WeaponType>().damage);
        }
    }
}
