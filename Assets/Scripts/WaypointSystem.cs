using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointSystem : MonoBehaviour
{
    [Header("Waypoints")]
    public GameObject[] wayPoints;
    [Space]
    float tTime = 0;
    
    [Header("Debug")]
    [SerializeField]
    bool drawnDebugGizmos = true;

    private void OnDrawGizmos()
    {
        if (drawnDebugGizmos)
        {
            tTime = Time.deltaTime;
            if (wayPoints != null)
            {
                for (int i = 0; i < wayPoints.Length - 1; i++)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(wayPoints[i].transform.position, wayPoints[i + 1].transform.position);
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawSphere(wayPoints[i].transform.position, 1f);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(wayPoints[wayPoints.Length - 1].transform.position, wayPoints[0].transform.position);

                }
            }
        }
    }
}
