using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPathway : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed = 2.0f;
    private int currentWaypointIndex = 0;

    void Start()
    {
        if (waypoints.Length > 0)
        {
            StartCoroutine(MoveToWaypoints());
        }
    }

    private IEnumerator MoveToWaypoints()
    {
        while (true)
        {
            Vector3 targetPosition = waypoints[currentWaypointIndex].position;

            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }

            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}