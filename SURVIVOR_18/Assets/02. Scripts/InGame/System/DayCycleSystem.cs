
using System;
using UnityEngine;
using Random = System.Random;

public class DayCycleSystem : MonoBehaviour
{
    #region Member Variables

    private Transform _dailyRotationRoot;

    /* Timer */
    private DayTimer _dayTimer;

    /* Timer Setup */
    // 임시 테스트용으로 변경해보면 시작 시간이 변경 됩니다. [by. 희성]
    [SerializeField] private float _startHour = 12f;
    
    /* Sun Time */
    private DayLight _dayLight;

    #endregion
    



    #region Behavior

    private void Start()
    {
        Initialize();
        StartTimer();
    }

    private void OnDisable()
    {
        if (_dayTimer != null)
        {
            _dayTimer.OnGameTimeChanged -= OnGameTimeChanged;
            _dayTimer.OnRealTimeChanged -= OnRealTimeChanged;
        }
    }

    #endregion



    #region Initialize

    private void Initialize()
    {
        _dayTimer = new DayTimer();
        _dayTimer.Initialize(_startHour);

        _dayLight = new DayLight();
        _dayLight.Initialize(transform);
        
        // Day Timer Event Subscribe.
        _dayTimer.OnGameTimeChanged += OnGameTimeChanged;
        _dayTimer.OnRealTimeChanged += OnRealTimeChanged;
    }

    #endregion



    #region Timer

    private void StartTimer()
    {
        _dayTimer.StartGameAndRealTimers();
        
        _dayLight.StartSunRotate();
    }
    
    /* Events */
    private void OnGameTimeChanged(TimeSpan gameTime)
    {
        Managers.Game.SetupGameTime(gameTime);
    }

    private void OnRealTimeChanged(TimeSpan realTime)
    {
        Managers.Game.SetupRealTime(realTime);
    }

    #endregion



    #region Coroutine

    

    #endregion
}
