using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : StarterAssetsInputs
{
    public bool attack;
    public float delay;

    private void Awake()
    {
        var _playerStamina = GetComponent<PlayerStatHandler>().Stamina;
        _playerStamina.OnBelowedToZero += StopSprint;
    }

#if ENABLE_INPUT_SYSTEM
    public void OnUse(InputValue value)
    {
        // 들고있는 아이템 체크
        AttackInput(value.isPressed);
    }

#endif 

    public void AttackInput(bool newAttackState)
    {

    }

    public void StopSprint()
    {
        sprint = false;
    }
}
