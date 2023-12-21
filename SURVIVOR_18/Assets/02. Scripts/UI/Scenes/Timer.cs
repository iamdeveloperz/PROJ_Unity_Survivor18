using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;

    private void Awake()
    {
        _timerText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        // 게임 시간을 문자열로 변환
        string gameTimeString = Managers.Game.GameTime.ToString("hh\\:mm");

        // 텍스트 컴포넌트에 시간 표시
        _timerText.text = gameTimeString;
    }
}
