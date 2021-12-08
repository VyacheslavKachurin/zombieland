using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryView 
{
    public void GetEquipmentModel(EquipmentModel model);

    public void GetInventoryModel(InventoryModel model);

    public void TogglePanel();
}
