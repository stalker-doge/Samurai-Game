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
        bodyParts = gameObject.GetComponentInChildren<EnemyBody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(totalHealth <=0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void DamagePart(int damage)
    {
        totalHealth -= damage;
    }
}
