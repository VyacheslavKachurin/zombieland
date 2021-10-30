using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Crosshair : MonoBehaviour
{
    public event Action<Vector3> OnCrosshairMoved;

    [SerializeField] private Texture2D _whiteCrosshair;
    [SerializeField] private Texture2D _reloadingTexture;
    [SerializeField] private Texture2D _redCrosshair;
    [SerializeField] private Texture2D _pauseCursor;
    private Texture2D sprite;
    private bool _isReloading;

    private float _offset = 0.1f;
    private Vector2 _hotSpot;

    private void Awake()
    {
        ReloadingSprite(false);
    }
    public void Aim(Vector3 target)
    {
        transform.position = new Vector3(target.x, target.y + _offset, target.z);
        
        OnCrosshairMoved?.Invoke(transform.position);
    }
    public void ReloadingSprite(bool isReloading)
    {
        _isReloading = isReloading;  
        if (!isReloading)
        {
            
             sprite = _whiteCrosshair;
        }
        else
        {
             sprite = _reloadingTexture;
        }
        _hotSpot = new Vector2(sprite.width / 2, sprite.height / 2); //different sprite size
        Cursor.SetCursor(sprite, _hotSpot, CursorMode.Auto);
    } 
    public void PauseCursor(bool pause)
    {
        Texture2D newSprite;
        if (pause)
        {
            newSprite = _pauseCursor;
            _hotSpot = new Vector2(0,0); //different sprite size
        }
        else
        {
            newSprite = sprite;
            _hotSpot = new Vector2(sprite.width / 2, sprite.height / 2);
        }
        
        Cursor.SetCursor(newSprite, _hotSpot, CursorMode.Auto);
       
    }


}
