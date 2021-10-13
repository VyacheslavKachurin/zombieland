using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Player : MonoBehaviour
{
    public event Action<Vector3> OnPlayerMoved;
    public event Action<bool> OnPlayerDeath;
    public event Action<float> OnPlayerGotAttacked;
    public event Action<IWeapon> OnWeaponChanged;

    public event Action<int> BulletsTest;

    [Range(0f, 10f)]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private WeaponHolder _weaponHolder;

    private float _horizontal;
    private float _vertical;
    private float _velocityZ;
    private float _velocityX;
    private float _dampTime = 0.1f;
    private Vector3 _direction;
    private Animator _animator;
    private float _health = 100;
    private Vector3 _mousePosition;
    private bool _isDead = false;

    private float _damageAmount = 20;//testing
    private IWeapon _currentWeapon;

    private void Start()
    {
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
    private void TakeDamage()
    {
        if (_health > 0)
        {
            _health -= _damageAmount;
            OnPlayerGotAttacked(_damageAmount);
            if (_health <= 0)
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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HitCollider"))
        {
            TakeDamage();
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
        if(isReloading)
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





}
