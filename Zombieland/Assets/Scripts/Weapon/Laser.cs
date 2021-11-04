using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour, IShootingType
{
    [SerializeField] private LaserBeam _laserBeam;

    public void CreateShot(Vector3 target, Vector3 origin)
    {
        Vector3 direction = target - origin;
        LaserBeam beam = Instantiate(_laserBeam, origin, transform.rotation);
        beam.SetVelocity(direction.normalized * 20);
    }
}
