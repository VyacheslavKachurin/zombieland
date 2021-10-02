using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class PlayerMovement : MonoBehaviour
{
    public static event Action<Vector3> OnAimMoved;
    public static event Action<Vector3> OnPlayerMoved;

    [Range(0f, 10f)]
    [SerializeField] private float _movementSpeed;

    private float _horizontal;
    private float _vertical;
    private float _velocityZ;
    private float _velocityX;
    private float _dampTime = 0.1f;
    private Vector3 _direction;
    public Vector3 Destination;
    private Animator _animator;
    private Camera _camera;
    private int _health = 1;
    public static bool IsDead = false;

    private void Start()
    {
        UpdateHealth();
        _camera = Camera.main;
        
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {

        if (!IsDead)
        {
            Move();
            AimTowardsMouse();
        }
    }
    private void Move()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

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
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
        {
            Destination = hitInfo.point;

            OnAimMoved(Destination);

            Destination.y = transform.position.y;
            
            Vector3 lookDirection = Destination - transform.position;
            lookDirection.Normalize();

            transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            
        }
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
        if (!IsDead)
        {
            IsDead = true;
            _animator.SetTrigger("Die");

        }
    }
    private void UpdateHealth()
    {
        // HealthText.text = $"Health: {_health}";
        Debug.Log("Im hit");
    }
   
  
}
