using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ArchitecturalSystem : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject tempPrefab;
    [SerializeField] private Material previewMat;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int raycastRange;
    [SerializeField] private float yGridSize = 0.1f;

    private Material originMat;
    private GameObject obj;
    private bool isHold = false;

    void Update()
    {
        if (isHold)
        {
            RaycastHit hit = RaycastHit();
            if (hit.collider != null
                && groundLayer.value == (groundLayer.value | (1 << hit.collider.gameObject.layer)))
                SetObjPos(hit.point);

            if (Input.GetMouseButtonDown(0))
            {
                SetInitialObject();
                isHold = false;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(obj);
                isHold = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            isHold = true;
            CreateBluePrintObject(RaycastHit().point);
        }
    }

    private RaycastHit RaycastHit()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, raycastRange);

        return hit;
    }

    private void SetObjPos(Vector3 hitPos)
    {
        Vector3 _location = hitPos;
        // Round -> 실수를 정수로 반올림하는 함수
        _location.Set(Mathf.Round(_location.x), Mathf.Round(_location.y / yGridSize) * yGridSize, Mathf.Round(_location.z));

        obj.transform.position = _location;
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

        //obj.layer = groundLayer.value;
        //obj.transform.GetChild(0).gameObject.layer = groundLayer.value;
    }
}
