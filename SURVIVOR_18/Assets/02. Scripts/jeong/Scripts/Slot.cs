using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public ItemData item; //������
    public Image itemImage;  // �������� �̹���
    public Button itemSelectBtn;  // ������ ���ù�ư

    // �κ��丮�� ���ο� ������ ���� �߰�
    public void AddItem(ItemData _item)
    {
        item = _item;
        if (item != null)
        {
            itemImage.sprite = item.itemImage;
        }
    }


    // �ش� ���� �ϳ� ����
    private void ClearSlot()
    {
        item = null;
        itemImage.sprite = null;
    }
}
