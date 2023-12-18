using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IInteractable
{
    string GetName();
    void OnInteract();
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
        if (Time.time - lastCheckTime > checkRate) //�ð��� 
        {
            lastCheckTime = Time.time; // ���� �ð� ����

            Ray ray  = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); //ȭ�� �� �߾ӿ��� �߻�
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
        promptText.text = $"[E] {curInteractable.GetName()} �ݱ�";
    }
}
