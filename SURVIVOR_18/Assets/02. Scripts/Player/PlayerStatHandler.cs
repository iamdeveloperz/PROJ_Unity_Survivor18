using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHandler : MonoBehaviour, IHitable
{
    [field:Header("Conditions")]
    [field: SerializeField]
    public Condition HP { get; private set; }
    [field: SerializeField]
    public Condition Hunger { get; private set; }
    [field: SerializeField]
    public Condition Moisture { get; private set; }
    [field: SerializeField]
    public Condition Stamina { get; private set; }

    [Header("IsEnough")]
    [SerializeField] private bool _isHungerEnough;
    [SerializeField] private bool _isMoistureEnough;
    private float _limittedStamina = 70.0f;

    private StarterAssetsInputs _input;

    private void Awake()
    {
        HP.Init();
        Hunger.Init();
        Moisture.Init();
        Stamina.Init();

        Hunger.OnBelowedToZero += (() => { StartCoroutine(HitPerSec()); });
        Hunger.OnRecovered += ((x) => { StopCoroutine(HitPerSec()); });

        Moisture.OnBelowedToZero += LimitMaxStamina;

        _input = GetComponent<StarterAssetsInputs>();

        // Do it : Condition UI Load
    }

    private void Update()
    {
        // CheckConditionRejenable();
        _isHungerEnough = Hunger.curValue != 0;
        _isMoistureEnough = Moisture.curValue != 0;

        ControlHP();
        ControlHunger();
        ControlMoisture();
        ControlStemina();
    }

    private void ControlHP()
    {
        RecoveryHPbyTime();
    }

    private void ControlHunger()
    {
        DigestFood();
    }

    private void ControlMoisture()
    {
        Sweat();
    }

    private void ControlStemina()
    {
        ConsumeStamina();
        RecoeryStaminaAtTime();
    }

    private void RecoveryHPbyTime()
    {
        if(_isHungerEnough)
        {
            HP.Add(HP.regenRate * Time.deltaTime);
        }
    }
    
    private void DigestFood()
    {
        Hunger.Subtract(Hunger.decayRate * Time.deltaTime);
    }

    private void Sweat()
    {
        Moisture.Subtract(Moisture.decayRate * Time.deltaTime);
    }

    private void ConsumeStamina()
    {
        if (_input.sprint)
        {
            Stamina.Subtract(Stamina.decayRate * Time.deltaTime);
        }
    }

    private void RecoeryStaminaAtTime()
    {
        if (!_input.sprint)
        {
            if(_isMoistureEnough)
            {
                Stamina.Add(Stamina.regenRate * Time.deltaTime);
            }
            else
            {
                if(Stamina.curValue <= _limittedStamina)
                    Stamina.Add(Stamina.regenRate * Time.deltaTime);
            }
        }
    }

    IEnumerator HitPerSec()
    {
        while(true)
        {
            yield return new WaitForSeconds(2.0f);
            Hit(HP.decayRate);
        }
    }

    public void Eat(float amount)
    {
        Hunger.Add(amount);
    }

    public void Recover(float amount)
    {
        HP.Add(amount);
    }

    public void Drink(float amount)
    {
        Moisture.Add(amount);
    }

    public void Hit(float amount)
    {
        HP.Subtract(amount);
    }

    private void LimitMaxStamina()
    {
        Stamina.curValue = Stamina.curValue > _limittedStamina ? _limittedStamina : Stamina.curValue;
    }
}