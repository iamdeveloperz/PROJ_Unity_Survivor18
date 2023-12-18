using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerConditionsUI : MonoBehaviour
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

        SetHPbar(playerStatHandler);
        SetHungerBar(playerStatHandler);
        SetMoistureBar(playerStatHandler);
        SetStaminaBar(playerStatHandler);
    }

    private void SetHPbar(PlayerStatHandler playerStatHandler)
    {
        _hpUI.SetMaximum((int)playerStatHandler.HP.maxValue);
        playerStatHandler.HP.OnUpdated = _hpUI.UpdateBar;
    }

    private void SetHungerBar(PlayerStatHandler playerStatHandler)
    {
        _hungerUI.SetMaximum((int)playerStatHandler.Hunger.maxValue);
        playerStatHandler.Hunger.OnUpdated = _hungerUI.UpdateBar;
    }

    private void SetMoistureBar(PlayerStatHandler playerStatHandler)
    {
        _moistureUI.SetMaximum((int)playerStatHandler.Moisture.maxValue);
        playerStatHandler.Moisture.OnUpdated = _moistureUI.UpdateBar;
        playerStatHandler.Moisture.OnBelowedToZero += (() => { staminaBlock.SetActive(true); });
        playerStatHandler.Moisture.OnRecovered += ((x) => { staminaBlock.SetActive(false); });
    }

    private void SetStaminaBar(PlayerStatHandler playerStatHandler)
    {
        _staminaUI.SetMaximum((int)playerStatHandler.Stamina.maxValue);
        playerStatHandler.Stamina.OnUpdated = _staminaUI.UpdateBar;
    }
}
