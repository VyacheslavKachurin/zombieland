using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Range(0f, 10f)] [SerializeField] private float SmoothSpeed;
    [Range(0f, 10f)] [SerializeField] private float _radius;

    private Vector3 _offset;

    public Transform Player;
    public Transform Crosshair;

    private void Awake()
    {
        _offset = transform.position - Player.position;
    }

    private void LateUpdate()
    {
        CameraFollowCrosshair();
    }

    private void CameraFollowCrosshair()
    {
        Vector3 clampedTarget = new Vector3(
            Mathf.Clamp(Crosshair.position.x, Player.position.x - _radius, Player.position.x + _radius), // X position
            Player.position.y,
            Mathf.Clamp(Crosshair.position.z, Player.position.z - _radius, Player.position.z + _radius) // Y position
            );
        Vector3 middle = (Player.position + clampedTarget) / 2;
        Vector3 wantedPosition = middle + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, wantedPosition, SmoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

}
