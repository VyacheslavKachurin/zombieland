using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Crosshair : MonoBehaviour
{
    public event Action<Vector3> OnCrosshairMoved;

    [SerializeField] private Texture2D _aimTexture;
    [SerializeField] private Texture2D _reloadingTexture;

    private float _offset = 0.1f;
    private Vector2 _hotSpot;

    private void Awake()
    {
        ChangeCursor(false);
    }
    public void Aim(Vector3 target)
    {
        transform.position = new Vector3(target.x, target.y + _offset, target.z);
        
        OnCrosshairMoved?.Invoke(transform.position);
    }
    public void ChangeCursor(bool isReloading)
    {
        Texture2D sprite;
        if (!isReloading)
        {
             sprite = _aimTexture;
        }
        else
        {
             sprite = _reloadingTexture;
        }
        _hotSpot = new Vector2(sprite.width / 2, sprite.height / 2); //different sprite size
        Cursor.SetCursor(sprite, _hotSpot, CursorMode.Auto);
    }
}
