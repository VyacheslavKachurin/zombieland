using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable
{
    public override void Interact()
    {
        Debug.Log("Interacted");
        Unsubscribe();
        PickUp();
    }

    private void PickUp()
    {
        Destroy(gameObject);
    }
}
