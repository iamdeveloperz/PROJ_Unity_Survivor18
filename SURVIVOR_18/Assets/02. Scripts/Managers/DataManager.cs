
using System;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    #region Data Member

    // Origin Resource Configs
    private Dictionary<ResourceType, OriginResourceConfig> _originResourceConfigs;

    public bool IsComplete { get; private set; } = false;

    #endregion



    #region Initialize

    public void Initialize()
    {
        InitOriginResourceConfig();

        IsComplete = true;
    }

    #endregion



    #region Resource Config Generate

    private void InitOriginResourceConfig()
    {
        _originResourceConfigs = new Dictionary<ResourceType, OriginResourceConfig>();

        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            var prefab = Managers.Resource.GetPrefab(type.ToString(), Literals.PATH_RESOURCEMODEL);
            var getData = (ItemData)Managers.Resource.GetScriptableObject(type.ToString() + "Data");

            var config = new OriginResourceConfig
            {
                ResourceType = type,
                ModelPrefab = prefab,
                GetResourceItem = getData
            };

            _originResourceConfigs[type] = config;
        }
    }

    #endregion



    #region Getter

    public OriginResourceConfig GetOriginResourceConfig(ResourceType type)
    {
        if (_originResourceConfigs.TryGetValue(type, out var originResourceConfig))
        {
            return originResourceConfig;
        }
        
        Debug.LogError($"ResourceConfig for type {type} not found.");
        return null;
    }
    
    public OriginResourceConfig[] GetOriginResourceConfigs()
    {
        return new List<OriginResourceConfig>(_originResourceConfigs.Values).ToArray();
    }

    #endregion
}