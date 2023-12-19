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

public class RegistableItemData : ItemData
{
    public RegistableItemData()
    {
        equipPart = EquipableParts.Hand;
    }
}

[CreateAssetMenu(fileName = "EquipableItemData", menuName = "New Item/HandleItem", order = 1)]
public class HandleItemData : RegistableItemData
{
    [Header("Interact Setting")]
    public float attackPower;
    public float range;
    public LayerMask targetLayer;
    public string target;
    public HandableType type; // Weapon Type // Gear?

    public HandleItemData() : base()
    {
        canStack = false;
        maxStackCount = 1;
    }
}

[CreateAssetMenu(fileName = "EquipableItemData", menuName = "New Item/ConsumableItem", order = 2)]
public class ConsumableItemData : RegistableItemData
{
    public ConsumableItemData() : base()
    {
        canStack = true;
    }
}

public class UnRegistableItemData : ItemData
{
    public UnRegistableItemData()
    {
        
    }
}

[CreateAssetMenu(fileName = "EquipableItemData", menuName = "New Item/IngredientItem", order = 3)]
public class IngredientItemData : UnRegistableItemData
{
    public IngredientItemData()
    {
        canStack = true;
    }
}

[CreateAssetMenu(fileName = "EquipableItemData", menuName = "New Item/WearableItem", order = 4)]
public  class WearableItemData : UnRegistableItemData
{
    public WearableItemData()
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

public class EatableItemData : ItemData
{

}