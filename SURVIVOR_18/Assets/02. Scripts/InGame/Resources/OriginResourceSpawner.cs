
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OriginResourceSpawner : MonoBehaviour
{
    #region Member Variables

    private List<OriginResourceConfig> _originResourceConfigs;
    private List<Transform> _spawnPoints;

    public Transform SpawnPointGroup;

    #endregion

    
    
    #region Behavior

    private void Start()
    {
        _originResourceConfigs = new List<OriginResourceConfig>();

        Initialize();
    }

    #endregion



    #region Initialize

    private void Initialize()
    {
        LoadOriginResourceConfig();
        SpawnGroupChild();
        InitSpawnOriginResources();
    }
    
    private void LoadOriginResourceConfig()
    {
        var configs = (Managers.Data.GetOriginResourceConfigs());
        
        foreach (var config in configs)
        {
            _originResourceConfigs.Add(config);
        }
    }

    private void SpawnGroupChild()
    {
        _spawnPoints ??= new List<Transform>();

        for (var i = 0; i < SpawnPointGroup.childCount; ++i)
        {
            _spawnPoints.Add(SpawnPointGroup.GetChild(i));
        }
    }

    private void InitSpawnOriginResources()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            // 원래는 이 형태로 사용해야 하지만 현재 Rock이 없기 때문에 주석 처리 [by. 희성]
             var type = (ResourceType)Random.Range(0, Enum.GetValues(typeof(ResourceType)).Length);
            //var type = ResourceType.Tree;
            SpawnOriginResource(type, spawnPoint);
        }
    }

    #endregion



    #region Main Methods

    private void SpawnOriginResource(ResourceType type, Transform spawnPoint)
    {
        var config = _originResourceConfigs.Find(config =>
            config.ResourceType == type);

        if (config == null || config.ModelPrefab == null) return;
        
        // 추 후에 부모 Root를 지정해주는 것도 나쁘지 않아보임 [By. 희성]
        var go = Instantiate(config.ModelPrefab, spawnPoint.position, Quaternion.identity);
        
        OriginResourcesSetup(config, go, type);
    }

    #endregion



    #region Sub Methods

    private void OriginResourcesSetup(OriginResourceConfig config, GameObject originObject, ResourceType type)
    {
        var originResource = originObject.AddComponent<OriginResource>();

        InitOriginResource(config, originResource, type);
    }

    private void InitOriginResource(OriginResourceConfig config, OriginResource originResource, ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Tree:
                originResource.Initialize(4, 1, 4, TimeSpan.FromSeconds(5f), config);
                break;
            case ResourceType.Rock:
                originResource.Initialize(8, 3, 6, TimeSpan.FromSeconds(10f), config);
                break;
        }
    }

    #endregion
}