using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private InputController _inputController;
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private Crosshair _crosshair;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private HUD _HUD;

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

        _cameraFollow.SetOffset(_player.transform.position);

        _crosshair = Instantiate(_crosshair, Vector3.zero, Quaternion.identity);
        _crosshair.OnCrosshairMoved += _cameraFollow.GetCrosshairPosition;

        _player = Instantiate(_player, Vector3.zero, Quaternion.identity);
        _player.OnPlayerMoved += _cameraFollow.GetPlayerPosition;
        _player.OnPlayerDeath += _enemySpawner.StopSpawning;
       


        //fix (GetComponentInChildren) because its too deep
        _weapon = _player.GetComponentInChildren<Weapon>();

        _inputController = Instantiate(_inputController, Vector3.zero, Quaternion.identity);
        _inputController.OnAxisMoved += _player.ReceiveAxis;
        _inputController.OnMouseMoved += _player.ReceiveMouse;
        _inputController.OnMouseMoved += _crosshair.Aim;
        _inputController.OnMouseMoved += _weapon.TakeMousePosition;

        _HUD = Instantiate(_HUD);
        _player.OnPlayerGotAttacked += _HUD.UpdateHealth;

    }
}
