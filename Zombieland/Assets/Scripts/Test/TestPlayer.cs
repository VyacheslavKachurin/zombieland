using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private Animator _animator;
    private CharacterController _cc;
    private float _vertical;
    private float _horizontal;
    private Vector3 _direction;
    private Ray _ray;
    public float Speed = 10f;

    void Start()
    {
        _cc = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        _vertical = Input.GetAxis("Vertical");
        _horizontal = Input.GetAxis("Horizontal");
        _direction = new Vector3(_horizontal, 0, _vertical);
        Move();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SetRunning(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            SetRunning(false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Aim(1);
        }
        if (Input.GetMouseButtonUp(1))
        {
            Aim(0);
        }
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Jump");
        }
        */
        ObjectCheck();
    }
    private void Move()
    {

        float velocityZ = Vector3.Dot(_direction.normalized, transform.forward);
        float velocityX = Vector3.Dot(_direction.normalized, transform.right);

        _animator.SetFloat("vertical", velocityZ, 0.1f, Time.deltaTime);
        _animator.SetFloat("horizontal", velocityX, 0.1f, Time.deltaTime);

        if (_direction.magnitude != 0)
        {
            transform.rotation =Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_direction),Time.deltaTime*10f); //keep player look towards last move
        }

        

   
    }
    private void SetRunning(bool value)
    {
        _animator.SetBool("isRunning", value);
    }
    private void Aim(int value)
    {

        Vector3 destination;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
        {
            destination = hitInfo.point;
            _animator.SetLayerWeight(1, value);

        }
        


    }
    private void FixedUpdate()
    {
        Gravity();
    }
    private void Gravity()
    {
        if (!_cc.isGrounded)
        {
            _cc.Move(Physics.gravity*Time.fixedDeltaTime);
        }
    }
    private void ObjectCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            _ray.origin = transform.position;
            _ray.direction = transform.forward;

            if(Physics.Raycast(_ray,out RaycastHit hitInfo, 1f))
            {
                if (hitInfo.rigidbody.tag == "Obstacle")
                {
                    _animator.SetTrigger("JumpOver");
                }
            }
        }
    }



}
