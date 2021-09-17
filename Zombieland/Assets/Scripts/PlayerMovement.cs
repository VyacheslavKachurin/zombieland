using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
<<<<<<< Updated upstream
    private Animator animator;
    private float horizontal;
    private float vertical;
    private float dampTime = 0.1f;
    private Vector3 direction;
    private Camera cam;
    [SerializeField][Range(0f,10f)]private float movementSpeed;
    [SerializeField]private LayerMask aimLayerMask;
=======
    private Animator _animator;
    private float _horizontal;
    private float _vertical;
    private float _velocityZ;
    private float _velocityX;
    private float _dampTime = 0.1f;
    private Vector3 _direction;
    private Camera _camera;
    public Crosshair Crosshair;

    [SerializeField]
    [Range(0f, 10f)]
    private float _movementSpeed;

    [SerializeField]
    private LayerMask _aimLayerMask;

>>>>>>> Stashed changes

    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        animator = GetComponent<Animator>();
    }

<<<<<<< Updated upstream
    // Update is called once per frame
    void Update()
=======
    private void Update()
>>>>>>> Stashed changes
    {
        Move();
        AimTowardsMouse();
    }
    private void FixedUpdate()
    {

    }
    private void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        

        direction = new Vector3(horizontal, 0, vertical);
        transform.Translate(direction.normalized*movementSpeed*Time.deltaTime,Space.World);

<<<<<<< Updated upstream
        animator.SetFloat("VelocityZ", vertical,dampTime,Time.deltaTime);
        animator.SetFloat("VelocityX", horizontal,dampTime,Time.deltaTime);
=======
        _direction = new Vector3(_horizontal, 0, _vertical);
        transform.Translate(_direction.normalized * _movementSpeed * Time.deltaTime, Space.World);

        _velocityZ = Vector3.Dot(_direction.normalized, transform.forward);
        _velocityX = Vector3.Dot(_direction.normalized, transform.right);

        _animator.SetFloat("VelocityZ", _velocityZ, _dampTime, Time.deltaTime);
        _animator.SetFloat("VelocityX", _velocityX, _dampTime, Time.deltaTime);
>>>>>>> Stashed changes
    }
    private void AimTowardsMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit hitInfo, Mathf.Infinity, aimLayerMask))
        {
            var destination = hitInfo.point;
            destination.y = transform.position.y;
<<<<<<< Updated upstream
           
=======

            Crosshair.Aim(destination);

>>>>>>> Stashed changes
            Vector3 lookDirection = destination - transform.position;
            lookDirection.Normalize();
            
            transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        }
    }
}
