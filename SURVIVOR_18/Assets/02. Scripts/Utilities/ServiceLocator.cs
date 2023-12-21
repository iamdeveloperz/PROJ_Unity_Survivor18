
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public static class ServiceLocator
{
    /// <summary>
    /// # 서비스를 들고 있는 Key-Value Dict
    /// </summary>
    private static readonly Dictionary<Type, Object> _services =
        new Dictionary<Type, Object>();


    
    
    #region Services

    /// <summary>
    /// # 서비스 등록
    /// => 전역에서 해당 컴포넌트에 접근할 수 있게 한다.
    /// </summary>
    /// <typeparam name="T">필요로 하는 스크립트(컴포넌트)</typeparam>
    public static void RegisterService<T>(T service) where T : Object
    {
        if (service == null) return;
        if (_services.ContainsKey(service.GetType()))
        {
            Debug.LogError($"Service Type {service.GetType()} is already register.");
            return;
        }

        _services[service.GetType()] = service;
    }
    
    
    
    /// <summary>
    /// # 서비스 제공
    /// </summary>
    public static T GetService<T>() where T : Object
    {
        if (_services.TryGetValue(typeof(T), out Object service))
        {
            return service as T;
        }

        return null;
    }

    #endregion

}