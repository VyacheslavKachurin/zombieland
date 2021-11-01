using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private Transform _statHolder;
    [SerializeField] private StatLine _statLine;
    [SerializeField] private TextMeshProUGUI _upgradePointsText;

    private List<StatLine> _statLines=new List<StatLine>();
    private bool _areButtonsOn;

    private ExperienceSystem _experienceSystemInstance;

    private void Start()
    {
        _experienceSystemInstance = ExperienceSystem.ExperienceSystemInstance;

        _experienceSystemInstance.OnUpgradePointsChanged += UpgradePoints;
        _experienceSystemInstance.OnUpgradePointsChanged += ToggleButtons;

        UpgradePoints(_experienceSystemInstance.UpgradePoints);
        ToggleButtons(_experienceSystemInstance.UpgradePoints);
        gameObject.SetActive(false); // subscribes to events and then turns off

    }
    public void ReceiveStats(PlayerStats stats)
    {
        CreateLine(stats.MaxHealth);
        CreateLine(stats.Speed);

    }
    private void UpgradePoints(int points)
    {
        _upgradePointsText.text = $"Points: {points}";

    }
    private void CreateLine(Stat stat)
    {
        StatLine lineInstance = Instantiate(_statLine, _statHolder);
        lineInstance.AssignButton(stat);
        _statLines.Add(lineInstance);
    }
    private void ToggleButtons(int points)
    {
        
        if (points == 0&&_areButtonsOn)
        {
            foreach (StatLine line in _statLines)
            {
                line.ToggleButton(false);
            }
            _areButtonsOn = false;
        }
        if (points > 0&&!_areButtonsOn)
        {
            foreach (StatLine line in _statLines)
            {
                line.ToggleButton(true);
            }
            _areButtonsOn = true;
        }
    }
    public void ToggleUpgradePanel(bool value)
    {
        gameObject.SetActive(value);
    }



}
