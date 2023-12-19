using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipableItemData", menuName = "New Item/IngredientItem", order = 3)]
public class IngredientItemData : UnRegistableItemData
{
    public IngredientItemData()
    {
        canStack = true;
    }
}