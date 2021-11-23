using UnityEngine;
using UnityEngine.UI;
using System;

public class InventorySlot : MonoBehaviour
{
    public event Action<Item> ItemRemoved;

    public Item Item = null;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _removeButton;
    [SerializeField] private Button _itemButton;

 
    public void AddItem(Item newItem)
    {

        Item = newItem;
        _icon.sprite = Item.Icon;
        _icon.enabled = true;

        _removeButton.interactable = true;
        _removeButton.onClick.AddListener(RemoveButtonClicked);

        _itemButton.interactable = true;
        _itemButton.onClick.AddListener(UseItem);
    }

    public void ClearSlot()
    {
        Item = null;
        _icon.sprite = null;
        _icon.enabled = false;
        _removeButton.interactable = false;
        _removeButton.onClick.RemoveAllListeners();

        _itemButton.interactable = false;
        _itemButton.onClick.RemoveAllListeners();
    }

    private void RemoveButtonClicked()
    {
        ClearSlot();
        ItemRemoved?.Invoke(Item);
    }

    private void UseItem()
    {
        if (Item != null)
        {
            Item.Use();
        }
    }


}
