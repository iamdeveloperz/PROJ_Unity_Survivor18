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
        // �巡�� ���� �� ��ġ�� �θ� �����ϰ�, ����ĳ��Ʈ�� �����մϴ�.
        rectTransform.SetParent(rectTransform.parent.parent);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // �巡�� �߿��� �������� ���콺 ��ġ�� �̵��մϴ�.
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // �巡�� ���� �� �θ� ������ �������� �ǵ�����, ����ĳ��Ʈ�� Ȱ��ȭ�մϴ�.
        rectTransform.SetParent(transform.parent);
        rectTransform.localPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
    }

    public void DropItemToSlot(Slot slot)
    {
        // ����� �������� ���Կ� �߰��ϰų� ��ȯ ���� ������ �����մϴ�.
        // Inventory Ŭ������ �޼��带 ȣ���Ͽ� �������� ó���� �� �ֽ��ϴ�.
        Inventory.Instance.HandleDroppedItem(slot, GetComponent<ItemData>());
    }
}
