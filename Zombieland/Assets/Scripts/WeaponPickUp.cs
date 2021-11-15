using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField] private GameObject _weapon;
    private void OnTriggerEnter(Collider other)
    {
        var player=other.GetComponent<Player>();

        if (player != null)
        {
            player.EquipWeapon(_weapon);
        }
    }
}
