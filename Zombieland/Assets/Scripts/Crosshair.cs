using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Crosshair : MonoBehaviour
{
    public event Action<Vector3> OnCrosshairMoved;

    [SerializeField] private Texture2D CursorTexture;

    private float _offset = 0.1f;
    private Vector2 _hotSpot;

    private void Awake()
    {
        _hotSpot = new Vector2(CursorTexture.width / 2, CursorTexture.height / 2);

    }
    public void Aim(Vector3 target)
    {

        transform.position = new Vector3(target.x, target.y + _offset, target.z);
        Cursor.SetCursor(CursorTexture, _hotSpot, CursorMode.Auto);



        OnCrosshairMoved?.Invoke(transform.position);

    }



}
