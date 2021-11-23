using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Item")]
public class Item : ScriptableObject
{
    public string Name = "New Item";
    public Sprite Icon = null;

    public virtual void Use()
    {
        Debug.Log("Using" + Name);
    }
}
