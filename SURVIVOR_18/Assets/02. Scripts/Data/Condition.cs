using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

[System.Serializable]
public class Condition
{
    public float curValue;
    public float maxValue;
    public float regenRate;
    public float decayRate;

    public Action<float> OnRecovered;
    public Action<float> OnDecreased;
    public Action<float> OnUpdated;
    public Action OnBelowedToZero;

    public void Init()
    {
        curValue = maxValue;
    }

    public void Add(float amount)
    {
        if (curValue >= maxValue) return;

        curValue = Mathf.Min(curValue + amount, maxValue);
        OnUpdated?.Invoke(GetPercentage());
    }

    public void Subtract(float amount)
    {
        if (curValue == 0) return;

        curValue = Mathf.Max(curValue - amount, 0.0f);
        OnUpdated?.Invoke(GetPercentage());
        if(curValue == 0)
        {
            OnBelowedToZero?.Invoke();
        }
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}
