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
    [SerializeField] private GameObject go_SlotsParent;  // Slot들의 부모인 Grid Setting 
    [SerializeField] private Slot[] slots;  // 슬롯들 배열
    public TextMeshProUGUI itemName;  // 슬롯들 배열
    public TextMeshProUGUI itemInfo;  // 슬롯들 배열

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
    public void CreateSlotItem() //현재 가지고 있는 아이템 생성 및 Null
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
        if (item.canStack) //아이템이 수량이면
        {
            if (items.Find(itemSlot => itemSlot.item == item) == null)
            {
                items.Add(new ItemSlot(item, 0));
            }
            for (int i = 0; i < items.Count; i++) //현재 들어와 있는 아이템이랑 대조
            {
                if (items[i].item.name == item.name) //아이템이 맞으면 수량 증가
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
