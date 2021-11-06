using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour, IShootingType
{
    //TODO: visualization must work all the time. not only during CreateShot();

    [SerializeField] private Projectile _projectile;

    private LineRenderer _lineRenderer;

    public int lineSegment;

    private Camera _camera;

    private Vector3 _shootPoint;

    private void Start()
    {
        _camera = Camera.main;
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = lineSegment;
    }
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
        _shootPoint = origin;
        Vector3 velocity = CalculateVelocity(target,origin,1f);

        Visualize(velocity);

        transform.rotation = Quaternion.LookRotation(velocity); //  move to animation rigging

        Projectile projectile = Instantiate(_projectile, origin, Quaternion.identity);
        projectile.SetVelocity(velocity);
    }

    private Vector3 CalculatePositionInTime(Vector3 v0, float time)
    {
        Vector3 Vxz = v0;
        Vxz.y = 0;

        Vector3 result = _shootPoint+v0*time;

        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (v0.y * time) + _shootPoint.y;

        result.y = sY;

        return result;
    }
    private void Visualize(Vector3 v0) {
        for (int i = 0; i < lineSegment; i++)
        {
            Vector3 position = CalculatePositionInTime(v0, i / (float)(lineSegment));

            _lineRenderer.SetPosition(i, position);
        }
    }
}
