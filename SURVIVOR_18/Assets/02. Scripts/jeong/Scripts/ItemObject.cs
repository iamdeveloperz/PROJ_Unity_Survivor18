using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData itemData;

    public string GetName()
    {
        return itemData.name;
    }

    public void ObjectDestroy()
    {
        Destroy(gameObject);
    }
}
