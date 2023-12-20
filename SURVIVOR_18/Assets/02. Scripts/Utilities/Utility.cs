
using System.Linq;
using UnityEngine;

public class Utility : MonoBehaviour
{
    #region Find Child => To Component
    
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : Object
    {
        if (go == null)
            return null;

        // 전부 LINQ형식으로 변경 [By. 희성]
        var components = go.GetComponentsInChildren<T>(true);   // 비활성화 오브젝트 포함

        if (!recursive)
        {
            // recursive가 false일 경우에는 전체가 아닌 직속 자식만을 필터링
            components = components.Where(comp =>
            {
                var component = comp as Component;
                return component != null && component.transform.parent == go.transform;
            }).ToArray();
        }

        return components.FirstOrDefault(comp =>
            string.IsNullOrEmpty(name) || comp.name == name);
    }
    
    #endregion



    #region Find Child => To GameObject
    
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        var transform = FindChild<Transform>(go, name, recursive);
        
        return (transform == null) ? null : transform.gameObject;
    }
    
    #endregion




    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        var component = go.GetComponent<T>();

        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }



    public static int GetRandomNumber(int min, int max)
    {
        var random = new System.Random();
        max += 1;

        return random.Next(min, max);
    }

}
