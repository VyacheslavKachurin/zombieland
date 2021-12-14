using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Animations.Rigging;

public class Player : MonoBehaviour, IDamageable
{
    public event Action<bool> PlayerDied;
    public event Action<float> OnPlayerGotAttacked;
    public event Action<IWeapon> OnWeaponChanged;

    [SerializeField] private WeaponHolder _weaponHolder;
    [SerializeField] private Transform _helmetHolder;
    [SerializeField] private Transform _vestHolder;
    [SerializeField] private GameObject _aimingObject;

    [SerializeField] private Rig _aimingRig;
    [SerializeField] private Rig _holdWeaponRig;
    [SerializeField] private Rig _handsIK;

    [SerializeField] private Animator _rigController;

    public IPlayerInput Input
    {
        get { return _input; }
    }

    public InventoryModel InventoryModel
    {
        get => _inventoryModel;
        set { _inventoryModel = value; } // i dont remember the nice syntax;
    }

    private InventoryModel _inventoryModel;

    public float CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; OnPlayerGotAttacked(0); }
    }

    private float _horizontal;
    private float _vertical;
    private Vector3 _direction;
    private float _velocityZ;
    private float _velocityX;
    private Vector3 _mousePosition;

    private float _dampTime = 0.1f;
    private Animator _animator;
    private PlayerStats _playerStats;

    private float _currentHealth;
    private bool _isDead = false;
    private float _movementSpeed;

    private IWeapon _currentWeapon;
    private GameObject _currentHelmet;
    private GameObject _currentVest;

    private Camera _camera;

    private CharacterController _cc;
    private Rigidbody[] _ragdoll;

    private Vector3 _relatedDirection;

    private IPlayerInput _input;

    private enum PlayerState
    {
        Idle, Aiming, Moving, Dead, Jumping
    }

    private PlayerState _currentState;

    private bool _isWeaponHolstered = false;

    private void Start()
    {
        _camera = Camera.main;
        AssignStats();

        _cc = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _weaponHolder.OnWeaponChanged += GetWeapon;

        _ragdoll = GetComponentsInChildren<Rigidbody>();

        DeactivateRagdoll();

        SwitchState(PlayerState.Idle);
    }

    private void Update()
    {
        GetGrounded();
        Move();

        if (_currentState == PlayerState.Moving || _currentState == PlayerState.Jumping)
        {
            RotateTowardsMovement();
        }
        if (_currentState == PlayerState.Aiming)
        {
            LookTowardsMouse();
            ProcessAimingState();
        }




    }

    private void Move()
    {
        _direction = new Vector3(_horizontal, 0, _vertical);

        _relatedDirection = _camera.transform.TransformDirection(_direction);//change direction according to camera rotation
        _relatedDirection.y = 0;

        if (_currentState == PlayerState.Aiming)
            return;

        if (_relatedDirection.magnitude > 0)
        {
            SwitchState(PlayerState.Moving);

        }
        else
        {
            SwitchState(PlayerState.Idle);
        }

    }

    private void HolsterWeapon()
    {
        _isWeaponHolstered = !_isWeaponHolstered;
        _rigController.SetBool("Rifle_holster", _isWeaponHolstered);


    }

    private void RotateTowardsMovement()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_relatedDirection), Time.deltaTime * 10f);// make turn speed
    }

    private void SwitchState(PlayerState state)
    {
        if (_currentState == state)
        {
            return;
        }

        _currentState = state;
        switch (state)
        {
            case PlayerState.Idle:
                SetIdleState();
                return;

            case PlayerState.Moving:
                SetMovingState();
                return;

            case PlayerState.Aiming:
                SetAimingState();
                return;

            case PlayerState.Jumping:
                SetJumpingState();
                return;

            default:
                return;
        }
    }

    private void SetIdleState()
    {
        _currentState = PlayerState.Idle;
        _animator.ResetTrigger("isJogging");
        _animator.SetTrigger("isIdle");

    }

    private void SetMovingState()
    {
        _rigController.SetBool("Aim", false);
        _animator.ResetTrigger("isIdle");
        _animator.ResetTrigger("isAiming");
        _animator.SetTrigger("isJogging");
    }

    private void SetAimingState()
    {
        _currentState = PlayerState.Aiming;
        _animator.SetTrigger("isAiming");

        _rigController.SetBool("Aim", true);


    }

    private void SetJumpingState()
    {
        _animator.ResetTrigger("isSprinting");
        _animator.ResetTrigger("isJogging");
        _animator.SetTrigger("Evade");
    }

    private void ProcessAimingState()
    {

        _velocityZ = Vector3.Dot(_relatedDirection.normalized, transform.forward);
        _velocityX = Vector3.Dot(_relatedDirection.normalized, transform.right);

        _animator.SetFloat("VelocityZ", _velocityZ, _dampTime, Time.deltaTime);
        _animator.SetFloat("VelocityX", _velocityX, _dampTime, Time.deltaTime);

    }

    private void LookTowardsMouse()
    {
        Vector3 lookDirection = _mousePosition - transform.position;
        lookDirection.Normalize();

        lookDirection.y = transform.position.y;// avoid player tilting around/on X axis

        if (lookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damageAmount;

            OnPlayerGotAttacked(damageAmount);

            if (_currentHealth <= 0)
            {
                Die();
            }
            else
            {
                _animator.SetTrigger("GetHit");
            }
        }
    }

    private void Die()
    {
        if (!_isDead)
        {
            _isDead = true;

            _aimingRig.enabled = false;
            _holdWeaponRig.enabled = false;
            _handsIK.enabled = false;
            _rigController.enabled = false;
            _cc.enabled = false;

            ActivateRagdoll();

            PlayerDied?.Invoke(_isDead);
            Destroy(this);
        }
    }

    private void ReceiveAxis(float horizontal, float vertical)
    {
        _horizontal = horizontal;
        _vertical = vertical;
    }

    private void ReceiveMouse(Vector3 mousePosition)
    {
        //TODO: check if its only for moving head and nothing more. if for head tilting - delete

        _mousePosition = mousePosition;
        if (Time.timeScale != 0)
        {

            // _aimingObject.transform.position = _mousePosition;

            // try to set up aiming help system
        }
    }

    private void ReceiveShootingInput(bool isShooting)
    {
        if (_currentState == PlayerState.Aiming)
        {
            _currentWeapon.Shoot(isShooting);
        }
    }

    private void GetWeapon(IWeapon weapon)
    {
        _currentWeapon = weapon;
        OnWeaponChanged(PassWeapon());
        _currentWeapon.OnWeaponReload += ReloadAnimation;

    }

    private void ReloadAnimation(bool isReloading)
    {
        if (isReloading)
            _animator.SetTrigger("Reloading");
    }

    private void ReceiveReloadInput()
    {
        _currentWeapon.Reload();
    }

    public IWeapon PassWeapon()
    {
        return _currentWeapon;
    }

    private void AssignStats()
    {
        _currentHealth = _playerStats.MaxHealth.GetValue();
        _movementSpeed = _playerStats.Speed.GetValue();

        _playerStats.Speed.OnValueChanged += UpgradeSpeed;

    }

    public PlayerStats ReturnPlayerStats()
    {
        _playerStats = GetComponent<PlayerStats>(); //otherwise it gets called earlier than player assign the variable 
        return _playerStats;
    }

    private void UpgradeSpeed(float speed)
    {
        _movementSpeed = speed;
    }

    private void FinishJumping()
    {
        SwitchState(PlayerState.Moving);
    }

    public void Initialize(IPlayerInput input)
    {
        _input = input;
        input.Moved += ReceiveAxis;
        input.CursorMoved += ReceiveMouse;
        input.OnShootingInput += ReceiveShootingInput;
        input.OnReloadPressed += ReceiveReloadInput;
        input.SprintingSwitched += SetSprinting;
        input.JumpPressed += GetJumpInput;
        input.AimedWeapon += ReceiveAimingInput;
        input.HolsteredWeapon += HolsterWeapon;
    }

    private void GetGrounded()
    {
        if (!_cc.isGrounded)
        {
            _cc.Move(Physics.gravity);
        }

    }

    private void SetSprinting(bool value)
    {
        if (value)
        {
            SwitchState(PlayerState.Moving);

            _aimingRig.weight = 0;
            _animator.ResetTrigger("isJogging");
            _animator.SetTrigger("isSprinting");

        }

        else if (!value && _relatedDirection.magnitude > 0)
        {
            _animator.ResetTrigger("isAiming");
            _animator.ResetTrigger("isSprinting");
            _animator.SetTrigger("isJogging");
        }
        else
        {
            _animator.ResetTrigger("isSprinting");
            _animator.SetTrigger("isIdle");
        }
    }

    private void GetJumpInput()
    {
        SwitchState(PlayerState.Jumping);
    }

    private void ReceiveAimingInput(bool value)
    {

        if (value)
        {
            SwitchState(PlayerState.Aiming);
        }
        else
        {
            SwitchState(PlayerState.Moving);

        }
        //  _aimingRig.weight = Convert.ToInt32(value);
    }

    public void EquipWeapon(GameObject weapon)
    {
        _weaponHolder.PickUpWeapon(weapon);

        _rigController.Play("RifleIdle", 0);
    }

    private void DeactivateRagdoll()
    {
        foreach (var rb in _ragdoll)
        {
            rb.isKinematic = true;
        }
    }

    private void ActivateRagdoll()
    {
        foreach (var rb in _ragdoll)
        {
            rb.isKinematic = false;
        }
        _animator.enabled = false;
    }

    public void EquipHelmet(GameObject helmet)
    {
        var newHelmet = Instantiate(helmet);

        if (_currentHelmet != null)
        {
            Destroy(_currentHelmet);
        }
        _currentHelmet = newHelmet;

        newHelmet.transform.parent = _helmetHolder;
        newHelmet.transform.localPosition = Vector3.zero;
        newHelmet.transform.localRotation = Quaternion.Euler(-90, 0, 0);
    }

    public void EquipVest(GameObject vest)
    {
        var newVest = Instantiate(vest);

        if (_currentVest != null)
        {
            Destroy(_currentVest);
        }
        _currentVest = newVest;

        newVest.transform.parent = _vestHolder;
        newVest.transform.localPosition = Vector3.zero;
        newVest.transform.localRotation = Quaternion.Euler(-90, 0, 0);

    }
}
