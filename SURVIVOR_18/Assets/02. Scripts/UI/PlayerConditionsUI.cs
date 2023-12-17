using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConditionsUI : MonoBehaviour
{
    private ConditionUI _hpUI;
    private ConditionUI _hungerUI;
    private ConditionUI _moistureUI;
    private ConditionUI _staminaUI;

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
        _staminaUI.SetMaximum((int)playerStatHandler.Stamina.maxValue);
        playerStatHandler.Stamina.OnUpdated = _staminaUI.UpdateBar;
    }
}
