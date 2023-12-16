using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLaycast : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject tempPrefab;
    [SerializeField] Material previewMat;

    // TIL
    private Material originMat;
    private GameObject obj;
    private bool isHold = false;

    void Start()
    {

    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, 20);

        if (Input.GetMouseButtonDown(0) && isHold == true)
        {
            obj.GetComponentInChildren<MeshRenderer>().material = originMat;
            
            // GameObject¿ßø° GameObject
            obj.transform.GetChild(0).gameObject.layer = 6;
            isHold = false;
        }

        if (hit.collider != null && hit.collider.gameObject.layer == 6)
        {
            if (isHold)
                obj.transform.position = hit.point;
            else if (Input.GetKeyDown(KeyCode.E))
            {
                isHold = true;

                obj = Instantiate(tempPrefab, hit.point, Quaternion.identity);
                
                //TIL
                Material material = obj.GetComponentInChildren<MeshRenderer>().material;
                originMat = material;

                obj.GetComponentInChildren<Renderer>().material = previewMat;
            }
        }
    }
}
