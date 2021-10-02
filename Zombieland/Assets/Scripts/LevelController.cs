using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private InputController _inputController;
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private Crosshair _crosshair;
    [SerializeField] private EnemySpawner _enemySpawner;

    private Weapon _weapon;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _enemySpawner = Instantiate(_enemySpawner, Vector3.zero, Quaternion.identity);

        _cameraFollow = Instantiate(
            _cameraFollow,
            _cameraFollow.transform.position,
            _cameraFollow.transform.rotation);

        _cameraFollow.SetOffset(_playerMovement.transform.position);

        _crosshair = Instantiate(_crosshair, Vector3.zero, Quaternion.identity);
        _crosshair.OnCrosshairMoved += _cameraFollow.GetCrosshairPosition;

        _playerMovement = Instantiate(_playerMovement, Vector3.zero, Quaternion.identity);
        _playerMovement.OnPlayerMoved += _cameraFollow.GetPlayerPosition;
        _playerMovement.OnPlayerDeath += _enemySpawner.StopSpawning;

        //fix (GetComponentInChildren) because its too deep
        _weapon = _playerMovement.GetComponentInChildren<Weapon>();

        _inputController = Instantiate(_inputController, Vector3.zero, Quaternion.identity);
        _inputController.OnAxisMoved += _playerMovement.ReceiveAxis;
        _inputController.OnMouseMoved += _playerMovement.ReceiveMouse;
        _inputController.OnMouseMoved += _crosshair.Aim;
        _inputController.OnMouseMoved += _weapon.TakeMousePosition;
    }
}
