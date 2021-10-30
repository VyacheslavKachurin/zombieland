using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatLine : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private Button _plusButton;

    public void AssignButton(Stat stat)
    {
        _nameText.text = stat.GetUIName();
        _valueText.text = stat.GetValue().ToString();
        _plusButton.onClick.AddListener(stat.IncreaseValue);
        _plusButton.onClick.AddListener(ExperienceSystem.ExperienceSystemInstance.UsePoint);
        ToggleButton(false);

        stat.OnValueChanged += UpdateValue;
    }
    private void UpdateValue(float value)
    {
        _valueText.text = value.ToString();
    }
    public void ToggleButton(bool isActive)
    {
        _plusButton.interactable = isActive;
    }


}
