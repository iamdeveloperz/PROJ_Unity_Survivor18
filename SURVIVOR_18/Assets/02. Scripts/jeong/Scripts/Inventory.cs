using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemData> items;
    [SerializeField]
    private GameObject go_SlotsParent;  // Slot���� �θ��� Grid Setting 

    private Slot[] slots;  // ���Ե� �迭

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        CreateSlotItem(items); //������ �ʱ�ȭ
    }
    public void CreateSlotItem(List<ItemData> item) //���� ������ �ִ� ������ ���� �� ����
    {
        int i = 0;

        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].AddItem(item[i]);
        }
        for (; i < slots.Length; i++)
        {
            slots[i].AddItem(null);
        }
    }
}
