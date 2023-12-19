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
    [SerializeField] private GameObject go_SlotsParent;  // Slot���� �θ��� Grid Setting 
    [SerializeField] private Slot[] slots;  // ���Ե� �迭
    public TextMeshProUGUI itemName;  // ���Ե� �迭
    public TextMeshProUGUI itemInfo;  // ���Ե� �迭

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
    public void CreateSlotItem() //���� ������ �ִ� ������ ���� �� Null
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
        if (item.canStack) //�������� �����̸�
        {
            if (items.Find(itemSlot => itemSlot.item == item) == null)
            {
                items.Add(new ItemSlot(item, 0));
            }
            for (int i = 0; i < items.Count; i++) //���� ���� �ִ� �������̶� ����
            {
                if (items[i].item.name == item.name) //�������� ������ ���� ����
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
