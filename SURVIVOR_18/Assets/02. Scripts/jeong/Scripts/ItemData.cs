using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Resource,
    Equipable,
    Consumable
}

[CreateAssetMenu(fileName = "Item", menuName = "new item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    public string itemInfo;
    public ItemType type;
    public Sprite itemImage;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;
}
