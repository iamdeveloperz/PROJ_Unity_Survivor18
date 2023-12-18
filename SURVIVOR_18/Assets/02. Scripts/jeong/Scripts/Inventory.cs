using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Inventory : MonoBehaviour
{
    
    [SerializeField]
    private GameObject go_SlotsParent;  // Slot���� �θ��� Grid Setting 

    [SerializeField]
    private Slot[] slots;  // ���Ե� �迭

    [SerializeField]
    private List<ItemData> items;

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }
    private void OnEnable()
    {
        CreateSlotItem();
    }
    public void CreateSlotItem() //���� ������ �ִ� ������ ���� �� ����
    {
        int i = 0;

        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].AddItem(items[i]);
        }
        for (; i < slots.Length; i++)
        {
            slots[i].AddItem(null);
        }
    }
}
