using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _isPressed = false;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (_isPressed)
        //{
        //    _DragPlayer();
        //}
    }

    private void _DragPlayer()
    {
        Ray mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mousePosition, out RaycastHit raycastHit))
        {
            Debug.Log("MousePos : " + mousePosition);
            _rigidbody.position = raycastHit.point;
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
    }
}
