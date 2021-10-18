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

    private LevelSystem _levelSystem;

    //  private Weapon _weapon;
    private bool _isGamePaused;
    private bool _isGameOver;
    private void Awake()
    {

        Initialize();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_isGameOver)
        {
            TogglePause();
        }
    }

    private void Initialize()
    {
        _isGamePaused = false;
        Time.timeScale = 1;

        _enemyCanvas = Instantiate(_enemyCanvas);
        _enemySpawner = Instantiate(_enemySpawner, Vector3.zero, Quaternion.identity);
        _enemySpawner.SetCanvas(_enemyCanvas);


        _cameraFollow = Instantiate(
            _cameraFollow,
            _cameraFollow.transform.position,
            _cameraFollow.transform.rotation);

        _cameraFollow.SetOffset(_player.transform.position);


        _crosshair = Instantiate(_crosshair, Vector3.zero, Quaternion.identity);
        _crosshair.OnCrosshairMoved += _cameraFollow.GetCrosshairPosition;


        _player = Instantiate(_player, Vector3.zero, Quaternion.identity);
        _player.OnPlayerMoved += _cameraFollow.GetPlayerPosition;
        _player.OnPlayerDeath += _enemySpawner.StopSpawning; //take care of bool
        _player.OnPlayerDeath += GameOver;
        _player.OnPlayerMoved += _enemySpawner.GetPlayerPosition;


        _inputController = Instantiate(_inputController, Vector3.zero, Quaternion.identity);
        _inputController.OnAxisMoved += _player.ReceiveAxis;
        _inputController.OnMouseMoved += _player.ReceiveMouse;
        _inputController.OnMouseMoved += _crosshair.Aim;
        _inputController.OnScrollWheelSwitched += _player.ReceiveScroolWheelInput;
        _inputController.OnShootingInput += _player.ReceiveShootingInput;
        _inputController.OnReloadPressed += _player.ReceiveReloadInput;



        _HUD = Instantiate(_HUD);
        OnGamePaused += _HUD.PauseGame;
        _HUD.ContinueButton.onClick.AddListener(this.TogglePause); //Action and UnityAction issues
        _inputController.OnInventoryButtonPressed += _HUD.ToggleInventoryPanel;

        _player.OnPlayerDeath += _HUD.GameOver;
        _player.OnWeaponChanged += AssignWeapon;
        _player.OnPlayerGotAttacked += _HUD.UpdateHealth;
        _player.GetComponent<CharacterStats>().MaxHealth.OnValueChanged += _HUD.UpgradeMaxHealthValue;

        AssignLevelSystem();


    }
    private void AssignLevelSystem()
    {
        _levelSystem = new LevelSystem();
        _levelSystem.OnExperienceGained += _HUD.UpdateExperienceBar;
        _levelSystem.OnLevelUp += _HUD.UpdateLevelText;
    }
    public void TogglePause()
    {
        _isGamePaused = !_isGamePaused;
        if (_isGamePaused)
        {
            Time.timeScale = 0;
            _player.enabled = !_isGamePaused;
        }
        else
        {
            Time.timeScale = 1;
            _player.enabled = !_isGamePaused;
        }

        OnGamePaused?.Invoke(_isGamePaused);
    }
    public void GameOver(bool isGamePaused)
    {
        TogglePause();
        _isGameOver = isGamePaused;
    }
    public void AssignWeapon(IWeapon currentWeapon)
    {

        currentWeapon.OnBulletsAmountChanged += _HUD.UpdateBullets;
        currentWeapon.OnWeaponReload += _crosshair.ChangeCursor;
        currentWeapon.OnWeaponReload += _inputController.IsWeaponReloading;
        _HUD.UpdateBullets(currentWeapon.ReturnBulletsAmount());

        _inputController.OnMouseMoved += currentWeapon.ReceiveAim;
        _HUD.UpdateImage(currentWeapon.WeaponIcon());


    }
}
