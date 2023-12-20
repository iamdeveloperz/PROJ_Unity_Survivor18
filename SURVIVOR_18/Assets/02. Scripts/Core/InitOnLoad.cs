
using UnityEngine;

public static class InitOnLoad 
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitApplication()
    {
        if (!InitManagers())
        {
            Debug.LogWarning("Data or Resource Can't Complete process.");
            return;
        }

        var prefabs = Managers.Resource.GetPrefabs(Literals.PATH_INIT);

        if (prefabs.Length <= 0) return;
        
        foreach (var prefab in prefabs)
        {
            var gameObject = Object.Instantiate(prefab);
            
            gameObject.name = prefab.name;
            
            Object.DontDestroyOnLoad(gameObject);
        }
    }

    private static bool InitManagers()
    {
        var isComplete = false;
        
        if (!Managers.Resource.IsLoaded)
        {
            Managers.Resource.LoadAllPrefabs();
        }
        
        if (!Managers.Data.IsComplete)
        {
            Managers.Data.Initialize();
        }

        if (Managers.Resource.IsLoaded && Managers.Data.IsComplete)
        {
            isComplete = true;
        }

        return isComplete;
    }
}
