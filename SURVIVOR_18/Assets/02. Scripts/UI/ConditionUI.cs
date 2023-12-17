using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : MonoBehaviour
{
    private Slider _bar;
    private Image _icon;
    private TextMeshProUGUI _text;
    private int _max;

    private void Awake()
    {
        _bar = transform.Find("Bar").GetComponent<Slider>();
        _icon = transform.Find("Icon").GetComponent<Image>();
        _text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    public void SetMaximum(int max)
    {
        _max = max;
    }

    public void UpdateBar(float percent)
    {        
        _bar.value = percent;
        _text.text = $"{(int)Mathf.Round(_max * percent)}";
    }
}
