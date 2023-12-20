
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    #region Member Variables

    // Sort Order
    private int _order = 10;
    
    // Popup Management
    private Stack<UIPopup> _popups = new Stack<UIPopup>();
    
    // Scenes Overlay
    private UIScene _scene;

    #endregion



    #region Properties => Set Root UI

    public GameObject Root
    {
        get
        {
            var root = GameObject.Find("@UI_Root");
            
            if(root == null)
            {
                root = new GameObject { name = "@UI_Root" };
            }

            return root;
        }
    }

    #endregion



    #region Scene UI

    public T ShowSceneUI<T>(string name = null) where T : UIScene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        var gameObject = Managers.Resource.Instantiate(name, Literals.PATH_UI);
        var sceneUI = Utility.GetOrAddComponent<T>(gameObject);
        
        gameObject.transform.SetParent(Root.transform);

        _scene = sceneUI;

        return sceneUI;
    }

    #endregion



    #region Popup UI

    public T ShowPopupUI<T>(string name = null) where T : UIPopup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        
        var gameObject = Managers.Resource.Instantiate(name, Literals.PATH_UI);
        var popupUI = Utility.GetOrAddComponent<T>(gameObject);
        
        gameObject.transform.SetParent(Root.transform);
        
        _popups.Push(popupUI);

        return popupUI;
    }

    public void ClosePopupUI(UIPopup popup)
    {
        if (_popups.Count == 0) return;

        if (_popups.Peek() != popup)
        {
            Debug.LogWarning("Close Popup failed");
            return;
        }
        
        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popups.Count == 0) return;

        var popup = _popups.Pop();
        
        ResourceManager.Destroy(popup.gameObject);

        _order -= 1;
    }

    public void CloseAllPopupUI()
    {
        while(_popups.Count > 0)
            ClosePopupUI();
    }

    #endregion



    #region Setup Canvas

    public void SetCanvas(GameObject go, bool sorting = true)
    {
        var canvas = Utility.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        var uiScales = Utility.GetOrAddComponent<CanvasScaler>(go);
        uiScales.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        var resolution = new Vector2(Literals.SCREEN_X, Literals.SCREEN_Y);
        uiScales.referenceResolution = resolution;
        uiScales.matchWidthOrHeight = Literals.MATCH_WIDTH;

        if(sorting)
        {
            _order += 1;
            canvas.sortingOrder = _order;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    #endregion
}
