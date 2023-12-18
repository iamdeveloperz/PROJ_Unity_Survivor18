using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData itemData;

    public string GetInteractPrompt()
    {
        return string.Format("Pickup {0}", itemData.name);
    }

    public void OnInteract()
    {
        Destroy(gameObject);
    }
}
