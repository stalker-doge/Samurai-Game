using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    float timeBeforeSleep;
    GameObject[] waypoints;
    int _currentWaypointIndex = 0;
    float moveSpeed = 0.1f;

    protected override void OnEnter()
    {
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        timeBeforeSleep = 10;
    }

    protected override void OnUpdate()
    {

        if (waypoints != null)
        {


            Transform wp = waypoints[_currentWaypointIndex].transform;
            if (Vector3.Distance(sc.transform.position, wp.position) < 1f)
            {
                _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
            }
            else
            {
                sc.GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(sc.transform.position, wp.position, moveSpeed));
            }
        }
        RaycastHit hit;
        if (Physics.Raycast(sc.transform.position, sc.transform.TransformDirection(Vector3.forward), out hit))
        {
            if(hit.collider.tag=="Player")
            {
                Debug.Log("Player Spotted!");
                sc.ChangeState(sc.chaseState);
            }
        }

        if (timeBeforeSleep < 0)
        {
            sc.ChangeState(sc.sleepState);
        }

        timeBeforeSleep -= Time.deltaTime;
    }

    public void OnExit(StateController sc)
    {

    }
}
