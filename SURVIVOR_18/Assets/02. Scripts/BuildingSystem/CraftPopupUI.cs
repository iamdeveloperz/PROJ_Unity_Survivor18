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
    [SerializeField] private ItemData _axeData;
    //[SerializeField] private ItemData _swordData;

    private int _ownWoodNum = 0;
    private int _ownRockNum = 0;

    // TODO
    private int _axeRequiredWoodNum = 1;
    private int _axeRequiredRockNum = 0;
    private int _swordRequiredWoodNum = 0;
    private int _swordRequiredRockNum = 0;

    private void OnEnable()
    {
        UpdateWoodRockNum();
        EnabledButtons();
    }

    private void UpdateWoodRockNum()
    {
        _ownWoodNum = 0;
        _ownRockNum = 0;

        for (int i = 0; i < Inventory.Instance.items.Count; ++i)
        {
            ItemSlot slot = Inventory.Instance.items[i];
            if (slot.item.name == "Wood")
                _ownWoodNum = slot.quantity;
            if (slot.item.name == "Rock")
                _ownRockNum = slot.quantity;
        }

        //TODO
        foreach (TextMeshProUGUI _text in _ownWoodNumTexts)
            _text.text = _ownWoodNum.ToString() + " / " + _axeRequiredWoodNum.ToString();
        foreach (TextMeshProUGUI _text in _ownRockNumTexts)
            _text.text = _ownRockNum.ToString() + " / " + _axeRequiredRockNum.ToString();
    }

    private void EnabledButtons()
    {
        _createButtons[0].enabled = (_ownWoodNum >= _axeRequiredWoodNum && _ownRockNum >= _axeRequiredRockNum);
        _createButtons[1].enabled = (_ownWoodNum >= _swordRequiredWoodNum && _ownRockNum >= _swordRequiredRockNum);
    }

    #region Craft Button

    public void CreateAxe()
    {
        Inventory.Instance.SubtractItem(_woodData, _axeRequiredWoodNum);
        Inventory.Instance.SubtractItem(_rockData, _axeRequiredRockNum);
        Inventory.Instance.AddItem(_axeData);

        UpdateWoodRockNum();
        EnabledButtons();
    }

    public void CreateSword()
    {
        //Inventory.Instance.SubtractItem(_woodData, _swordRequiredWoodNum);
        //Inventory.Instance.SubtractItem(_rockData, _swordRequiredRockNum);
        //Inventory.Instance.AddItem(_swordData);
    }
    #endregion
}
