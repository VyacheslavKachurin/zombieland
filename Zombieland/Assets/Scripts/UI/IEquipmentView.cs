using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipmentView
{
    public void EquipItem(Item newItem);

    public void GetInventoryModel(InventoryModel model);

}
