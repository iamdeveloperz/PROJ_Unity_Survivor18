using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData itemData;

    public string GetName()
    {
        return itemData.displayName;
    }

    public void ObjectDestroy()
    {
        if (Inventory.Instance != null && itemData != null)
        {
            Inventory.Instance.AddItem(itemData);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Inventory.Instance or itemData is null. Object destruction aborted.");
        }
    }
}
