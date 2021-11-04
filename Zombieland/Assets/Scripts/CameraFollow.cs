using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Range(0f, 10f)] [SerializeField] private float SmoothSpeed;
    [Range(0f, 10f)] [SerializeField] private float _radius;

    private Vector3 _offset;
    private Transform _targetTransform;
    private Transform _crosshairTransform;

    private void LateUpdate()
    {
        CameraFollowCrosshair(_targetTransform.position, _crosshairTransform.position);
    }

    public void CameraFollowCrosshair(Vector3 targetPosition, Vector3 crosshairPosition)
    {
            Vector3 clampedTarget = new Vector3(
                Mathf.Clamp(crosshairPosition.x, targetPosition.x - _radius, targetPosition.x + _radius), // X position
                targetPosition.y,
                Mathf.Clamp(crosshairPosition.z, targetPosition.z - _radius, targetPosition.z + _radius) // Z position
                );
            Vector3 middle = (targetPosition + clampedTarget) / 2;
            Vector3 wantedPosition = middle + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, wantedPosition, SmoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        

    }

    public void SetTarget(Transform targetTransform)
    {
        _targetTransform = targetTransform;
        _offset = transform.position - targetTransform.position;
    }

    public void SetCrosshairPosition(Transform crosshairTransform)
    {
        _crosshairTransform = crosshairTransform;
    }

}
