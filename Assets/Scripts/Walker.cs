using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using WPEditor;

public class Walker : MonoBehaviour
{
    WaypointSystem waypointSystem;

    public bool shouldPatroll = true;

    bool patrollOnce = false;
    bool agentMoving = false;

    NavMeshAgent agent;
    [Space]
    [SerializeField]
    float agentSpeed = 0f;

    [SerializeField]
    float patroleDelay = 0f;
    [Space]

    [SerializeField]
    int indexer = -1;

    [SerializeField]
    float tTime = 0;


    private void Start()
    {
        waypointSystem = GameObject.FindGameObjectWithTag("WaypointManger").GetComponent<WaypointSystem>();

        agent = GetComponent<NavMeshAgent>();
        //agent.SetDestination(waypointData.wayPoints[0].transform.position);

        indexer = 0;
        agent.speed = agentSpeed;
    }

    private void FixedUpdate()
    {
        tTime += Time.deltaTime;

        if (agent.destination != null)
        {
            if (!agentMoving & tTime >= patroleDelay & !patrollOnce)
            {
                //Debug.Log("[AGENT]:agent indexer position = " + indexer);
                //Debug.Log("[AGENT]: Moveing to target");
                agent.SetDestination(waypointSystem.points[indexer].position);
                agentMoving = true;
            }
            else if (agent.remainingDistance < 5f & agentMoving)
            {
                //Debug.Log("[AGENT]: Stoped");
                tTime = 0;
                indexer++;
                agentMoving = false;
            }
            else if (!agentMoving & indexer >= waypointSystem.points.Length)
            {
                //Debug.Log("I got to the last point");
                indexer = 0;
                agentMoving = false;

                if (!shouldPatroll)
                {
                    patrollOnce = true;
                }
            }
        }
    }
}
