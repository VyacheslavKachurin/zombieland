using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InputController : MonoBehaviour, IPlayerInput
{
    public event Action<Vector3> CursorMoved;
    public event Action<float, float> Moved;
    public event Action<bool> OnShootingInput;
    public event Action<bool> OnScrollWheelSwitched;
    public event Action OnReloadPressed;
    public event Action<bool> OnUpgradeButtonPressed;
    public event Action<bool> OnGamePaused;

    private float _horizontal;
    private float _vertical;
    private Camera _camera;
    private Vector3 _destination;
    private bool _isShooting;
    private bool _isReloading;

    private bool _isPaused = false;

    private bool _isUpgradeOn = false;
    private bool _wasPausePressed = false;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!_isPaused)
        {
            ReadAxisInput();
            ReadMouseInput();
            ReadShootInput();
            SwitchWeaponInput();
            ReloadInput();
        }

        UpgradeButtonInput();
        ReadPauseInput();
    }

    private void ReadPauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_isUpgradeOn)
        {
            _wasPausePressed = !_wasPausePressed;
            _isPaused = !_isPaused;
            OnGamePaused(_isPaused);
        }
    }

    private void ReadAxisInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        Moved?.Invoke(_horizontal, _vertical);
    }

    private void ReadMouseInput()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
        {
            _destination = hitInfo.point;
            //  _destination.y = transform.position.y; leave it for future           

            CursorMoved?.Invoke(_destination);
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
        if (_isReloading)
        {
            return;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            OnScrollWheelSwitched?.Invoke(true);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            OnScrollWheelSwitched?.Invoke(false);
        }
    }

    private void ReloadInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnReloadPressed();
        }
    }

    public void IsWeaponReloading(bool isReloading)
    {
        _isReloading = isReloading;
    }

    private void UpgradeButtonInput()
    {
        if (Input.GetKeyDown(KeyCode.U) && !_wasPausePressed)
        {
            _isPaused = !_isPaused;
            _isUpgradeOn = !_isUpgradeOn;
            OnUpgradeButtonPressed(_isPaused);


        }
    }

    public void Continue()
    {
        _isPaused = false;
        _wasPausePressed = false;

    }

}
