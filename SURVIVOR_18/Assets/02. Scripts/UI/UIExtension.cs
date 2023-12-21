
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public static class UIExtension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        return Utility.GetOrAddComponent<T>(go);
    }

    public static void BindEvent(this GameObject go, Action<PointerEventData> action, UIBase.UIEvents type = UIBase.UIEvents.Click)
    {
        UIBase.BindEvent(go, action, type);
    }
}
