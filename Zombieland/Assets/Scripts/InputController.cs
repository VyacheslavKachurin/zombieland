using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InputController : MonoBehaviour, IPlayerInput
{
    [SerializeField] private LayerMask _layerMask;
    public event Action<Vector3> CursorMoved;
    public event Action<float, float> Moved;
    public event Action<bool> OnShootingInput;
    public event Action OnReloadPressed;
    public event Action<bool> OnUpgradeButtonPressed;
    public event Action<bool> OnGamePaused;
    public event Action<bool> SprintingSwitched;
    public event Action JumpPressed;
    public event Action<bool> AimedWeapon;
    public event Action HolsteredWeapon;
    public event Action InteractButtonPressed;
    public event Action InventoryButtonPressed;

    private float _horizontal;
    private float _vertical;
    private Camera _camera;
    private Vector3 _destination;
    private bool _isShooting;
    private bool _isReloading;

    private bool _isPaused = false;


    private enum InputState { Playing, Paused, Upgrading, Inventory };

    private InputState _currentState;

    private void Start()
    {
        _camera = Camera.main;
        _currentState = InputState.Playing;
    }

    private void Update()
    {
        if (_currentState == InputState.Playing)
        {
            ReadSprintButton();
            ReadAxisInput();
            ReadCursorInput();
            ReadShootInput();
            ReloadInput();
            ReadJumpButton();
            ReadAimingInput();
            ReadHolsterButton();
            ReadInteractButton();

        }

        ReadInventoryButton();
        UpgradeButtonInput();
        ReadPauseInput();
    }

    private void ReadPauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_currentState == InputState.Playing || _currentState == InputState.Paused)
            {
                _isPaused = !_isPaused;

                _currentState = _currentState == InputState.Playing ? InputState.Paused : InputState.Playing;

                OnGamePaused(_isPaused);
            }
        }


    }

    private void ReadAxisInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        Moved?.Invoke(_horizontal, _vertical);
    }

    private void ReadCursorInput()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _layerMask))
        {
            _destination = hitInfo.point;
            //  _destination.y = transform.position.y; leave it for future           

            CursorMoved?.Invoke(_destination);
        }
    }
    private void ReadAimingInput()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            AimedWeapon(true);

        }
        if (Input.GetButtonUp("Fire2"))
        {
            AimedWeapon(false);
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
        if (Input.GetKeyDown(KeyCode.U)) //TODO : get rid of bools
        {
            if (_currentState == InputState.Playing || _currentState == InputState.Upgrading)
            {
                _isPaused = !_isPaused;

                _currentState = _currentState == InputState.Playing ? InputState.Upgrading : InputState.Playing;

                OnUpgradeButtonPressed(_isPaused);
            }
        }
    }

    public void Continue()
    {
        _isPaused = false;
        _currentState = InputState.Playing;
        OnGamePaused(_isPaused);

    }

    private void ReadSprintButton()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SprintingSwitched(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            SprintingSwitched(false);
        }
    }
    private void ReadJumpButton()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpPressed();
        }
    }

    private void ReadHolsterButton()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            HolsteredWeapon();
        }
    }

    private void ReadInteractButton()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            InteractButtonPressed?.Invoke();
        }
    }

    private void ReadInventoryButton()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (_currentState == InputState.Playing || _currentState == InputState.Inventory)
            {
              
                _currentState = _currentState == InputState.Playing ? InputState.Inventory : InputState.Playing;
                InventoryButtonPressed?.Invoke();
                
            }
        }
    }

}
