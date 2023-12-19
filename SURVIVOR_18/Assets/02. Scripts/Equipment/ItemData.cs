using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DefaultItemData", menuName ="New Item/Default", order= 0)]
public class ItemData : ScriptableObject
{
    [Header("Default Setting")]
    public string displayName;
    public string description;
    public Sprite icon;
    public EquipableParts equipPart;
    public bool canStack;
    public int maxStackCount;
    public GameObject dropPrefab;
}

public class RegistableItem : ItemData
{
    public RegistableItem()
    {
        equipPart = EquipableParts.Hand;
    }
}

[CreateAssetMenu(fileName = "EquipableItemData", menuName = "New Item/HandleItem", order = 1)]
public class HandleItem : RegistableItem
{
    [Header("Interact Setting")]
    public float attackPower;
    public float range;
    public LayerMask targetLayer;
    public string target;
    public HandableType type; // Weapon Type // Gear?

    public HandleItem() : base()
    {
        canStack = false;
        maxStackCount = 1;
    }
}

[CreateAssetMenu(fileName = "EquipableItemData", menuName = "New Item/ConsumableItem", order = 2)]
public class ConsumableItem : RegistableItem
{
    public ConsumableItem() : base()
    {
        canStack = true;
    }
}

public class UnRegistableItem : ItemData
{
    public UnRegistableItem()
    {
        
    }
}

[CreateAssetMenu(fileName = "EquipableItemData", menuName = "New Item/IngredientItem", order = 3)]
public class IngredientItem : UnRegistableItem
{
    public IngredientItem()
    {
        canStack = true;
    }
}

[CreateAssetMenu(fileName = "EquipableItemData", menuName = "New Item/WearableItem", order = 4)]
public  class WearableItem : UnRegistableItem
{
    public WearableItem()
    {
        canStack = false;
        maxStackCount = 1;
    }
}

public class EquipItemData : ItemData
{
    public EquipableParts equipPart;
}

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

public class WearableItemData : EquipItemData
{
    public float deffend;

    public WearableItemData()
    {
        equipPart = EquipableParts.Head;
    }
}
public class EatableItemData : ItemData
{

}