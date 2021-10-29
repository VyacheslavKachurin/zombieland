using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour, IShootingType
{
    [SerializeField] private GameObject _laserBeam;
    public void CreateShot(Vector3 target, Vector3 origin)
    {
       // Vector3 desiredRotation = new Vector3(origin.x, origin.y, origin.z);
        GameObject beam = Instantiate(_laserBeam, origin,Quaternion.Euler(origin));

        beam.GetComponent<Rigidbody>().velocity = beam.transform.forward * 20;
    }
}
