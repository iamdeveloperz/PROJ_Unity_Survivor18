using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using static UnityEditor.SceneView;

public class PlayerInputController : MonoBehaviour
{
    public GameObject inventory;
    private Inventory inven;
    private CinemachineBrain _cameraMove;
    public bool isinvenOpen = false;
    private PlayerInputs _playerInputs;

    private void Awake()
    {
        _cameraMove = Camera.main.GetComponent<CinemachineBrain>();
        inven = inventory.GetComponent<Inventory>();
        _playerInputs = GetComponent<PlayerInputs>();
    }
    private void Start()
    {
        //inventory.SetActive(false);
    }

    public void OnInventory(InputValue value)
    {
        OpenInventory();
    }
    public void OpenInventory()
    {
        if (!isinvenOpen)
        {
            PlayerMoveLookControl(!isinvenOpen);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            PlayerMoveLookControl(!isinvenOpen);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    private void PlayerMoveLookControl(bool isBool)
    {
        inventory.SetActive(isBool);
        isinvenOpen = isBool;
        _cameraMove.enabled = !isBool;
        //Time.timeScale = isBool ? 0 : 1;
        _playerInputs.DoSomething = isBool;
    }
}
