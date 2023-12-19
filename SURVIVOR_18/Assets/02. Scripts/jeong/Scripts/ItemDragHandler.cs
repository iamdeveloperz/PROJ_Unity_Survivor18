using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ItemType
{
    Resource,
    Equipable,
    Consumable
}

//[CreateAssetMenu(fileName = "Item", menuName = "new item")]
//public class ItemData : ScriptableObject
//{
//    [Header("Info")]
//    public string itemName;
//    public string itemInfo;
//    public ItemType type;
//    public Sprite itemImage;
//    public GameObject dropPrefab;

//    [Header("Stacking")]
//    public bool canStack;
//    public int maxStackAmount;
//}
public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그 시작 시 위치와 부모를 저장하고, 레이캐스트를 차단합니다.
        rectTransform.SetParent(rectTransform.parent.parent);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 드래그 중에는 아이템을 마우스 위치로 이동합니다.
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그 종료 시 부모를 원래의 슬롯으로 되돌리고, 레이캐스트를 활성화합니다.
        rectTransform.SetParent(transform.parent);
        rectTransform.localPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
    }

    public void DropItemToSlot(Slot slot)
    {
        // 드롭한 아이템을 슬롯에 추가하거나 교환 등의 동작을 구현합니다.
        // Inventory 클래스의 메서드를 호출하여 아이템을 처리할 수 있습니다.
        Inventory.Instance.HandleDroppedItem(slot, GetComponent<ItemData>());
    }
}
