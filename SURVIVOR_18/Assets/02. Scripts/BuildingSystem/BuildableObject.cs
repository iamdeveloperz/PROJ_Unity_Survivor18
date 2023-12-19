using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableObject : MonoBehaviour
{
    //[SerializeField] private int _buildingHealth = 1;
    [SerializeField] private Renderer _renderer;

    private Material _originMat;
    private BuildableObjectColliderManager _colliderManager;

    void Awake()
    {
        _originMat = new Material(_renderer.material);
        _colliderManager = GetComponentInChildren<BuildableObjectColliderManager>();
    }

    void Update()
    {
        
    }

    public void SetInitialObject()
    {
        SetOriginMaterial();

        // TODO
        gameObject.layer = 30;
        for (int i = 0; i < transform.childCount; ++i)
            transform.GetChild(i).gameObject.layer = 30;
        //obj.layer = LayerMask.GetMask("Building");
        //obj.transform.GetChild(0).gameObject.layer = groundLayer.value;
    }

    public void SetMaterial(Material material)
    {
        _renderer.material = material;
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
