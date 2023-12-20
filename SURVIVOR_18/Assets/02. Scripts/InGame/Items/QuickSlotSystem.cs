using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 착용 가능한 아이템 부위
public enum EquipableParts
{
    Head,
    Hand,
    Max
}

public enum HandableType // GrabType
{
    EmptyHand,
    Sword,
    Axe,
    Pick,
    Building,
    Food,
}

public class QuickSlotSystem : MonoBehaviour
{
    // 손에 들고 있는 아이템 정보
    [field:SerializeField] public RegistableItem HandleItem { get; private set; }

    public GameObject[] items;
    public Transform hand;
    private PlayerInputs _playerInputs;
    public RegistableItemData tempItemData;
    private int _selectedIndex = 1;
    private int[] _targetIndexs;

    public GameObject root_QuickSlots;
    [SerializeField] private QuickSlotUI[] _quickSlotUis;

    private void Awake()
    {
        items = new GameObject[5];
        _targetIndexs = new int[5];
        if (root_QuickSlots == null) Debug.Log("QuickSlotSystem >> root_QuickSlots [ IS NULL ]");
        _quickSlotUis = root_QuickSlots.GetComponentsInChildren<QuickSlotUI>();

        for (int i = 0; i <  items.Length; ++i)
        {
            items[i] = CreateItemObject("EmptyHand");
            items[i].SetActive(false);
        }

        _playerInputs = GetComponent<PlayerInputs>();
        _playerInputs.OnPressedQuickNumber += OperatorQuickSlot;

        //Registe(0, tempItemData);
        _selectedIndex = 1;
        OperatorQuickSlot(_selectedIndex);
    }

    private GameObject CreateItemObject(string itemName)
    {
        var go = Managers.Resource.Instantiate(itemName, Literals.PATH_HANDABLE);
        var tempTransform = go.transform;
        go.transform.parent = hand;
        go.transform.localPosition = tempTransform.position;
        go.transform.localRotation = tempTransform.rotation;
        return go;
    }

    private void OperatorQuickSlot(int index)
    {
        _selectedIndex = index - 1;
        var data = items[_selectedIndex].GetComponent<RegistableItem>().itemData;
        switch (data)
        {
            case ConsumableItemData _:
                Inventory.Instance.UseItem(_targetIndexs[_selectedIndex]);
                if(Inventory.Instance.itemSlots[_targetIndexs[_selectedIndex]].quantity == 0)
                {
                    _quickSlotUis[_selectedIndex].Clear();
                    UnRegiste(_selectedIndex);
                    items[_selectedIndex] = CreateItemObject("EmptyHand");
                }
                break;

            case HandleItemData _:
                SwitchingHandleItem();
                break;
        }
    }

    private void SwitchingHandleItem()
    {
        Debug.Log("Switching " + _selectedIndex);
        for(int i = 0; i < items.Length;++i)
        {
            if(i == _selectedIndex)
            {
                items[i].SetActive(true);
                HandleItem = items[i].GetComponent<RegistableItem>();
            }
            else
            {
                items[i].SetActive(false);
            }
        }
    }

    public void Registe(int index, ItemData itemData, int targetIndex)
    {
        UnRegiste(index);
        _targetIndexs[index] = targetIndex;
        Registe(index, itemData);
    }

    public void Registe(int index, ItemData itemData)
    {
        if(itemData is RegistableItemData)
        {
            Debug.Log("Registe");            
            var registableItemData = itemData as RegistableItemData;
            Registe(index, registableItemData);
        }

        SwitchingHandleItem();
    }

    // 착용 함수
    public void Registe(int index, RegistableItemData itemData)
    {
        // index 슬롯에 item 등록        
        if(itemData is HandleItemData)
        {
            var itemdata = itemData as HandleItemData;
            var itemObject = CreateItemObject(itemdata.searchName);
            items[index] = itemObject;
        }
        else if(itemData is ConsumableItemData)
        {
            var itemdata = itemData as ConsumableItemData;
            var itemObject = CreateItemObject(itemdata.searchName);
            items[index] = itemObject;
        }
    }

    // 해제 함수
    public void UnRegiste(int index)
    {
        Destroy(items[index]);
        _targetIndexs[index] = -1;
        items[index] = null;
    }
}
