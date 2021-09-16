using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator _animator;
    private float _horizontal;
    private float _vertical;
    private float _dampTime = 0.1f;
    private Vector3 _direction;
    private Camera _camera;

    [SerializeField] 
    [Range(0f, 10f)] 
    private float _movementSpeed;

    [SerializeField] 
    private LayerMask _aimLayerMask;


    // Start is called before the first frame update
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

        _animator.SetFloat("VelocityZ", _vertical, _dampTime, Time.deltaTime);
        _animator.SetFloat("VelocityX", _horizontal, _dampTime, Time.deltaTime);
    }
    private void AimTowardsMouse()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _aimLayerMask))
        {
            var destination = hitInfo.point;

            destination.y = transform.position.y;

            Vector3 lookDirection = destination - transform.position;
            lookDirection.Normalize();

            transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        }

    }
}
