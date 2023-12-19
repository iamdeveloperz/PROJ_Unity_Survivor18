using System;
using System.Collections;
using System.Collections.Generic;
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
    Weapon,
    Axe,
    Pick,
}

public class QuickSlotSystem : MonoBehaviour
{
    // 손에 들고 있는 아이템 정보
    [field:SerializeField] public RegistableItem HandleItem { get; private set; }

    public GameObject[] items;
    public Transform hand;
    private PlayerInputs _playerInputs;
    public RegistableItemData tempItemData;
    public event Action OnRegisted;
    public event Action OnUnRegisted;

    private void Awake()
    {
        items = new GameObject[5];
        for(int i = 0; i <  items.Length; ++i)
        {
            items[i] = CreateItemObject("EmptyHand");
            items[i].SetActive(false);
        }

        _playerInputs = GetComponent<PlayerInputs>();
        _playerInputs.OnPressedQuickNumber += OperatorQuickSlot;


        Registe(0, tempItemData);
        OperatorQuickSlot(1);
    }

    private GameObject CreateItemObject(string itemName)
    {
        var go = Instantiate(Resources.Load<GameObject>($"Prefabs/{itemName}"));
        var tempTransform = go.transform;
        go.transform.parent = hand;
        go.transform.localPosition = tempTransform.position;
        go.transform.localRotation = tempTransform.rotation;
        return go;
    }

    private void OperatorQuickSlot(int index)
    {
        var data = items[index].GetComponent<RegistableItem>().itemData;
        switch (data)
        {
            case ConsumableItemData _:
                //items[index]
                break;

            case HandleItemData _:
                SwitchingHandleItem(index);
                break;
        }
    }

    private void SwitchingHandleItem(int index)
    {
        int n = index - 1;
        for(int i = 0; i < items.Length;++i)
        {
            if(i == n)
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

    // 착용 함수
    public void Registe(int index, RegistableItemData itemData)
    {
        // index 슬롯에 item 등록
        UnRegiste(index);
        if(itemData is HandleItemData)
        {
            var itemdata = itemData as HandleItemData;
            var itemObject = CreateItemObject(itemdata.type.ToString());
            items[index] = itemObject;
        }
    }

    // 해제 함수
    public void UnRegiste(int index)
    {
        Destroy(items[index]);
        items[index] = null;
    }
}
