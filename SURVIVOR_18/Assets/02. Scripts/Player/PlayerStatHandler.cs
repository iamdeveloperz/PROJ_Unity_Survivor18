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

    private void Awake()
    {
        HP.Init();
        Hunger.Init();
        Moisture.Init();
        Stamina.Init();

        Hunger.OnBelowedToZero += (() => { StartCoroutine(HitPerSec()); });
        Hunger.OnRecovered += ((x) => { StopCoroutine(HitPerSec()); });
        // Do it : Condition UI Load
    }

    private void Update()
    {
        // CheckConditionRejenable();
        _isHungerEnough = Hunger.curValue != 0;
        _isMoistureEnough = Moisture.curValue != 0;

        // RejenCondition();
        HP.Add(HP.regenRate * Time.deltaTime * Convert.ToInt32(_isHungerEnough));
        Stamina.Add(Stamina.regenRate * Time.deltaTime * Convert.ToInt32(_isMoistureEnough));

        // DecayCondition();
        Hunger.Subtract(Hunger.decayRate * Time.deltaTime);
        Moisture.Subtract(Moisture.decayRate * Time.deltaTime);

        if(true /* is sprint */)
        {
            Stamina.Subtract(Stamina.decayRate * Time.deltaTime);
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
}
