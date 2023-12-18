using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableObject : MonoBehaviour
{
    public event Action OnRedMatAction;
    public event Action OnBluePrintMatAction;
    
    [SerializeField] private Material _redMat;
    [SerializeField] private Material _bluePrintMat;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Collider _thisObjectAnotherColldier;

    private void OnTriggerEnter(Collider other)
    {
        if (other != _thisObjectAnotherColldier)
        {
            _renderer.material = _redMat;
            OnRedMatAction?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other != _thisObjectAnotherColldier)
        {
            _renderer.material = _redMat;
            OnRedMatAction?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _renderer.material = _bluePrintMat;
        OnBluePrintMatAction?.Invoke();
    }
}
