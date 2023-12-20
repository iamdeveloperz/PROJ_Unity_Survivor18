using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceGathering : MonoBehaviour
{
    private float _attackDelay = 1.0f;
    private float _attackDelayTimer = 0.0f;
    private Animator _animator;
    private PlayerInputs _playerInputs;
    private Camera _camera;
    private QuickSlotSystem _quickSlotSystem;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInputs = GetComponent<PlayerInputs>();
        _camera = Camera.main;
        _quickSlotSystem = GetComponent<QuickSlotSystem>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        var handleItemData = _quickSlotSystem.HandleItem.itemData as HandleItemData;

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

                    RuntimeAnimatorController ac = _animator.runtimeAnimatorController;
                    for(int i = 0; i < ac.animationClips.Length; ++i)
                    {
                        if(handleItemData.type.ToString() == ac.animationClips[i].name)
                        {
                            float time = ac.animationClips[i].length;
                            CoroutineManagement.Instance.StartManagedCoroutine(LockAdditionalInput(time));
                        }
                    }

                    hit.collider.gameObject.GetComponent<IHitable>()?.Hit(handleItemData.attackPower);
                }                
            }
        }
    }

    IEnumerator LockAdditionalInput(float time)
    {
        _playerInputs.DoSomething = true;
        time *= 0.7f;
        Debug.Log("ani time " + time);        
        yield return new WaitForSeconds(time);
        _playerInputs.DoSomething = false;
    }
}
