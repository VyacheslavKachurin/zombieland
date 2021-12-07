using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public event Action<Item> ItemRemoved;
    public event Action<Item> ItemEquipped;

    public Item Item = null;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _removeButton;
    [SerializeField] private Button _itemButton;
    [SerializeField] private TextMeshProUGUI _itemAmountText;


    public void AddItem(Item newItem)
    {

        Item = newItem;
        _icon.sprite = Item.Icon;
        _icon.enabled = true;

        ToggleRemoveButton();

        if (_removeButton != null)
        {
            _removeButton.onClick.AddListener(RemoveButtonClicked);
        }

        if (_itemButton != null)
        {
            _itemButton.interactable = true;
            _itemButton.onClick.AddListener(EquipItem);
        }
    }

    public void ClearSlot()
    {
        Item = null;
        _icon.sprite = null;
        _icon.enabled = false;

        if (_removeButton != null)
        {
            _removeButton.interactable = false;
            _removeButton.onClick.RemoveAllListeners();
        }

        if (_itemButton != null)
        {
            _itemButton.interactable = false;
            _itemButton.onClick.RemoveAllListeners();
        }

    }

    private void RemoveButtonClicked()
    {
        ClearSlot();
        ItemRemoved?.Invoke(Item);
    }

    private void EquipItem()
    {
        if (Item != null)
        {
            ItemEquipped?.Invoke(Item);
            ClearSlot();
        }
    }

    public void Disable()
    {
        _itemButton.interactable = false;
        ToggleRemoveButton();
    }

    public void ToggleRemoveButton()
    {
        if (_removeButton != null)
            _removeButton.interactable = !_removeButton.interactable;
    }


}
