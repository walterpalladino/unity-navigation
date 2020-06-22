using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private LayerMask _layerMask;   //  Used to filter which are the walkable areas


    private Animator _animator;
    private NavMeshAgent _navMeshAgent;

    private float _maxDistance = 100.0f;

    private bool _isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_navMeshAgent.enabled)
        {

            _isMoving = CheckIfMoving();

            if (!_isMoving)
            {

//                _animator.SetFloat("Speed", 0.0f);
                _navMeshAgent.isStopped = true;

                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hitInfo;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    bool hit = Physics.Raycast(ray, out hitInfo, _maxDistance, _layerMask);

                    //  If we hit something
                    if (hit)
                    {
                        Debug.Log("Hit : " + hitInfo.transform.gameObject.name);
                        //  Move the character to the destination
                        Vector3 newPosition = hitInfo.point;
                        newPosition.y = transform.position.y;   //  Keep the same height

                        Debug.Log("Original newPosition : " + newPosition);

                        //  Check if the destination is reachable on this turn and limit it if its needed
                        //                  newPosition = PathUtils.CheckEndPosition(_navMeshAgent, newPosition, _characterProfile.movementWalk);
                        newPosition = PathUtils.CheckEndPosition(_navMeshAgent, newPosition, 100);

                        //                Debug.Log("Updated newPosition : " + newPosition);

                        //  Validate if the destination is reachable
                        NavMeshPath path = new NavMeshPath();
                        bool canGetPath = _navMeshAgent.CalculatePath(newPosition, path);
                        Debug.Log("Can Get Path : " + canGetPath);
                        Debug.Log(" Path Status : " + path.status);

                        if (path.status == NavMeshPathStatus.PathComplete)
                        {
                            //_animator.SetFloat("Speed", _navMeshAgent.speed);

                            _navMeshAgent.updateRotation = true;
                            _navMeshAgent.isStopped = false;
                            _navMeshAgent.SetDestination(newPosition);

    //                        _positionMarker.DisplayMarker(newPosition);
                            _isMoving = true;

  //                          _gameTurnManager.Next();
                        }


                    }

                }

                if (!_isMoving)
                {
//                    _positionMarker.HideMarker();
                }

            }
        }

    }


    //  Check if the model is moving on the NavMesh
    private bool CheckIfMoving()
    {

        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            return false;
        }
        else
        {
            return true;
        }

    }


}
