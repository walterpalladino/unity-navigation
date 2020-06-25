using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class PathMonitor : MonoBehaviour
{

    public bool isOnLink = false;
    public NavMeshLink link;
    public OffMeshLinkData data;

    public bool hasPathPending = false;
    public bool destinationReached = false;



    private NavMeshAgent _navMeshAgent;


    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_navMeshAgent.isOnOffMeshLink)
        {
            isOnLink = true;
            link = _navMeshAgent.navMeshOwner as NavMeshLink;
            /*
            link = agent.navMeshOwner as NavMeshLink;
            if (link != null)
            {
                return true;
            }
            return true;
            */
            data = _navMeshAgent.currentOffMeshLinkData;
        }
        else
        {
            isOnLink = false;
            link = null;
        }
        hasPathPending = _navMeshAgent.pathPending;
        destinationReached = PathUtils.DestinationReached(_navMeshAgent, transform.position);
    }
}