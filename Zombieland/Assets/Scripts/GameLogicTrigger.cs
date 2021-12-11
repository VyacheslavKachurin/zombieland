using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameLogicTrigger : MonoBehaviour
{
    public event Action PlayerEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerEntered?.Invoke();
        }
    }
}
