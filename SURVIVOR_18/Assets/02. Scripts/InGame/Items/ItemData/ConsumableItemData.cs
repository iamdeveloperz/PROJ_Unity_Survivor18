using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipableItemData", menuName = "New Item/ConsumableItem", order = 2)]
public class ConsumableItemData : RegistableItemData
{
    public ItemDataConsumable[] consumables;
    public ConsumableItemData() : base()
    {
        canStack = true;
        handType = HandableType.EmptyHand;
    }
}
