using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class PlayerMovement : MonoBehaviour
{
    public event Action<Vector3> OnAimMoved;
    public event Action<Vector3> OnPlayerMoved;
    public event Action<bool> OnPlayerDeath;

    [Range(0f, 10f)]
    [SerializeField] private float _movementSpeed;

    private float _horizontal;
    private float _vertical;
    private float _velocityZ;
    private float _velocityX;
    private float _dampTime = 0.1f;
    private Vector3 _direction;
    private Animator _animator;
    private Camera _camera;
    private int _health = 1;
    private Vector3 _mousePosition;
    private bool _isDead = false;


    private void Start()
    {
        //  UpdateHealth();
        _camera = Camera.main;

        _animator = GetComponent<Animator>();
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

        transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
    }
    public void TakeDamage()
    {
        if (_health == 0)
        {
            return;
        }
        _health--;
        UpdateHealth();
        if (_health > 0)
        {
            _animator.SetTrigger("GetHit");


        }
        if (_health == 0)
        {
            Die();
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
    private void UpdateHealth()
    {
        // HealthText.text = $"Health: {_health}";
        Debug.Log("Im hit");
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


}
