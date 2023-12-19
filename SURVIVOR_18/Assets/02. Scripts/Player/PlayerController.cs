using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _attackDelay = 1.0f;
    private float _attackDelayTimer = 0.0f;
    private Animator _animator;
    private PlayerInputs _playerInputs;
    private Camera _camera;
    private EquipSystem _equipSystem;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInputs = GetComponent<PlayerInputs>();
        _camera = Camera.main;
        _equipSystem = GetComponent<EquipSystem>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        var handleItemData = _equipSystem.HandleItem.itemData as HandleItemData;
        _attackDelayTimer += Time.deltaTime;
        if (_attackDelayTimer < _attackDelay) return;
                
        if (_playerInputs.interact)
        {
            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, handleItemData.range, handleItemData.targetLayer))
            {                
                if(hit.collider.CompareTag(handleItemData.target))
                {
                    Debug.Log($"{hit.collider.gameObject.name}");
                    _attackDelayTimer = 0.0f;
                    _animator.SetTrigger(handleItemData.type.ToString());
                    hit.collider.gameObject.GetComponent<IHitable>()?.Hit(handleItemData.attackPower);
                }                
            }
        }
    }
}
