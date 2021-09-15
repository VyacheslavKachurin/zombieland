using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;
   
    [Range(0f,5f)]public float smoothSpeed;
    
    // Start is called before the first frame update
    void Awake()
    {
        offset = transform.position-player.position;   
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CameraFollowPlayer();


    }
    private void CameraFollowPlayer()
    {
        Vector3 wantedPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, wantedPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
        
    }
  
    
   
     
}
