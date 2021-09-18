using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private float _offset = 0.1f;
    public void Aim(Vector3 target)
    {
        transform.position = new Vector3(target.x, target.y + _offset, target.z);
        Cursor.visible = false;
    }
}
