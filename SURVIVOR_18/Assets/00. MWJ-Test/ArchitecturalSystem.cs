using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchitecturalSystem : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject tempPrefab;
    [SerializeField] Material previewMat;
    [SerializeField] private int raycastRange;

    private Material originMat;
    private GameObject obj;
    private bool isHold = false;
    private LayerMask groundLayer = 6;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isHold)
        {
            isHold = true;
            CreateBluePrintObject(RaycastHitPos().point);
        }

        if (isHold)
        {
            RaycastHit hit = RaycastHitPos();
            if (hit.collider != null && hit.collider.gameObject.layer == groundLayer)
                obj.transform.position = hit.point;

            if (Input.GetMouseButtonDown(0))
            {
                SetInitialObject();
                isHold = false;
            }
        }
    }

    private RaycastHit RaycastHitPos()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, raycastRange);

        return hit;
    }

    private void CreateBluePrintObject(Vector3 pos)
    {
        obj = Instantiate(tempPrefab, pos, Quaternion.identity);
        originMat = obj.GetComponentInChildren<MeshRenderer>().material;
        obj.GetComponentInChildren<Renderer>().material = previewMat;
    }

    private void SetInitialObject()
    {
        obj.GetComponentInChildren<MeshRenderer>().material = originMat;
        
        //obj.transform.GetChild(0).gameObject.layer = groundLayer;
    }
}
