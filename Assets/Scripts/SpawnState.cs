using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnState : State
{
    //cooldown for spawning enemies
    float spawnCooldown = 5.0f;
    //radius to spawn enemies in
    float spawnRadius = 10.0f;
    //enemy to spawn
    GameObject enemy;

    //State for the spawn manager to use when spawning enemies

    protected override void OnEnter()
    {
        //updates enemy to the enemy prefab
        enemy = Resources.Load<GameObject>("Enemy");
        Debug.Log("Will spawn!");
    }

    protected override void OnUpdate()
    {

        if (spawnCooldown <= 0)
        {
            //spawn an enemy in a random location around the player
            Vector3 spawnPos = new Vector3(sc.transform.position.x + Random.Range(-spawnRadius, spawnRadius), 2, sc.transform.position.z + Random.Range(-spawnRadius, spawnRadius));
            GameObject.Instantiate(enemy, spawnPos, Quaternion.identity);
            spawnCooldown = 5.0f;
        }
        else
        {
            spawnCooldown -= Time.deltaTime;
        }
    }

    protected override void OnHurt()
    {
        // Transition to Hurt State
    }
    protected override void OnExit()
    {
        // "Must've been the wind"
    }
}
