using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPlayer : MonoBehaviour
{
    private bool _isPressed = false;

    [SerializeField] private GameObject _player;
    [SerializeField] private float _releaseDelay;

    private SpringJoint _playerSpringJoint;

    private void Awake()
    {
        _playerSpringJoint = _player.GetComponent<SpringJoint>();

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
    }

    private void OnMouseUp()
    {
        Debug.Log("Mouse Released");
        _isPressed = false;
        StartCoroutine(_ReleasePlayer());
    }

    private IEnumerator _ReleasePlayer()
    {
        yield return new WaitForSeconds(_releaseDelay);
    }
}
