
public class UIMainGame : UIScene
{
    #region Enums

    enum Texts
    {
        GameTimerText,
    }

    enum GameObjects
    {
        HPBar,
        HungerBar,
        MoistureBar,
        StaminaBar
    }

    #endregion



    #region Initialize

    private void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        
        //Bind<>();
    }

    #endregion
}
