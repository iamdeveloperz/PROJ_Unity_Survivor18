using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableObjectColliderManager : MonoBehaviour
{
    public event Action OnRedMatAction;
    public event Action OnBluePrintMatAction;
    
    [SerializeField] private Material _redMat;
    [SerializeField] private Material _bluePrintMat;
    [SerializeField] private Renderer _thisObjectRenderer;
    [SerializeField] private Collider _thisObjectAnotherCollider;

    private void OnTriggerStay(Collider other)
    {
        if (other != _thisObjectAnotherCollider)
        {
            _thisObjectRenderer.material = _redMat;
            OnRedMatAction?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _thisObjectRenderer.material = _bluePrintMat;
        OnBluePrintMatAction?.Invoke();
    }
}
