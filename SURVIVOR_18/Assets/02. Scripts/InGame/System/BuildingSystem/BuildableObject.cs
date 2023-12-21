using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildableObject : MonoBehaviour
{
    //[SerializeField] private int _buildingHealth = 1;
    [SerializeField] private Renderer _renderer;
    private NavMeshObstacle _navMeshObstacle;

    private Material _originMat;
    private BuildableObjectColliderManager _colliderManager;

    private int buildingLayer = 30;

    void Awake()
    {
        _originMat = new Material(_renderer.material);
        _colliderManager = GetComponentInChildren<BuildableObjectColliderManager>();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
    }

    void Update()
    {
        
    }

    public void SetInitialObject()
    {
        SetOriginMaterial();

        gameObject.layer = buildingLayer;
        for (int i = 0; i < transform.childCount; ++i)
            transform.GetChild(i).gameObject.layer = buildingLayer;

        _navMeshObstacle.enabled = true;
    }

    public void SetMaterial(Material material)
    {
        _renderer.material = material;
    }

    public Material GetMaterial()
    {
        return _originMat;
    }

    public void SetOriginMaterial()
    {
        _renderer.material = _originMat;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void DestroyColliderManager()
    {
        Destroy(_colliderManager);
    }
}
