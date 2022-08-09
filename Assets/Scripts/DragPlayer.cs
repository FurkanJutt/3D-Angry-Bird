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

    private SpringJoint _playerSpringJoint;
    private Rigidbody _playerRigidbody;

    private void Awake()
    {
        _playerSpringJoint = _player.GetComponent<SpringJoint>();
        _playerRigidbody = _player.GetComponent<Rigidbody>();

        _releaseDelay = 1 / 4;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (Physics.Raycast(mousePosition, out RaycastHit raycastHit))
        {
            Debug.Log("MousePos : " + mousePosition);
            _player.transform.position = raycastHit.point;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Mouse Pressed");
        _isPressed = true;
        _playerRigidbody.useGravity = false;
        _player.GetComponent<SphereCollider>().enabled = false;
    }

    private void OnMouseUp()
    {
        Debug.Log("Mouse Released");
        _player.GetComponent<SphereCollider>().enabled = true;
        _playerRigidbody.useGravity = true;
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
