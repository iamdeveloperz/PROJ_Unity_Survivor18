
using UnityEngine;

public class MainGameScene : MonoBehaviour
{
    private void Start()
    {
        Managers.UI.ShowSceneUI<UIMainGame>(Literals.UI_MAINGAME_Scene_Main);
    }
}
