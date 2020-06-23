using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum OffMeshLinkMoveMethod
{
    Teleport,
    NormalSpeed,
    Parabola,
    Curve
}

[RequireComponent(typeof(NavMeshAgent))]
public class AgentLinkMover : MonoBehaviour
{

    private NavMeshAgent _navMeshAgent;


    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private bool isOnLink = false;

    // Update is called once per frame
    void Update()
    {
        if (_navMeshAgent.isOnOffMeshLink)
        {
            if (!isOnLink)
            {
                isOnLink = true;

                if (method == OffMeshLinkMoveMethod.NormalSpeed)
                {
                    StartCoroutine(NormalSpeed(_navMeshAgent));
                }
                else if (method == OffMeshLinkMoveMethod.Parabola)
                {
                    StartCoroutine(Parabola(_navMeshAgent, 2.0f, 0.5f));
                }
                else if (method == OffMeshLinkMoveMethod.Curve)
                {
                    StartCoroutine(Curve(_navMeshAgent, 0.5f));
                }
            }
        }
        else
        {
            isOnLink = false;
        }
    }

    //  Check: https://forum.unity.com/threads/how-to-trigger-a-jump-on-an-offmesh-link.313628/
    public OffMeshLinkMoveMethod method = OffMeshLinkMoveMethod.Parabola;
    public AnimationCurve curve = new AnimationCurve();
    IEnumerator StartMovement()
    {
        _navMeshAgent.autoTraverseOffMeshLink = false;
        while (true)
        {
            if (_navMeshAgent.isOnOffMeshLink)
            {
                if (method == OffMeshLinkMoveMethod.NormalSpeed)
                    yield return StartCoroutine(NormalSpeed(_navMeshAgent));
                else if (method == OffMeshLinkMoveMethod.Parabola)
                    yield return StartCoroutine(Parabola(_navMeshAgent, 2.0f, 0.5f));
                else if (method == OffMeshLinkMoveMethod.Curve)
                    yield return StartCoroutine(Curve(_navMeshAgent, 0.5f));
                _navMeshAgent.CompleteOffMeshLink();
            }
            yield return null;
        }
    }
    IEnumerator NormalSpeed(NavMeshAgent agent)
    {
        Debug.Log("NormalSpeed");
        agent.autoTraverseOffMeshLink = false;
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
        while (agent.transform.position != endPos)
        {
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);
            yield return null;
        }

        agent.CompleteOffMeshLink();
        Debug.Log("NormalSpeed Completed");
    }
    IEnumerator Parabola(NavMeshAgent agent, float height, float duration)
    {
        Debug.Log("Parabola");
        agent.autoTraverseOffMeshLink = false;

        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
        float normalizedTime = 0.0f;
        while (normalizedTime < 1.0f)
        {
            float yOffset = height * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
            agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }

        agent.CompleteOffMeshLink();
        Debug.Log("Parabola Completed");
    }

    IEnumerator Curve(NavMeshAgent agent, float duration)
    {
        Debug.Log("Curve");
        agent.autoTraverseOffMeshLink = false;

        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
        float normalizedTime = 0.0f;
        while (normalizedTime < 1.0f)
        {
            float yOffset = curve.Evaluate(normalizedTime);
            agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }

        agent.CompleteOffMeshLink();
        Debug.Log("Curve Completed");
    }

}
