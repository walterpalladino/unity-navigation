using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowAny : MonoBehaviour
{

    public float _followHeight = 4f;
    public float _followDistance = 5f;
    public float _smoothSpeed = 0.9f;
    public float _smoothChangeCharacterSpeed = 3f;
    public float _verticalOffset = 2.0f;

    [SerializeField]
    private Transform _actualCharacter;           // Reference to the player's transform.
    public Transform actualCharacter {
        set {  
            if (_actualCharacter) _targetChanged = true;
            _actualCharacter = value;
        }
    }

    [SerializeField]
    private float maxDistanceToTrack = 0.01f;
    private bool _targetChanged = false;
    private bool _cameraPositionUpdating = false;


    public bool IsMoving {
        get { return _cameraPositionUpdating; }
    }


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //  Will update the position when there is a character to track and there is no actual movement
        if (_actualCharacter && !_cameraPositionUpdating) {
            TrackActualCharacter();
        }

    }

    private void TrackActualCharacter()
    {
        float targetHeight;
        float currentHeight;
        float currentRotation;

        targetHeight = _actualCharacter.position.y + _followHeight;

        currentRotation = transform.eulerAngles.y;

        currentHeight = Mathf.Lerp(transform.position.y, targetHeight, _smoothSpeed * Time.deltaTime);

        Quaternion euler = Quaternion.Euler(0f, currentRotation, 0f);

        Vector3 targetPosition = _actualCharacter.position - (euler * Vector3.forward) * _followDistance;

        targetPosition.y = currentHeight;

        if (_targetChanged) {
            StartCoroutine(MoveCamera(targetPosition));
        } else
        {
            transform.position = targetPosition;
            transform.LookAt(_actualCharacter.position + Vector3.up * _verticalOffset);
        }
    }

    IEnumerator MoveCamera (Vector3 endPosition)
    {
        _cameraPositionUpdating = true;
        while (Vector3.Distance(transform.position, endPosition) > maxDistanceToTrack)
        {
            transform.position = Vector3.Slerp(transform.position, endPosition, Time.deltaTime * _smoothChangeCharacterSpeed);
            yield return new WaitForEndOfFrame();
        }
        _targetChanged = false;
        _cameraPositionUpdating = false;
    }
}
