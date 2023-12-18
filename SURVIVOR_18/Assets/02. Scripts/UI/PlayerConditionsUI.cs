using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerConditionsUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private ConditionUI _hpUI;
    private ConditionUI _hungerUI;
    private ConditionUI _moistureUI;
    private ConditionUI _staminaUI;
    public GameObject staminaBlock;

    private void Awake()
    {
        _hpUI = transform.Find("HP").GetComponent<ConditionUI>();
        _hungerUI = transform.Find("Hunger").GetComponent<ConditionUI>();
        _moistureUI = transform.Find("Moisture").GetComponent<ConditionUI>();
        _staminaUI = transform.Find("Stamina").GetComponent<ConditionUI>();

        var playerStatHandler = GameObject.Find("Player").GetComponent<PlayerStatHandler>();
        _hpUI.SetMaximum((int)playerStatHandler.HP.maxValue);
        playerStatHandler.HP.OnUpdated = _hpUI.UpdateBar;
        _hungerUI.SetMaximum((int)playerStatHandler.Hunger.maxValue);
        playerStatHandler.Hunger.OnUpdated = _hungerUI.UpdateBar;
        _moistureUI.SetMaximum((int)playerStatHandler.Moisture.maxValue);
        playerStatHandler.Moisture.OnUpdated = _moistureUI.UpdateBar;
        playerStatHandler.Moisture.OnBelowedToZero += (() => { staminaBlock.SetActive(true); });
        playerStatHandler.Moisture.OnRecovered += ((x) => { staminaBlock.SetActive(false); });
        _staminaUI.SetMaximum((int)playerStatHandler.Stamina.maxValue);
        playerStatHandler.Stamina.OnUpdated = _staminaUI.UpdateBar;        
    }

    public void OnBeginDrag(PointerEventData ped)
    {
        Debug.Log("OnBeginDrag");
        var go = Instantiate(Resources.Load<GameObject>("UI/Image"));
        go.transform.SetParent(GameObject.Find("Canvas").transform);
    }

    public void OnDrag(PointerEventData ped)
    {
        Debug.Log("OnDrag");

    }

    public void OnEndDrag(PointerEventData ped)
    {
        Debug.Log("OnEndDrag");

    }

    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown");

    }

    private void OnMouseDrag()
    {
        
    }

    private void OnMouseUp()
    {
        
    }
}
