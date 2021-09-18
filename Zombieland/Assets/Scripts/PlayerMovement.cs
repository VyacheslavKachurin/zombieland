using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private LayerMask _aimLayerMask;

    private float _horizontal;
    private float _vertical;
    private float _velocityZ;
    private float _velocityX;
    private float _dampTime = 0.1f;
    private Vector3 _direction;

    private Animator _animator;
    private Camera _camera;
    public Crosshair Crosshair;
    private void Awake()
    {
        _camera = Camera.main;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        AimTowardsMouse();
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
    }
    private void AimTowardsMouse()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _aimLayerMask))
        {
            var destination = hitInfo.point;

            Crosshair.Aim(destination);

            destination.y = transform.position.y;
            Vector3 lookDirection = destination - transform.position;
            lookDirection.Normalize();

            transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        }
    }
}
