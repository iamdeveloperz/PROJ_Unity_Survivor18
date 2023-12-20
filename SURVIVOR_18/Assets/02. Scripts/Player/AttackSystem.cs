using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    private Animator _animator;
    private PlayerInputs _playerInputs;
    public float attackDelay = 1.0f;
    private float _attackDelayTimer = 0.0f;
    private Camera _camera;
    private QuickSlotSystem _quickSlotSystem;
    public LayerMask targetLayer;
    private int _attackAnimation;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInputs = GetComponent<PlayerInputs>();
        _camera = Camera.main;
        _quickSlotSystem = GetComponent<QuickSlotSystem>();
    }

    void Start()
    {
        _attackAnimation = Animator.StringToHash("Attack");
        _attackDelayTimer = attackDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerInputs.DoSomething) return;

        var handleItemData = _quickSlotSystem.HandleItem.itemData as HandleItemData;
        if (handleItemData == null || handleItemData.type == HandableType.EmptyHand) return;

        _attackDelayTimer += Time.deltaTime;
        if (_attackDelayTimer < attackDelay) return;
        
        if(_playerInputs.attack)
        { 
            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, handleItemData.range, targetLayer))
            {
                float weight = handleItemData.type == HandableType.Weapon ? 1.0f : 0.5f;
                hit.collider.gameObject.GetComponent<IHitable>()?.Hit(handleItemData.attackPower * weight);
            }

            // 중복되는 구간. 별도로 뺄 수 있는지 고민하기.
            RuntimeAnimatorController ac = _animator.runtimeAnimatorController;
            for (int i = 0; i < ac.animationClips.Length; ++i)
            {
                if (handleItemData.type.ToString() == ac.animationClips[i].name)
                {
                    float time = ac.animationClips[i].length;
                    CoroutineManagement.Instance.StartManagedCoroutine(LockAdditionalInput(time));
                }
            }

            _attackDelayTimer = 0.0f;
            _animator.SetTrigger(_attackAnimation);
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
