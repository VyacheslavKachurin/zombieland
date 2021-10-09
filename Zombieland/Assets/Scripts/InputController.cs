using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InputController : MonoBehaviour
{
    public event Action<Vector3> OnMouseMoved;
    public event Action<float, float> OnAxisMoved;

    private float _horizontal;
    private float _vertical;
    private Camera _camera;
    private Vector3 _destination;
    
    private void Start()
    {
        _camera = Camera.main;
    }
    private void Update()
    {   
            ReadKeyBoardInput();
            ReadMouseInput();
    }

    private void ReadKeyBoardInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        OnAxisMoved?.Invoke(_horizontal, _vertical);
    }
    private void ReadMouseInput()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
        {
            _destination = hitInfo.point;
            _destination.y = transform.position.y;

            OnMouseMoved?.Invoke(_destination);
        }

       
    }
 
}
