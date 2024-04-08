using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChaseState : State
{
    float timeBeforePatrol;
    float attackCooldown=2;
    float moveSpeed=0.1f;
    bool isChasing = true;
    float maxDistance = 10;
    GameObject player;
    protected override void OnEnter()
    {
        timeBeforePatrol = 10;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void OnUpdate()
    {
        ChasePlayer();
        RaycastHit hit;
        if (Physics.Raycast(sc.transform.position, sc.transform.TransformDirection(Vector3.forward), out hit))
        {
            if (hit.collider.tag == "Player"&& hit.distance<1)
            {
                if(attackCooldown<=0)
                {
                    Attack();
                    attackCooldown = 2;
                }
            }
        }
        else
        {
            timeBeforePatrol -= Time.deltaTime;
        }
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }

        if (timeBeforePatrol < 0)
        {
            sc.ChangeState(sc.sleepState);
        }

    }


    void Attack()
    {
        Debug.Log("HIT!");
    }

    void ChasePlayer()
    {
        if (isChasing)
        {
            sc.transform.LookAt(player.transform);
            sc.transform.position += sc.transform.forward * moveSpeed * Time.deltaTime;
        }
        if (Vector3.Distance(sc.transform.position, player.transform.position) >= maxDistance)
        {
            isChasing = false;
        }
    }
}
