using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftPopupUI : MonoBehaviour
{
    [SerializeField] private List<Button> _createButtons;
    [SerializeField] private List<TextMeshProUGUI> _ownWoodNumTexts;
    [SerializeField] private List<TextMeshProUGUI> _ownRockNumTexts;

    [SerializeField] private ItemData _woodData;
    [SerializeField] private ItemData _rockData;
    [SerializeField] private List<RegistableItemData> _weaponDatas;

    private int _ownWoodNum = 0;
    private int _ownRockNum = 0;

    private int _woodIndexInInventory = -1;
    private int _rockIndexInInventory = -1;

    private void OnEnable()
    {
        UpdateCraftPopup();
    }

    private void UpdateCraftPopup()
    {
        _ownWoodNum = 0;
        _ownRockNum = 0;

        for (int i = 0; i < Inventory.Instance.itemSlots.Length; ++i)
        {
            ItemSlot slot = Inventory.Instance.itemSlots[i];
            if (slot.item == null) continue;
            
            
            if (slot.item.name == Literals.WOOD)
            {
                _ownWoodNum = slot.quantity;
                _woodIndexInInventory = i;
            }
            if (slot.item.name == Literals.ROCK)
            {
                _ownRockNum = slot.quantity;
                _rockIndexInInventory = i;
            }
        }

        for(int i = 0; i< _weaponDatas.Count; ++i)
        {
            _ownWoodNumTexts[i].text = _ownWoodNum.ToString() + " / " + _weaponDatas[i].requiredWoodNum.ToString();
            _ownRockNumTexts[i].text = _ownRockNum.ToString() + " / " + _weaponDatas[i].requiredRockNum.ToString();

            _createButtons[i].enabled
               = (_ownWoodNum >= _weaponDatas[i].requiredWoodNum
               && _ownRockNum >= _weaponDatas[i].requiredRockNum);
        }
    }

    private void ConsumeItemInInventory(int index)
    {
        if (_woodIndexInInventory >= 0)
            Inventory.Instance.Consumeitem(_woodIndexInInventory, _weaponDatas[index].requiredWoodNum);
        if (_rockIndexInInventory >= 0)
            Inventory.Instance.Consumeitem(_rockIndexInInventory, _weaponDatas[index].requiredRockNum);
        Inventory.Instance.AddItem(_weaponDatas[index]);
    }

    #region Craft Button

    // TODO
    public void CreateWeapon1()
    {
        ConsumeItemInInventory(0);
        UpdateCraftPopup();
    }

    public void CreateWeapon2()
    {
        ConsumeItemInInventory(1);
        UpdateCraftPopup();
    }

    public void CreateWeapon3()
    {
        ConsumeItemInInventory(2);
        UpdateCraftPopup();
    }
    #endregion
}
