using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : StarterAssetsInputs
{
    public bool attack;
    public bool interact;
    public event Action<int> OnPressedQuickNumber;
    
    [SerializeField] private bool _doSomething;
    public bool DoSomething
    {
        get 
        {
            return _doSomething;
        }
        set
        {
            _doSomething = value;
            //GetComponent<PlayerInput>().enabled = !_doSomething;
            //move = Vector2.zero;
            GetComponent<ThirdPersonController>().enabled = !_doSomething;
        }
    }

    public event Action OnCreateBluePrintAction;
    public event Action OnInstallArchitectureAction;
    public event Action OnRotateArchitectureLeftAction;
    public event Action OnRotateArchitectureRightAction;
    public event Action OnCancelBuildModeAction;
    public event Action OnBreakModeAction;
    public event Action OnBreakArchitectureAction;

    private void Awake()
    {
        var _playerStamina = GetComponent<PlayerStatHandler>().Stamina;
        _playerStamina.OnBelowedToZero += StopSprint;
    }

#if ENABLE_INPUT_SYSTEM
    public void OnAttack(InputValue value)
    {
        AttackInput(value.isPressed);
    }

    public void OnInteract(InputValue value)
    {
        InteractInput(value.isPressed);
    }

    public void OnQuickSlot(InputValue value)
    {
        string strNumber = value.Get().ToString();
        Debug.Log("Press" + strNumber);
        int num = int.Parse(strNumber);
        if (num != 0)
        {
            QuickSlotInput(num);
        }
    }

    #region Building System

    public void OnCreateBluePrintArchitecture()
    {
        OnCreateBluePrintAction?.Invoke();
    }

    public void OnRotateArchitectureLeft()
    {
        OnRotateArchitectureLeftAction?.Invoke();
    }

    public void OnRotateArchitectureRight()
    {
        OnRotateArchitectureRightAction?.Invoke();
    }

    public void OnCancelBuildMode()
    {
        OnCancelBuildModeAction?.Invoke();
    }

    public void OnInstallArchitecture()
    {
        OnInstallArchitectureAction?.Invoke();
    }

    public void OnBreakMode()
    {
        OnBreakModeAction?.Invoke();
    }

    public void OnBreakArchitecture()
    {
        OnBreakArchitectureAction?.Invoke();
    }
    #endregion

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
