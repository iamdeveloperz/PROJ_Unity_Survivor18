using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipableItemData", menuName = "New Item/WearableItem", order = 4)]
public class WearableItemData : UnRegistableItemData
{
    public WearableItemData()
    {
        canStack = false;
        maxStackCount = 1;
    }
}

