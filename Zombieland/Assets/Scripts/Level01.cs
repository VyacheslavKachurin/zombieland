using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level01 : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private InputController _inputController;
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private Crosshair _crosshair;


    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _playerMovement = Instantiate(_playerMovement, Vector3.zero, Quaternion.identity);
        _cameraFollow = Instantiate(
            _cameraFollow,
            _cameraFollow.transform.position,
            _cameraFollow.transform.rotation);

        _cameraFollow.SetOffset(_playerMovement.transform.position);
        _crosshair = Instantiate(_crosshair, Vector3.zero, Quaternion.identity);

        _playerMovement.OnPlayerMoved += _cameraFollow.GetPlayerPosition;
        _crosshair.OnCrosshairMoved += _cameraFollow.GetCrosshairPosition;
        _cameraFollow.SetOffset(_playerMovement.transform.position);
        _playerMovement.OnAimMoved += _crosshair.Aim;


    }
}
