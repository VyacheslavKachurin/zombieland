using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Range(0f, 10f)] [SerializeField] private float SmoothSpeed;
    [Range(0f, 10f)] [SerializeField] private float _radius;

    private Vector3 _offset;
    private Transform _playerTransform;
    private Transform _crosshairTransform;

    private void LateUpdate()
    {
        CameraFollowCrosshair(_playerTransform.position, _crosshairTransform.position);
    }

    public void CameraFollowCrosshair(Vector3 playerPosition, Vector3 crosshairPosition)
    {
            Vector3 clampedTarget = new Vector3(
                Mathf.Clamp(crosshairPosition.x, playerPosition.x - _radius, playerPosition.x + _radius), // X position
                playerPosition.y,
                Mathf.Clamp(crosshairPosition.z, playerPosition.z - _radius, playerPosition.z + _radius) // Z position
                );
            Vector3 middle = (playerPosition + clampedTarget) / 2;
            Vector3 wantedPosition = middle + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, wantedPosition, SmoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        

    }

    public void SetOffset(Vector3 playerPosition)
    {
        _offset = transform.position - playerPosition;
    }

    public void GetPlayerPosition(Transform playerPosition)
    {
        _playerTransform = playerPosition;
    }

    public void GetCrosshairPosition(Transform crosshairPosition)
    {
        _crosshairTransform = crosshairPosition;
    }

}
