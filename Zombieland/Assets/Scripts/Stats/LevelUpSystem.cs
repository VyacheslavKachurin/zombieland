using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _speedText;

    [SerializeField] private Button _healthPlus;
    [SerializeField] private Button _healthMinus;

    [SerializeField] private Button _speedPlus;
    [SerializeField] private Button _speedMinus;

    [SerializeField] List<Button> _buttons;

    private CharacterStats _characterStats;
    private Player _playerInstance;
    private LevelSystem _levelSystemInstance;
    private int _upgradePoints = 0;
    private bool isUpgradeAvailable;

    private void Start()
    {
        ToggleButtons();

        _levelSystemInstance = LevelSystem.LevelSystemInstance;
        _levelSystemInstance.OnUpgradePointsGained += GetUpgradePoints;

        _playerInstance = Player.PlayerInstance;
        _characterStats = Player.CharacterStats;

        _healthText.text = _characterStats.MaxHealth.GetValue().ToString();
        _speedText.text = _characterStats.MovementSpeed.GetValue().ToString();

        _characterStats.MaxHealth.OnValueChanged += UpdateHealth;
        _characterStats.MovementSpeed.OnValueChanged += UpdateMovementSpeed;

        _healthPlus.onClick.AddListener(_characterStats.MaxHealth.UpgradeValue);
        _healthPlus.onClick.AddListener(UsePoint);
        _healthMinus.onClick.AddListener(_characterStats.MaxHealth.DowngradeValue);

        _speedPlus.onClick.AddListener(_characterStats.MovementSpeed.UpgradeValue);
        _speedPlus.onClick.AddListener(UsePoint);
        _speedMinus.onClick.AddListener(_characterStats.MovementSpeed.DowngradeValue);
    }

    public void UpdateHealth(int value)
    {

        _healthText.text = _characterStats.MaxHealth.GetValue().ToString();
        _playerInstance.AssignStats();

    }
    public void UpdateMovementSpeed(int value)
    {
        _speedText.text = _characterStats.MovementSpeed.GetValue().ToString();
        _playerInstance.AssignStats();
    }
    private void GetUpgradePoints(int value)
    {
        Debug.Log($"got {value} upgrade points");
        _upgradePoints = value;
        isUpgradeAvailable = !isUpgradeAvailable;
        ToggleButtons();
    }
    private void UsePoint()
    {

        Debug.Log("points used");
        if (_upgradePoints > 1)
        {
            _upgradePoints--;
        }
        else
        {
            isUpgradeAvailable = !isUpgradeAvailable;
            ToggleButtons();

        }
    }
    private void ToggleButtons()
    {


        foreach (Button button in _buttons)
        {
            button.interactable = isUpgradeAvailable;
        }


    }

}
