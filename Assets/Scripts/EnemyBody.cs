using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    [SerializeField] int totalHealth = 10;
    [SerializeField] EnemyBody bodyParts;

    // Start is called before the first frame update
    void Start()
    {
        //gets all the body parts attached to the enemy as children
        bodyParts = GetComponentInChildren<EnemyBody>();

    }

    // Update is called once per frame
    void Update()
    {
        if(totalHealth <=0)
        {
            //checks if the body part is the main body part
            if (bodyParts == null)
            {
                //if it is the main body part, the enemy is destroyed
                Destroy(this.gameObject);
            }
            else
            {
                //detach the body parts
                bodyParts.Detach();
            }
        }
    }

    public void DamagePart(int damage)
    {
        totalHealth -= damage;
    }

    public void Detach()
    {
        //detach the body parts
        bodyParts.transform.parent = null;
    }
}
