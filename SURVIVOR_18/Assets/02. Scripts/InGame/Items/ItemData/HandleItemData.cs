using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipableItemData", menuName = "New Item/HandleItem", order = 1)]
public class HandleItemData : RegistableItemData
{
    [Header("Interact Setting")]
    public float attackPower;
    public float range;
    public LayerMask targetLayer;
    public string target;
    public HandableType handType; // Weapon Type // Gear?

    public HandleItemData() : base()
    {
        canStack = false;
        maxStackCount = 1;
    }
}
