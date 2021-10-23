using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Animations.Rigging;
public class Player : MonoBehaviour, IDamageable
{
    public event Action<Vector3> OnPlayerMoved;
    public event Action<bool> OnPlayerDeath;
    public event Action<float> OnPlayerGotAttacked;
    public event Action<IWeapon> OnWeaponChanged;

    [SerializeField] private WeaponHolder _weaponHolder;
    [SerializeField] private GameObject _aimingObject;
    [SerializeField] private Rig _aimRig;
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
    private List<Stat> _stats = new List<Stat>();

    private void Start()
    {



        AssignStats();

        _animator = GetComponent<Animator>();
        _weaponHolder.OnWeaponChanged += GetWeapon;

    }

    private void Update()
    {
        if (!_isDead)
        {
            Move();
            AimTowardsMouse();
        }
    }
    private void Move()
    {
        _direction = new Vector3(_horizontal, 0, _vertical);
        transform.Translate(_direction.normalized * _movementSpeed * Time.deltaTime, Space.World);

        _velocityZ = Vector3.Dot(_direction.normalized, transform.forward);
        _velocityX = Vector3.Dot(_direction.normalized, transform.right);

        _animator.SetFloat("VelocityZ", _velocityZ, _dampTime, Time.deltaTime);
        _animator.SetFloat("VelocityX", _velocityX, _dampTime, Time.deltaTime);

        OnPlayerMoved?.Invoke(transform.position);
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

    public void ReceiveAxis(float horizontal, float vertical)
    {
        _horizontal = horizontal;
        _vertical = vertical;
    }

    public void ReceiveMouse(Vector3 mousePosition)
    {
        _mousePosition = mousePosition;
        if (Time.timeScale != 0)
        {
            _aimingObject.transform.position = _mousePosition;
        }
    }

    public void ReceiveShootingInput(bool isShooting)
    {
        _currentWeapon.Shoot(isShooting);
    }

    public void ReceiveScroolWheelInput(bool input)
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

    public void ReceiveReloadInput()
    {
        _currentWeapon.Reload();
    }

    public IWeapon PassWeapon()
    {
        return _currentWeapon;
    }
    private void AssignStats()
    {
        // _playerStats = GetComponent<PlayerStats>(); moved to ReturnPlayerStats()

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



}
