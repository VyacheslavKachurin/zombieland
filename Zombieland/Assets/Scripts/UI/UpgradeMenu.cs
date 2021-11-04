using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private Transform _statHolder;
    [SerializeField] private StatLine _statLine;
    [SerializeField] private TextMeshProUGUI _upgradePointsText;

    private ExperienceSystem _experienceSystem;

    private List<StatLine> _statLines = new List<StatLine>();
    private bool _areButtonsOn;

    public void ReceiveStats(PlayerStats stats)
    {
        CreateLine(stats.MaxHealth);
        CreateLine(stats.Speed);
    }

    public void UpgradePoints(int points)
    {
        _upgradePointsText.text = $"Points: {points}";

    }

    private void CreateLine(Stat stat)
    {
        StatLine lineInstance = Instantiate(_statLine, _statHolder);
        lineInstance.AssignButton(stat,_experienceSystem);
        _statLines.Add(lineInstance);
    }

    public void ToggleButtons(int points)
    {

        if (points == 0 && _areButtonsOn)
        {
            foreach (StatLine line in _statLines)
            {
                line.ToggleButton(false);
            }
            _areButtonsOn = false;
        }
        if (points > 0 && !_areButtonsOn)
        {
            foreach (StatLine line in _statLines)
            {
                line.ToggleButton(true);
            }
            _areButtonsOn = true;
        }
    }

    public void ToggleUpgradePanel(bool isEnabled)
    {
        gameObject.SetActive(isEnabled);
    }

    public void SetExperienceSystem(ExperienceSystem XPSystem)
    {
        _experienceSystem = XPSystem;

        _experienceSystem.OnUpgradePointsChanged += UpgradePoints;
        _experienceSystem.OnUpgradePointsChanged += ToggleButtons;

        UpgradePoints(_experienceSystem.UpgradePoints);
        ToggleButtons(_experienceSystem.UpgradePoints);

        //TODO : add event firing when "add" as property?
    }
}
