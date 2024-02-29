using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    int currentWaypoint = 0;
    [SerializeField] float followSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) < .1f)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, followSpeed * Time.deltaTime);
    }
}
