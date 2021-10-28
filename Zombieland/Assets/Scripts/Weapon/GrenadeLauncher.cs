using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour, IShootingType
{
    [SerializeField] private Projectile _projectile;

    private Vector3 CalculateVelocity(Vector3 target, Vector3 origin,float time)
    {
        Vector3 distance = target - origin;

        Vector3 distanceXZ = distance;
        distanceXZ.y = 0;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 velocity = distanceXZ.normalized*Vxz;
        velocity.y = Vy;

        return velocity;
    
    }
    public void CreateShot(Vector3 target, Vector3 origin)
    {
        Vector3 velocity = CalculateVelocity(target,origin,1f);
        transform.rotation = Quaternion.LookRotation(velocity); //  move to animation rigging

        Projectile projectile = Instantiate(_projectile, origin, Quaternion.identity);
        projectile.SetVelocity(velocity);
    }
}
