using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Animations.Rigging;

public class Player : MonoBehaviour, IDamageable
{

    public static Player instance;

    public event Action<bool> OnPlayerDeath;
    public event Action<float> OnPlayerGotAttacked;
    public event Action<IWeapon> OnWeaponChanged;

    [SerializeField] private WeaponHolder _weaponHolder;
    [SerializeField] private GameObject _aimingObject;
    [SerializeField] private Rig _aimingRig;   

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

    private Camera _camera;

    private CharacterController _cc;

    private enum PlayerState
    {
        Aiming, Walking, Dead, Jumping
    }

    private PlayerState _currentState = PlayerState.Walking;

    private void Start()
    {
       
     
        instance = this;
        _camera = Camera.main;
        AssignStats();

        _cc = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _weaponHolder.OnWeaponChanged += GetWeapon;

    }

    private void Update()
    {
        if (_currentState == PlayerState.Aiming)
        {
            AimTowardsMouse();       
        }

        GetGrounded();
        Move();


    }

    private void Move()
    {
        _direction = new Vector3(_horizontal, 0, _vertical);

        Vector3 relatedDirection = _camera.transform.TransformDirection(_direction);//change direction according to camera rotation
        relatedDirection.y = 0;

        //  transform.Translate(relatedDirection.normalized * _movementSpeed * Time.deltaTime, Space.World);

        _velocityZ = Vector3.Dot(relatedDirection.normalized, transform.forward);
        _velocityX = Vector3.Dot(relatedDirection.normalized, transform.right);

        _animator.SetFloat("VelocityZ", _velocityZ, _dampTime, Time.deltaTime);
        _animator.SetFloat("VelocityX", _velocityX, _dampTime, Time.deltaTime);

        if (_direction.magnitude != 0 && _currentState != PlayerState.Aiming)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(relatedDirection), Time.deltaTime * 10f);// make turn speed
        }
    }

    private void AimTowardsMouse()
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
            _animator.SetTrigger("Die");

            OnPlayerDeath?.Invoke(_isDead);
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

    private void ReceiveScroolWheelInput(bool input)
    {
        _weaponHolder.ChangeWeapon(input);
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

    private void SetWeaponAnimation()
    {

    }
    private void FinishJumping()
    {
        _currentState = PlayerState.Walking;
    }

    public void Initialize(IPlayerInput input)
    {
        input.Moved += ReceiveAxis;
        input.CursorMoved += ReceiveMouse;
        input.OnScrollWheelSwitched += ReceiveScroolWheelInput;
        input.OnShootingInput += ReceiveShootingInput;
        input.OnReloadPressed += ReceiveReloadInput;
        input.SprintingSwitched += SetSprinting;
        input.JumpPressed += Evade;
        input.AimedWeapon += AimWeapon;
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
        _animator.SetBool("isSprinting", value);
    }

    private void Evade()
    {
        if (_currentState != PlayerState.Jumping)
        {
            _currentState = PlayerState.Jumping;
            _animator.SetTrigger("Evade");
        }
    }

    private void AimWeapon(bool value)
    {
        if (value)
        {
            _currentState = PlayerState.Aiming;
        }
        else
        {
            _currentState = PlayerState.Walking;
        }

        //  _animatorOverride["Weapon_Empty"] = _currentWeapon.ReturnWeaponAnimation();

        _animator.SetBool("isAiming", value);
        _aimingRig.weight = Convert.ToInt32(value);
    }

    public void EquipWeapon(GameObject weapon)
    {
        _weaponHolder.EquipWeapon(weapon);
    }

}
