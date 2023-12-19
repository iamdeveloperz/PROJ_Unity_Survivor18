
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OriginResourceSpawner : MonoBehaviour
{
    #region Member Variables

    private List<OriginResourceConfig> _originResourceConfigs;
    [SerializeField] private Transform[] _spawnPoints;

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
        InitSpawnOriginResources();
    }

    private void InitSpawnOriginResources()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            // 원래는 이 형태로 사용해야 하지만 현재 Rock이 없기 때문에 주석 처리 [by. 희성]
            // var type = (ResourceType)Random.Range(0, Enum.GetValues(typeof(ResourceType)).Length);
            var type = ResourceType.Tree;
            SpawnOriginResource(type, spawnPoint);
        }
    }

    #endregion



    #region Main Methods
    
    private void LoadOriginResourceConfig()
    {
        var configs = (Managers.Data.GetOriginResourceConfigs());
        
        foreach (var config in configs)
        {
            _originResourceConfigs.Add(config);
        }
    }

    private void SpawnOriginResource(ResourceType type, Transform spawnPoint)
    {
        var config = _originResourceConfigs.Find(config =>
            config.ResourceType == type);

        if (config == null || config.ModelPrefab == null) return;
        
        // 추 후에 부모 Root를 지정해주는 것도 나쁘지 않아보임 [By. 희성]
        var gameObject = Instantiate(config.ModelPrefab, spawnPoint.position, Quaternion.identity);
        
        OriginResourcesSetup(config, gameObject, type);
    }

    #endregion



    #region Sub Methods

    private void OriginResourcesSetup(OriginResourceConfig config, GameObject originObject, ResourceType type)
    {
        var originResource = gameObject.AddComponent<OriginResource>();
        var initAmount = DetermineInitAmount(type);
        
        originResource.InitAmount(initAmount);
        originResource.InitConfig(config);
    }

    private int DetermineInitAmount(ResourceType type) => type switch
    {
        // 임시적으로 리터럴을 사용 하겠습니다. [By. 희성]
        ResourceType.Tree => 4,
        ResourceType.Rock => 8,
        _ => 1
    };

    #endregion
}