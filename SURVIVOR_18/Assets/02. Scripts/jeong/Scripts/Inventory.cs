using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using static UnityEditor.Progress;

[Serializable]
public class ItemSlot
{
    public ItemData item;
    public int quantity;

    public ItemSlot(ItemData item,int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}
public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject go_SlotsParent; 
    [SerializeField] private Slot[] slots;  
    public GameObject itemName;  
    public GameObject itemInfo;
    public List<ItemSlot> items;

    public static Inventory Instance;
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }
    private void OnEnable()
    {
        CreateSlotItem();
    }
    private void OnDisable()
    {
        if (itemName.activeSelf)
        {
            itemName.SetActive(false);
            itemInfo.SetActive(false);
        }
    }
    public void itemText(string itemNameText, string itemInfoText)
    {
        itemName.SetActive(true);
        itemInfo.SetActive(true);
        itemName.GetComponent<TextMeshProUGUI>().text = itemNameText;
        itemInfo.GetComponent<TextMeshProUGUI>().text = itemInfoText;
    }
    public void CreateSlotItem() 
    {
        int i = 0;
        if (items != null)
        {
            for (; i < slots.Length && i < items.Count; i++)
            {
                slots[i].AddItem(items[i]);
            }
        }
        for (; i < slots.Length; i++)
        {
            slots[i].AddItem(null);
        }
    }
    public void AddItem(ItemData item)
    {
        if (item.canStack)
        {
            if (items.Find(itemSlot => itemSlot.item == item) == null)
            {
                items.Add(new ItemSlot(item, 0));
            }
            for (int i = 0; i < items.Count; i++) 
            {
                if (items[i].item.name == item.name) 
                {
                    items[i].quantity++;
                    slots[i].CheckQuantity(items[i]);
                }
            }
        }
        else
        {
            items.Add(new ItemSlot(item, 0));
        }
    }
}
