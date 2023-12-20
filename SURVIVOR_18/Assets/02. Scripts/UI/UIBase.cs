using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public abstract class UIBase : MonoBehaviour
{
    // 명시적으로 상속받는 자식들이 구현해야 되는 부분
    public abstract void Initialize();


    #region Enum Events

    public enum UIEvents
    {
        Click,
        PointerDown,
        PointerUp,
        PointerEnter,
        PointerExit,
        Drag,
        BeginDrag,
        EndDrag
    }

    public enum MouseEvents
    {
        Click,
        PointerDown,
        PointerUp,
        PointerEnter,
        Drag,
        BeginDrag,
        EndDrag
    }

    #endregion

    

    #region Bind Member Dictionary

    // 바인딩할 오브젝트들을 담을 딕셔너리
    protected Dictionary<Type, Object[]> _bindObjects = new Dictionary<Type, Object[]>();

    #endregion



    #region Bind Object Properties

    /// <summary>
    /// # Generic 타입의 Getter
    /// </summary>
    /// <param name="idx">Enum Type</param>
    protected T Get<T>(int idx) where T : Object
    {
        if (_bindObjects.TryGetValue(typeof(T), out var objects) == false)
            return null;

        return objects[idx] as T;
    }
    
    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    protected TMP_Text GetText(int idx) { return Get<TMP_Text>(idx); }
    protected TMP_InputField GetInputField(int idx) { return Get<TMP_InputField>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }

    #endregion



    #region Binding
    
    protected void Bind<T>(Type type) where T : Object
    {
        var objectNames = Enum.GetNames(type);
        var objects = new Object[objectNames.Length];

        _bindObjects.Add(typeof(T), objects);

        for(var idx = 0; idx < objects.Length; ++idx)
        {
            if (typeof(T) == typeof(GameObject))
                objects[idx] = Utility.FindChild(gameObject, objectNames[idx], true);
            else
                objects[idx] = Utility.FindChild<T>(gameObject, objectNames[idx], true);

            if (objects[idx] == null)
                Debug.LogError(message: $"Failed to binding ({objectNames[idx]})");
        }
    }

    protected void BindObject(Type type) => Bind<GameObject>(type);
    protected void BindText(Type type) => Bind<TMP_Text>(type);
    protected void BindInputField(Type type) => Bind<TMP_InputField>(type);
    protected void BindButton(Type type) => Bind<Button>(type);
    protected void BindImage(Type type) => Bind<Image>(type);

    #endregion
    
    
    
    #region Bind Events
    
    public static void BindEvent(GameObject go, Action<PointerEventData> action, UIEvents type = UIEvents.Click)
    {
        var evtHandler = Utility.GetOrAddComponent<UIEventHandler>(go);
        
        switch(type)
        {
            case UIEvents.Click:
                evtHandler.OnClickEvent -= action;
                evtHandler.OnClickEvent += action;
                break;
            case UIEvents.PointerDown:
                evtHandler.OnPointerDownEvent -= action;
                evtHandler.OnPointerDownEvent += action;
                break;
            case UIEvents.PointerUp:
                evtHandler.OnPointerUpEvent -= action;
                evtHandler.OnPointerUpEvent += action;
                break;
            case UIEvents.PointerEnter:
                evtHandler.OnPointerEnterEvent -= action;
                evtHandler.OnPointerEnterEvent += action;
                break;
            case UIEvents.PointerExit:
                evtHandler.OnPointerExitEvent -= action;
                evtHandler.OnPointerExitEvent += action;
                break;
            case UIEvents.Drag:
                evtHandler.OnDragEvent -= action;
                evtHandler.OnDragEvent += action;
                break;
            case UIEvents.BeginDrag:
                evtHandler.OnBeginDragEvent -= action;
                evtHandler.OnBeginDragEvent += action;
                break;
            case UIEvents.EndDrag:
                evtHandler.OnEndDragEvent -= action;
                evtHandler.OnEndDragEvent += action;
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
    
    #endregion
}
