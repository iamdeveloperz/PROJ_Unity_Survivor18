using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public ItemData item; //아이템
    public Image itemImage;  // 아이템의 이미지
    public Button itemSelectBtn;  // 아이템 선택버튼

    // 인벤토리에 새로운 아이템 슬롯 추가
    public void AddItem(ItemData _item)
    {
        item = _item;
        if (item != null)
        {
            itemImage.sprite = item.itemImage;
        }
    }


    // 해당 슬롯 하나 삭제
    private void ClearSlot()
    {
        item = null;
        itemImage.sprite = null;
    }
}
