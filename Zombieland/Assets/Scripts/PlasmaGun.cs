using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaGun : MonoBehaviour
{
    public Transform GunPoint;
    public PlayerMovement PlayerMovement;
    private Vector3 _aim;
    private Coroutine _firingCoroutine;
    public GameObject PlasmaShot;
    public float FiringSpeed = 0.1f;
    public float ShotSpeed = 10f;



    void Update()
    {
        Shoot();
    }
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _firingCoroutine = StartCoroutine(FireContiniously());
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(_firingCoroutine);
        }

    }

    private IEnumerator FireContiniously()
    {
        while (true)
        {
            
            GameObject shotInstance = Instantiate(PlasmaShot, GunPoint.position, Quaternion.identity);
            Vector3 target = PlayerMovement.Destination;
            
            _aim = target-shotInstance.transform.position;
            
            shotInstance.GetComponent<Rigidbody>().velocity = _aim.normalized * ShotSpeed;
            yield return new WaitForSeconds(FiringSpeed);

        }

    }
}
