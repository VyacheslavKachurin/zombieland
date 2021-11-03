using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LevelController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private InputController _inputController;
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private Crosshair _crosshair;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private HUD _HUD;
    [SerializeField] private UpgradeMenu _upgradeMenu;
    [SerializeField] private PauseMenu _pauseMenu;
    

    private ExperienceSystem _experienceSystem;
    private SaveSystem _saveSystem;


    private bool _isGamePaused;
    private bool _isGameOver;

    private void Awake()
    {        
        _saveSystem = new SaveSystem();

        Initialize();
       
    }

    private void Initialize()
    {
        _pauseMenu = Instantiate(_pauseMenu);
        _isGamePaused = false;
        Time.timeScale = 1;

       
        _enemySpawner = Instantiate(_enemySpawner, Vector3.zero, Quaternion.identity);


        _cameraFollow = Instantiate(
            _cameraFollow,
            _cameraFollow.transform.position,
            _cameraFollow.transform.rotation);

        _crosshair = Instantiate(_crosshair, Vector3.zero, Quaternion.identity);
        _cameraFollow.GetCrosshairPosition(_crosshair.transform);

        _player = Instantiate(_player, Vector3.zero, Quaternion.identity);

        _cameraFollow.SetTarget(_player.transform);

        _player.OnPlayerDeath += _enemySpawner.StopSpawning; //TODO : take care of bool
        _player.OnPlayerDeath += GameOver;

        _enemySpawner.SetTarget(_player.transform);

        _inputController = Instantiate(_inputController, Vector3.zero, Quaternion.identity);
        _inputController.OnMouseMoved += _crosshair.Aim;
        _inputController.OnGamePaused += _crosshair.PauseCursor;

        _inputController.OnGamePaused += TogglePause;

        _player.Initialize(_inputController);

        _HUD = Instantiate(_HUD);
        _inputController.OnGamePaused += _pauseMenu.ShowPanel;

        _pauseMenu.ContinueButton.onClick.AddListener(Continue);
        _pauseMenu.SaveButton.onClick.AddListener(SaveGame);

        _pauseMenu.LoadButton.onClick.AddListener(GameManager.Instance.LoadGame);

        _player.OnPlayerDeath += _pauseMenu.GameOver;
        _player.OnWeaponChanged += AssignWeapon;
        _player.OnPlayerGotAttacked += _HUD.UpdateHealth;

        _upgradeMenu = Instantiate(_upgradeMenu);

        _inputController.OnUpgradeButtonPressed += _upgradeMenu.ToggleUpgradePanel;
        _inputController.OnUpgradeButtonPressed += TogglePause;

        SetExperienceSystem();
    }
 
    private void Continue()
    {
        TogglePause(false);
        _pauseMenu.ShowPanel(false);
        _inputController.Continue();
        _crosshair.PauseCursor(false);

    }

    public void TogglePause(bool isPaused)
    {
        _isGamePaused = isPaused;
        if (_isGamePaused)
        {
            Time.timeScale = 0;   
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void GameOver(bool isGamePaused)
    {
        TogglePause(true);
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

        _upgradeMenu.SetExperienceSystem(_experienceSystem);

        _upgradeMenu.ReceiveStats(playerStats);

        playerStats.MaxHealth.OnValueChanged += _HUD.UpgradeMaxHealthValue;

        _enemySpawner.SetExperienceSystem(_experienceSystem);

    }

    private void SaveGame()
    {
        _saveSystem.SaveGame(_experienceSystem, _player);

    }

    public void LoadGame()
    {
        _saveSystem.LoadGame(_experienceSystem, _player);
    }


}
