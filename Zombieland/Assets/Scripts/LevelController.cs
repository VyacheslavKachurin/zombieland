using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LevelController : MonoBehaviour
{
    public event Action<bool> OnGamePaused; 

    [SerializeField] private Player _player;
    [SerializeField] private InputController _inputController;
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private Crosshair _crosshair;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private HUD _HUD;
    [SerializeField] private Canvas _enemyCanvas;

    private Weapon _weapon;
    private bool _isGamePaused;
    private bool _isGameOver;
    private void Start()
    {
        _isGamePaused = false;
        Initialize();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&!_isGameOver)
        {
            PauseGame();
        }
    }

    private void Initialize()
    {
        _enemyCanvas = Instantiate(_enemyCanvas);
        _enemySpawner = Instantiate(_enemySpawner, Vector3.zero, Quaternion.identity);
        _enemySpawner.SetCanvas(_enemyCanvas);
        OnGamePaused += _enemySpawner.PauseGame;

        _cameraFollow = Instantiate(
            _cameraFollow,
            _cameraFollow.transform.position,
            _cameraFollow.transform.rotation);

        _cameraFollow.SetOffset(_player.transform.position);
        OnGamePaused += _cameraFollow.PauseGame;

        _crosshair = Instantiate(_crosshair, Vector3.zero, Quaternion.identity);
        _crosshair.OnCrosshairMoved += _cameraFollow.GetCrosshairPosition;
       

        _player = Instantiate(_player, Vector3.zero, Quaternion.identity);
        _player.OnPlayerMoved += _cameraFollow.GetPlayerPosition;
        _player.OnPlayerDeath += _enemySpawner.StopSpawning;
        _player.OnPlayerDeath += GameOver;
        OnGamePaused += _player.PauseGame;
       
        //fix (GetComponentInChildren) because its too deep
        _weapon = _player.GetComponentInChildren<Weapon>();
        OnGamePaused += _weapon.PauseGame;

        _inputController = Instantiate(_inputController, Vector3.zero, Quaternion.identity);
        _inputController.OnAxisMoved += _player.ReceiveAxis;
        _inputController.OnMouseMoved += _player.ReceiveMouse;
        _inputController.OnMouseMoved += _crosshair.Aim;
        _inputController.OnMouseMoved += _weapon.TakeMousePosition;

        _HUD = Instantiate(_HUD);
        OnGamePaused += _HUD.PauseGame;
        _HUD.ContinueButton.onClick.AddListener(this.PauseGame); //Action and UnityAction issues
        _player.OnPlayerDeath += _HUD.GameOver;

        _player.OnPlayerGotAttacked += _HUD.UpdateHealth;
        _weapon.OnBulletAmountChanged += _HUD.UpdateBullets;

    }
    public void PauseGame()
    {
        
        _isGamePaused = !_isGamePaused;       
        OnGamePaused?.Invoke(_isGamePaused);
    }
    public void GameOver(bool isGamePaused)
    {
        PauseGame();
        _isGameOver = isGamePaused;
    }
}
