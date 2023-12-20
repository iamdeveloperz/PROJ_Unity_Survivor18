using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingStation : MonoBehaviour
{
    [SerializeField] private GameObject _craftingPopup;
    //private CinemachineBrain _cameraMove;

    private void Awake()
    {
        //_cameraMove = Camera.main.GetComponent<CinemachineBrain>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMoveLookControl(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMoveLookControl(false);
        }
    }

    private void PlayerMoveLookControl(bool isPlayerInCraftzone)
    {
        _craftingPopup.SetActive(isPlayerInCraftzone);
        Cursor.lockState = isPlayerInCraftzone ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPlayerInCraftzone;

        //_cameraMove.enabled = !isPlayerInCraftzone;
    }
}
