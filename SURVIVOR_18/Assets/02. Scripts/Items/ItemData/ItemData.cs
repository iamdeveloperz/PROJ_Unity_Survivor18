using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    useItem,
    equipItem,
    etcItem,
}
[CreateAssetMenu(fileName ="DefaultItemData", menuName ="New Item/Default", order= 0)]
public class ItemData : ScriptableObject
{
    [Header("Default Setting")]
    // [ 만약 아이템의 종류가 다양해진다면 ]
    // public string searchName; 
    public string displayName;
    public string description;
    public Sprite icon;
    public EquipableParts equipPart;
    public ItemType type; 
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

public class UnRegistableItemData : ItemData
{
    public UnRegistableItemData()
    {
        
    }
}