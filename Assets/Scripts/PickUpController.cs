using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{

    public WeaponType weaponScript;
    public Rigidbody rb;
    public BoxCollider coll;
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
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
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
    }

    private void Start()
    {
        if(!equipped) 
        {
            weaponScript.enabled=false;
            rb.isKinematic=false;
            coll.isTrigger=false;
        }
    }
}
