using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipableItemData", menuName = "New Item/Equipable", order = 1)]
public class EquipItemData : ItemData
{
    public EquipableParts equipPart;    
}

[CreateAssetMenu(fileName = "EquipableItemData", menuName = "New Item/Equipable/Grabble", order = 2)]
public class GrabbleItemData : EquipItemData
{
    public float attackPower;
    public float range;
    public LayerMask targetLayer;
    public string target;
    public HandableType type;

    public GrabbleItemData()
    {
        equipPart = EquipableParts.Hand;
    }
}

[CreateAssetMenu(fileName = "EquipableItemData", menuName = "New Item/Equipable/Wearable", order = 3)]
public class WearableItemData : EquipItemData
{
    public float deffend;

    public WearableItemData()
    {
        equipPart = EquipableParts.Head;
    }
}