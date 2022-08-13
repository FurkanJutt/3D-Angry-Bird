using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPlayer : MonoBehaviour
{
    private bool _isPressed = false;
    private bool _mouseUp = false;

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _sling;
    [SerializeField] private float _releaseDelay;
    [SerializeField] private float _maxDragDistance = 2f;

    private Vector3[] positions = new Vector3[2];

    private SpringJoint _playerSpringJoint;
    private Rigidbody _playerRigidbody;
    private Rigidbody _slingRigidbody;
    private LineRenderer _playerLineRenderer;
    private TrailRenderer _playerTrailRenderer;

    private void Awake()
    {
        _playerSpringJoint = _player.GetComponent<SpringJoint>();
        _playerRigidbody = _player.GetComponent<Rigidbody>();
        _slingRigidbody = _playerSpringJoint.connectedBody;
        _playerLineRenderer = _player.GetComponent<LineRenderer>();
        _playerTrailRenderer = _player.GetComponent<TrailRenderer>();

        _releaseDelay = 1 / 4;
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerLineRenderer.enabled = false;
        _playerTrailRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPressed)
        {
            _DragPlayer();
        }
        else if(_mouseUp && _playerSpringJoint)
        {
            _ReleasePlayer();
        }
    }

    private void _DragPlayer()
    {
        Ray mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
        //float distance = Vector2.Distance(_playerRigidbody.position, _slingRigidbody.position);

        //Debug.Log("Distance : "+distance);
        if (Physics.Raycast(mousePosition, out RaycastHit raycastHit))
        {
            //if (distance > _maxDragDistance)
            //{
            //    //var currentPos = _slingRigidbody.position + Vector3.ClampMagnitude(_playerRigidbody.position - _slingRigidbody.position, _maxDragDistance);
            //    //direction = (_playerRigidbody.position - _slingRigidbody.position).normalized;
            //    //Debug.Log("Direction : " + direction);
            //    // _playerRigidbody.position = _slingRigidbody.position + direction * _maxDragDistance;
            //    //_player.transform.position = raycastHit.point;
            //    //_player.transform.position = currentPos;
            //}
            //else
            //{
            SetLinePositions();
            _player.transform.position = raycastHit.point;
            //}
        }
    }

    private void SetLinePositions()
    {
        positions[0] = _playerRigidbody.position;
        positions[1] = _slingRigidbody.position;
        _playerLineRenderer.SetPositions(positions);
    }

    private void OnMouseDown()
    {
        _isPressed = true;
        _playerRigidbody.useGravity = false;
        _playerLineRenderer.enabled = true;
        _player.GetComponent<SphereCollider>().enabled = false;
    }

    private void OnMouseUp()
    {
        _player.GetComponent<SphereCollider>().enabled = true;
        _playerRigidbody.useGravity = true;
        _playerLineRenderer.enabled = false;
        _playerTrailRenderer.enabled = true;
        _isPressed = false;
        _mouseUp = true;
    }

    private void _ReleasePlayer()
    {
        if (_playerSpringJoint.transform.position.x >= _sling.transform.position.x)
        {
            Destroy(_playerSpringJoint);
        }
    }
}
