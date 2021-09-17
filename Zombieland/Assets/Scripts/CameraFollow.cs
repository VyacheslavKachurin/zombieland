using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
<<<<<<< Updated upstream
    public Transform player;
    private Vector3 offset;
   
    [Range(0f,5f)]public float smoothSpeed;
    
    // Start is called before the first frame update
    void Awake()
=======
    public Transform Player;
    private Vector3 _offset;

    [Range(0f, 5f)]
    public float SmoothSpeed;
    [Range(0f, 10f)]
    public float _radius;
    public Transform Crosshair;

    private void Awake()
>>>>>>> Stashed changes
    {
        offset = transform.position-player.position;   
    }

<<<<<<< Updated upstream
    // Update is called once per frame
    void LateUpdate()
=======
    private void LateUpdate()
>>>>>>> Stashed changes
    {
        CameraFollowCrosshair();
    }

    private void CameraFollowCrosshair()
    {
<<<<<<< Updated upstream
        Vector3 wantedPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, wantedPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
        
    }
  
    
   
     
=======
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
>>>>>>> Stashed changes
}
