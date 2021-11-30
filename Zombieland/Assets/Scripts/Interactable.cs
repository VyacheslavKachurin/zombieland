using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected Player _player;
    private IPlayerInput _input;
    private InventoryModel _inventoryModel;

    public abstract void Interact();

    public void ShowMessage()
    {
        Debug.Log("Press F to interact");
    }

    public void HideMessage()
    {
        
    }

    public void Subscribe()
    {
        _input.InteractButtonPressed += Interact;
    }

    public void Unsubscribe()
    {
        _input.InteractButtonPressed -= Interact;
    }

    private void OnTriggerEnter(Collider other)
    {
        _player = other.gameObject.GetComponent<Player>();
        _input = _player.Input;

        ShowMessage();
        Subscribe();
    }

    private void OnTriggerExit(Collider other)
    {
        HideMessage();
        Unsubscribe();
    }
}
