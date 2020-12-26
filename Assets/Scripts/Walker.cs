using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walker : MonoBehaviour
{
    [SerializeField]
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

    [Header("Debug")]
    [SerializeField]
    Transform currentTargetPos;

    [SerializeField]
    int indexer = 0;

    [SerializeField]
    float tTime = 0;


    private void Start()
    {
        waypointSystem = GameObject.FindGameObjectWithTag("WaypointManger").GetComponent<WaypointSystem>();

        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypointSystem.wayPoints[0].transform.position);

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
                if (indexer <= waypointSystem.wayPoints.Length - 1)
                {
                    Debug.Log(currentTargetPos.ToString());
                    Debug.Log("Agent is moveing to target");
                    agent.SetDestination(waypointSystem.wayPoints[indexer].transform.position);
                    agentMoving = true;
                    currentTargetPos = waypointSystem.wayPoints[indexer].transform;
                }
                else
                {
                    indexer = 0;
                    agentMoving = false;
                    if (!shouldPatroll)
                    {
                        patrollOnce = true;
                    }
                }
            }
            else if (agent.remainingDistance < 2f & agentMoving == true)
            {
                Debug.Log("agent stoped");
                indexer++;
                tTime = 0;
                agentMoving = false;
            }
        }
    }
}
