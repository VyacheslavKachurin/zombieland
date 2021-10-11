using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InputController : MonoBehaviour
{
    public event Action<Vector3> OnMouseMoved;
    public event Action<float, float> OnAxisMoved;
    public event Action<bool> OnShootingInput;
    public event Action<bool> OnScrollWheelSwitched;

    private float _horizontal;
    private float _vertical;
    private Camera _camera;
    private Vector3 _destination;
    private bool _isShooting;
    private void Start()
    {
        _camera = Camera.main;
    }
    private void Update()
    {
        ReadAxisInput();
        ReadMouseInput();
        ReadShootInput();
        SwitchWeaponInput();
    }

    private void ReadAxisInput()
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
    private void ReadShootInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _isShooting = true;
            OnShootingInput?.Invoke(_isShooting);         
        }
        if (Input.GetButtonUp("Fire1"))
        {
            _isShooting = false;
            OnShootingInput?.Invoke(_isShooting);
        } 
    }
    private void SwitchWeaponInput()
    {
       if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            OnScrollWheelSwitched?.Invoke(true);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            OnScrollWheelSwitched?.Invoke(false);
        }
    }
}
