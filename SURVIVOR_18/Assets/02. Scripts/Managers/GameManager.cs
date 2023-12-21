
using System;

public class GameManager
{
    #region Member Variables

    /* Timer Variables */
    private TimeSpan _gameTime;
    private TimeSpan _realTime;
    private float _timeOfDay;
    
    #endregion
    


    #region Properties

    /* Timers Getter */
    public TimeSpan GameTime => _gameTime;
    public TimeSpan RealTime => _realTime;

    public float TimeOfDay
    {
        get
        {
            _timeOfDay = (float)(_gameTime.TotalMinutes / Literals.DAY_PER_MINUTES) % 1.0f;
            return _timeOfDay;
        }
    }
    
    /* InGame State */
    public InGameStatement InGameState { get; set; }

    #endregion



    #region Setter

    /* Timer Setup Events */
    public void SetupGameTime(TimeSpan gameTime)
    {
        _gameTime = gameTime;
    }

    public void SetupRealTime(TimeSpan realTime)
    {
        _realTime = realTime;
    }

    #endregion
}