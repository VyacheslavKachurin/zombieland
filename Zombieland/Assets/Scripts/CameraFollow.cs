using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;
    private Vector3 _offset;

    [Range(0f, 5f)]
    public float SmoothSpeed;

    // Start is called before the first frame update
    private void Awake()
    {
        _offset = transform.position - Player.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        CameraFollowPlayer();


    }
    private void CameraFollowPlayer()
    {
        Vector3 wantedPosition = Player.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, wantedPosition, SmoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;

    }




}
