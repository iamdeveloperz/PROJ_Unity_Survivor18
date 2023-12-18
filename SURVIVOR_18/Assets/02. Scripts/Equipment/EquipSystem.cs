using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ������ ������ ����
public enum EquipableParts
{
    Head,
    Hand,
    Max
}

public enum HandableType // GrabType
{
    None,
    Weapon,
    Axe,
    Pick,
}

public class EquipSystem : MonoBehaviour
{
    // �տ� ��� �ִ� ������ ����
    [field:SerializeField] public GrabbableItem HandleItem { get; private set; }

    public GameObject[] items;
    public Transform hand;
    private PlayerInputs _playerInputs;

    private void Awake()
    {
        items = new GameObject[5];
        for(int i = 0; i <  items.Length; ++i)
        {
            items[i] = CreateItemObject("EmptyHand");
            items[i].SetActive(false);
        }

        _playerInputs = GetComponent<PlayerInputs>();
        _playerInputs.OnPressedQuickNumber += SwitchingHandleItem;


        items[0] = CreateItemObject("Pick");
        SwitchingHandleItem(1);
    }
    // ������ ���� ��ġ�� ������ ������
    // 

    private GameObject CreateItemObject(string itemName)
    {
        var go = Instantiate(Resources.Load<GameObject>($"Prefabs/{itemName}"));
        var tempTransform = go.transform;
        go.transform.parent = hand;
        go.transform.localPosition = tempTransform.position;
        go.transform.localRotation = tempTransform.rotation;
        return go;
    }

    private void SwitchingHandleItem(int index)
    {
        int n = index - 1;
        for(int i = 0; i < items.Length;++i)
        {
            if(i == n)
            {
                items[i].SetActive(true);
                HandleItem = items[i].GetComponent<GrabbableItem>();
            }
            else
            {
                items[i].SetActive(false);
            }
        }
    }

    // ���� �Լ�
    public void Registe(int index, Item item)
    {
        // index ���Կ� item ���

        // GameObject �� Resources.Load

        // 
    }

    // ���� �Լ�
    public void UnRegiste(int index)
    {

    }
}
