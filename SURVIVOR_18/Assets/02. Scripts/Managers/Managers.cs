
public class Managers : SingletonBehavior<Managers>
{
    #region Manager Variables

    private readonly GameManager _gameManager = new();

    #endregion



    #region Manager Properties

    public static GameManager Game => Instance._gameManager;

    #endregion
}
