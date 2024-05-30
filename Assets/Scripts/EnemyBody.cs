using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    [SerializeField] int totalHealth = 10;
    [SerializeField] bool isCriticalPart = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (totalHealth <= 0)
        {
            //if the part has a parent, detach it
            if (transform.parent != null)
            {
                transform.parent = null;
                //remove constraints on the rigidbody
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
            //doesn't destroy the object immediately, so that the player can see the part fall off
            Destroy(gameObject, 10.0f);
        }
    }

    public void DamagePart(int damage)
    {
        //if the part is critical, it takes double damage
        if (isCriticalPart)
        {
            damage *= 2;
            //checks if the part is still alive
            if (totalHealth <= 0)
            {
                return;
            }
            //triggers a hitstop from the scene
            FindAnyObjectByType<HitStop>().Stop(0.1f);
            //changes material color to white
            GetComponent<Renderer>().material.color = Color.white;

        }
        totalHealth -= damage;
    }

    public void Detach()
    {
        transform.parent = null;
    }
}
