using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Inventory : MonoBehaviour
{
    
    [SerializeField]
    private GameObject go_SlotsParent;  // Slot들의 부모인 Grid Setting 

    [SerializeField]
    private Slot[] slots;  // 슬롯들 배열

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
    public void CreateSlotItem() //현재 가지고 있는 아이템 생성 및 공백
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
