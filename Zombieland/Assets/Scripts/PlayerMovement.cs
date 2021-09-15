using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private float horizontal;
    private float vertical;
    private float dampTime = 0.1f;
    private Vector3 direction;
    private Camera cam;
    [SerializeField][Range(0f,10f)]private float movementSpeed;
    [SerializeField]private LayerMask aimLayerMask;

    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        AimTowardsMouse();
    }
    private void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        

        direction = new Vector3(horizontal, 0, vertical);
        transform.Translate(direction.normalized*movementSpeed*Time.deltaTime,Space.World);

        animator.SetFloat("VelocityZ", vertical,dampTime,Time.deltaTime);
        animator.SetFloat("VelocityX", horizontal,dampTime,Time.deltaTime);
    }
    private void AimTowardsMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit hitInfo, Mathf.Infinity, aimLayerMask))
        {
            var destination = hitInfo.point;

            destination.y = transform.position.y;
           
            Vector3 lookDirection = destination - transform.position;
            lookDirection.Normalize();
            
            transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        }

    }
}
