
public static class Literals
{
    #region Day System

    public const int DAY_PER_SECONDS = 86400;
    public const int DAY_PER_MINUTES = DAY_PER_SECONDS / 60;
    public const int DAY_PER_HOUR = DAY_PER_MINUTES / 60;

    public const int DAY_LENGTH_SECONDS = 20;

    #endregion



    #region UI Resolution

    public const float SCREEN_X = 1920f;
    public const float SCREEN_Y = 1080f;

    public const float MATCH_WIDTH = 0.33f;

    #endregion
    
    

    #region Resources Path

    public const string PATH_UI = "Prefabs/UI/";
    public const string PATH_INIT = "Prefabs/InitOnLoad/";
    public const string PATH_RESOURCEMODEL = "Prefabs/OriginResourceModel/";
    public const string PATH_ITEM = "Prefabs/Item/";
    public const string PATH_HANDABLE = "Prefabs/Handable/";
    public const string PATH_PLAYER = "Prefabs/Player/";
    public const string PATH_STRUCTURE = "Prefabs/Structure/";
    // public const string PATH_SO = "ScriptableObject/";

    #endregion

    

    #region Resources - UI

    public const string UI_MAINGAME_Scene_Main = "Scene_UI_MainGame";

    #endregion



    #region Craft

    public const string WOOD = "TreeData";
    public const string ROCK = "RockData";

    #endregion
}