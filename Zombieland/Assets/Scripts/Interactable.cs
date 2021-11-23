using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] public float _radius = 3f;
    private IPlayerInput _input;

    public abstract void Interact();

    public void ShowMessage()
    {
        Debug.Log("Press F to interact");

    }

    public void HideMessage()
    {
        Debug.Log("dont press f");
    }

    public void Subscribe()
    {
        _input.InteractButtonPressed += Interact;
        Debug.Log("subscribed");
    }

    public void Unsubscribe()
    {
        _input.InteractButtonPressed -= Interact;
        Debug.Log("unsubcribed");
    }

    private void OnTriggerEnter(Collider other)
    {
        _input = other.gameObject.GetComponent<Player>().Input;

        ShowMessage();
        Subscribe();
    }

    private void OnTriggerExit(Collider other)
    {
        HideMessage();
        Unsubscribe();
    }
}
