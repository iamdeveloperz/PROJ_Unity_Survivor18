using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : StarterAssetsInputs
{
    public bool attack;
    public bool interact;
    public event Action<int> OnPressedQuickNumber;

    private void Awake()
    {
        var _playerStamina = GetComponent<PlayerStatHandler>().Stamina;
        _playerStamina.OnBelowedToZero += StopSprint;
    }

#if ENABLE_INPUT_SYSTEM
    public void OnAttack(InputValue value)
    {
        // 들고있는 아이템 체크
        AttackInput(value.isPressed);
    }

    public void OnInteract(InputValue value)
    {
        InteractInput(value.isPressed);
    }

    public void OnQuickSlot(InputValue value)
    {
        string strNumber = value.Get().ToString();
        int num = int.Parse(strNumber);
        if(num != 0)
        {
            QuickSlotInput(num);
        }
    }

#endif 

    public void AttackInput(bool newAttackState)
    {
        attack = newAttackState;
    }

    public void StopSprint()
    {
        sprint = false;
    }

    public void InteractInput(bool newInteractState)
    {
        interact = newInteractState;
    }

    public void QuickSlotInput(int newQuickSlot)
    {

        OnPressedQuickNumber?.Invoke(newQuickSlot);
    }
}
