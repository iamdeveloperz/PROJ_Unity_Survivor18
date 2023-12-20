using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    string GetName();
    void ObjectDestroy();
}
public class interactionManager : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float macCheckDistance;
    public LayerMask layerMask;

    private GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (Time.time - lastCheckTime > checkRate) //시간이 
        {
            lastCheckTime = Time.time; // 현재 시간 저장

            Ray ray  = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); //화면 정 중앙에서 발사
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, macCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractable = null;
                curInteractGameObject = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = $"[E] {curInteractable.GetName()} 줍기";
    }

    public void OnInteract()
    {
        if (curInteractable != null)
        {
            curInteractable.ObjectDestroy();
            curInteractable = null;
            curInteractGameObject = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
