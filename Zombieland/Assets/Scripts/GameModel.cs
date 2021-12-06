using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameModel : MonoBehaviour
{
    public Player Player
    { get { return _player; } }

    public ExperienceSystem ExperienceSystem
    { get { return _experienceSystem; } }

    [SerializeField] private Player _player;
    [SerializeField] private GameObject _aimingLayer;

    //TODO: move to uiroot;
    [SerializeField] private HUD _HUD;
    [SerializeField] private UpgradeMenu _upgradeMenu;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private InventoryModel _inventoryModel;
    [SerializeField] private EquipmentModel _equipmentModel;

    private FollowingCamera _followingCamera;
    private InputController _inputController;
    private Crosshair _crosshair;
    private EnemySpawner _enemySpawner;
    private IUIRoot _IUIRoot;

    private IResourceManager _iresourceManager;

    private EquipmentView _equipmentView;
    private InventoryView _inventoryView;

    private ExperienceSystem _experienceSystem;
    private AudioManager _audioManager;

    private bool _isGamePaused;

    private void Awake()
    {
        Initialize();
        _audioManager = AudioManager.Instance;
        _audioManager.PlayGameTheme();

    }
    private void Initialize()
    {
        _iresourceManager = gameObject.AddComponent<ResourceManager>(); // is it okay?

        _iresourceManager.CreateEnvironment(Environment.Environment1);

        _IUIRoot = _iresourceManager.CreateUIRoot();

       _equipmentModel = Instantiate(_equipmentModel);

        _equipmentView = _equipmentModel.GetComponent<EquipmentView>();


        _inventoryModel = Instantiate(_inventoryModel);

        _equipmentView.GetInventoryModel(_inventoryModel);

        _inventoryView = _inventoryModel.GetComponent<InventoryView>();
        _inventoryView.GetEquipmentModel(_equipmentModel);


        Instantiate(_aimingLayer, _aimingLayer.transform.position, Quaternion.identity);
        _pauseMenu = Instantiate(_pauseMenu);
        _isGamePaused = false;
        Time.timeScale = 1;




        _enemySpawner = _iresourceManager.CreatePrefabInstance<EnemySpawner>(Objects.EnemySpawner);

        _followingCamera = _iresourceManager.CreatePrefabInstance<FollowingCamera>(Objects.FollowingCamera);

        

        _crosshair = _iresourceManager.CreatePrefabInstance<Crosshair>(Objects.Crosshair);
        _followingCamera.SetCrosshairPosition(_crosshair.transform);

        _player = Instantiate(_player, Vector3.zero, Quaternion.identity);


        _player.InventoryModel = _inventoryModel;

        _followingCamera.SetTarget(_player.transform);



        _enemySpawner.SetTarget(_player.transform);

        _inputController = _iresourceManager.CreatePrefabInstance<InputController>(Objects.InputController);

        _inputController.CursorMoved += _crosshair.Aim;
        _inputController.OnGamePaused += _crosshair.PauseCursor;

      _inputController.OnGamePaused += TogglePause;

        _player.Initialize(_inputController);

        _equipmentModel.GetPlayer(_player);

        _HUD = Instantiate(_HUD);
        _inputController.OnGamePaused += _pauseMenu.ShowPanel;

        _pauseMenu.ContinueButton.onClick.AddListener(Continue);
        _pauseMenu.SaveButton.onClick.AddListener(SaveGame);
        _pauseMenu.LoadButton.onClick.AddListener(GameManager.Instance.LoadGame); // doesnt work


        _player.OnPlayerDeath += GameOver;
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

    public void AssignWeapon(IWeapon currentWeapon)
    {
        currentWeapon.OnBulletsAmountChanged += _HUD.UpdateBullets;
        currentWeapon.OnWeaponReload += _crosshair.ReloadingSprite;
        currentWeapon.OnWeaponReload += _inputController.IsWeaponReloading;
        _HUD.UpdateBullets(currentWeapon.ReturnBulletsAmount());

        _inputController.CursorMoved += currentWeapon.ReceiveAim;
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

        _inputController.InventoryButtonPressed += _inventoryModel.TogglePanel;
    }

    private void SaveGame()
    {
        GameManager.Instance.SaveGame();
    }

    public void LoadGame()
    {
        GameManager.Instance.LoadGame();
    }

    private void GameOver(bool value)
    {
        _enemySpawner.StopSpawning(value); //TODO : take care of bool
        StartCoroutine(TogglePauseAfterDeath());
    }

    private IEnumerator TogglePauseAfterDeath()
    {
        yield return new WaitForSeconds(2f);
        _pauseMenu.GameOver(true);
        TogglePause(true);
    }

}
