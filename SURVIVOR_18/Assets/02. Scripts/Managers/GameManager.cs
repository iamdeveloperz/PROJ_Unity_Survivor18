
using System;

public class GameManager
{
    #region Member Variables

    /* Timer Variables */
    private TimeSpan _gameTime;
    private TimeSpan _realTime;
    
    #endregion
    


    #region Properties

    /* Timers Getter */
    public TimeSpan GameTime => _gameTime;
    public TimeSpan RealTime => _realTime;
    
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