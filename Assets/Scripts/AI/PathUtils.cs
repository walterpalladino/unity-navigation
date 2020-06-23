using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PathUtils {


    public static Vector3 CheckEndPosition (Vector3 startPosition, Vector3 testPosition, LayerMask layerMask, float maxDistance) {

        float distance = 0;
        Vector3 endPosition = Vector3.zero;

        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(startPosition, testPosition, layerMask, path))
        {
            endPosition = testPosition;
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                float segmentDistance = (path.corners[i + 1] - path.corners[i]).magnitude;
                if (distance + segmentDistance <= maxDistance)
                {
                    distance += segmentDistance;
                }
                else // Path length exceeds maxDistance
                {
                    endPosition = path.corners[i] + ((path.corners[i + 1] - path.corners[i]).normalized * (maxDistance - distance));
                    break;
                }
            }

        } else {
            Debug.Log("No Path found");
        }

        return endPosition;
    }

    public static Vector3 CheckEndPosition(NavMeshAgent agent, Vector3 testPosition, float maxDistance)
    {

        float distance = 0;
        Vector3 endPosition = Vector3.zero;

        NavMeshPath path = new NavMeshPath();
        if (agent.CalculatePath(testPosition, path))
        {
            endPosition = testPosition;
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                float segmentDistance = (path.corners[i + 1] - path.corners[i]).magnitude;
                if (distance + segmentDistance <= maxDistance)
                {
                    distance += segmentDistance;
                }
                else // Path length exceeds maxDistance
                {
                    endPosition = path.corners[i] + ((path.corners[i + 1] - path.corners[i]).normalized * (maxDistance - distance));
                    break;
                }
            }

        }
        else
        {
            Debug.Log("No Path found");
        }

        return endPosition;
    }

    public static float CalculateDistance (Vector3 startPosition, Vector3 endPosition, LayerMask layerMask)
    {
        Debug.Log("Crow Distance: " + Vector3.Distance(startPosition, endPosition));
        float distance = -1;

        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(startPosition, endPosition, layerMask, path)) {
            distance = 0;
            Debug.Log(path.corners.Length);
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                float segmentDistance = (path.corners[i + 1] - path.corners[i]).magnitude;
                distance += segmentDistance;
            }

        }

        return distance;
    }

    public static float CalculateDistance(NavMeshAgent agent, Vector3 endPosition)
    {
        float distance = -1;

        NavMeshPath path = new NavMeshPath();
        if (agent.CalculatePath( endPosition, path))
        {
            distance = 0;
            Debug.Log(path.corners.Length);
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                float segmentDistance = (path.corners[i + 1] - path.corners[i]).magnitude;
                distance += segmentDistance;
            }

        }

        return distance;
    }


    /*
    public static bool CheckOnMesLink(NavMeshAgent agent, out NavMeshLink link, out OffMeshLinkData data)
    {

        link = null;
        data = null;

        if (agent.isOnOffMeshLink)
        {
            link = agent.navMeshOwner as NavMeshLink;
            if (link != null)
            {
                return true;
            }
            data = agent.currentOffMeshLinkData;
            return true;
        }
        else
        {
            return false;
        }
    }
    */
}
