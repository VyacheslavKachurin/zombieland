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

    private ExperienceSystem _experienceSystem;

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
        OnGamePaused += _crosshair.PauseCursor;


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

        _player.OnPlayerDeath += _HUD.GameOver;
        _player.OnWeaponChanged += AssignWeapon;
        _player.OnPlayerGotAttacked += _HUD.UpdateHealth;

        SetExperienceSystem();
       

    }

    public void TogglePause()
    {
        //maybe switch cursor sprite here on pause?

        _isGamePaused = !_isGamePaused;
        if (_isGamePaused)
        {
            Time.timeScale = 0;
            _player.enabled = !_isGamePaused;
            _player.ReceiveShootingInput(false);

            _inputController.OnShootingInput -= _player.ReceiveShootingInput;
        }
        else
        {
            Time.timeScale = 1;
            _player.enabled = !_isGamePaused;
            _inputController.OnShootingInput += _player.ReceiveShootingInput;
            
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
        currentWeapon.OnWeaponReload += _crosshair.ReloadingSprite;
        currentWeapon.OnWeaponReload += _inputController.IsWeaponReloading;
        _HUD.UpdateBullets(currentWeapon.ReturnBulletsAmount());

        _inputController.OnMouseMoved += currentWeapon.ReceiveAim;
        _HUD.UpdateImage(currentWeapon.WeaponIcon());
        
    }
    private void SetExperienceSystem()
    {
        PlayerStats playerStats = _player.ReturnPlayerStats();
        _experienceSystem = new ExperienceSystem();

        _HUD.UpdateMaxExperience(_experienceSystem.GetExperienceToNextLevel()); 
        //event fires before hud manages to subscribe to it
        
        _experienceSystem.OnXPGained += _HUD.UpdateXP;
        _experienceSystem.OnLevelUp += _HUD.UpdateLevel;

        _inputController.OnUpgradeButtonPressed += _HUD.ToggleUpgradePanel;
        _inputController.OnUpgradeButtonPressed += TogglePause; // it turns on the upgrade panel as well as menu 

        _HUD.ReturnUpgradePanel().ReceiveStats(playerStats);

        playerStats.MaxHealth.OnValueChanged += _HUD.UpgradeMaxHealthValue;



    }
}
