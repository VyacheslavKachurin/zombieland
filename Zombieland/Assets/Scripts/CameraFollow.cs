using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Range(0f, 10f)] [SerializeField] private float SmoothSpeed;
    [Range(0f, 10f)] [SerializeField] private float _radius;

    private Vector3 _offset;
    private Vector3 _playerPosition;
    private Vector3 _crosshairPosition;
    private bool _isPaused = false;

    private void LateUpdate()
    {
        CameraFollowCrosshair(_playerPosition, _crosshairPosition);
    }

    public void CameraFollowCrosshair(Vector3 playerPosition, Vector3 crosshairPosition)
    {
        if (!_isPaused)
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

    }
    public void SetOffset(Vector3 playerPosition)
    {
        _offset = transform.position - playerPosition;
    }
    public void GetPlayerPosition(Vector3 playerPosition)
    {
        _playerPosition = playerPosition;
    }
    public void GetCrosshairPosition(Vector3 crosshairPosition)
    {
        _crosshairPosition = crosshairPosition;
    }
    public void PauseGame(bool isPaused)
    {
        _isPaused = isPaused;
    }

}
