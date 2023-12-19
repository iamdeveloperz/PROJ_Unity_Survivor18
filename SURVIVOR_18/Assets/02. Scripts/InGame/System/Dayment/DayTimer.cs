
using System;
using System.Collections;
using UnityEngine;

public class DayTimer
{
    #region  Member Variables

    [Header("Timer")]
    // 게임 시간과 실제 시간의 타이머 배수 (초 단위)
    // ex) `30`일 경우 `게임 하루`는 실제 시간 30초
    private readonly float _dayLengthSeconds = Literals.DAY_LENGTH_SECONDS;
    
    private float _startHour; // by. 희성 => 임시용. 나중에 시간도 저장이 된다면 필요 없어짐.
    
    private TimeSpan _currentGameTime;
    private TimeSpan _currentRealTime;
    
    /* Events */
    public event Action<TimeSpan> OnGameTimeChanged;
    public event Action<TimeSpan> OnRealTimeChanged;

    #endregion



    #region Properties

    private float MinuteLength => _dayLengthSeconds / Literals.DAY_PER_MINUTES;

    #endregion



    #region Initialize => Inner Methods

    private void SetupStartHour(float startHour)
    {
        if (startHour is < 0 or > 24)
        {
            Debug.LogWarning($"Wrong start hour setup : {startHour}");
            Debug.LogWarning("Initialize start hour from '8'");
            _startHour = 8.0f;
            return;
        }

        _startHour = startHour;
    }

    private void SetupCurrentGameTimeFromStartHour()
    {
        _currentGameTime += TimeSpan.FromHours(_startHour);
    }

    #endregion
    
    

    #region Public Methods

    /* Initialize */
    public void Initialize(float startHour)
    {
        SetupStartHour(startHour);
        SetupCurrentGameTimeFromStartHour();
    }
    

    public void StartGameAndRealTimers()
    {
        CoroutineManagement.Instance.StartManagedCoroutine(UpdateGameTimeAsync());
        CoroutineManagement.Instance.StartManagedCoroutine(UpdateRealTimeAsync());
    }

    #endregion



    #region Coroutines

    private IEnumerator UpdateGameTimeAsync()
    {
        float elapsedTime = 0f;
        
        while (true)
        {
            elapsedTime += Time.fixedDeltaTime;

            if (elapsedTime >= MinuteLength)
            {
                _currentGameTime += TimeSpan.FromMinutes(1);
                
                OnGameTimeChanged?.Invoke(_currentGameTime);

                elapsedTime -= MinuteLength;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator UpdateRealTimeAsync()
    {
        while (true)
        {
            _currentRealTime += TimeSpan.FromSeconds(1);
            
            OnRealTimeChanged?.Invoke(_currentRealTime);

            yield return new WaitForSeconds(1);
        }
    }

    #endregion
    
}
