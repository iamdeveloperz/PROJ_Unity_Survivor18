using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class ArchitecturalSystem : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private GameObject _tempPrefab;
    [SerializeField] private Material _previewMat;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private int _raycastRange = 20;
    [SerializeField] private float _yGridSize = 0.1f;
    [SerializeField] private int _rotationAngle = 45;

    private Material _originMat;
    private GameObject _obj;
    private bool _isHold = false;
    private bool _canCreateObject = true;

    void Update()
    {
        if (_isHold)
        {
            RaycastHit hit = RaycastHit();
            if (hit.collider != null
                && _groundLayer == (_groundLayer | (1 << hit.collider.gameObject.layer)))
                SetObjPosition(hit.point);

            if (Input.GetMouseButtonDown(0) && _canCreateObject)
            {
                SetInitialObject();
                _isHold = false;
                _obj.GetComponentInChildren<BoxCollider>().isTrigger = false;

                Destroy(_obj.GetComponentInChildren<BuildableObject>());
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(_obj);
                _isHold = false;
            }
            else if (Input.GetKeyDown(KeyCode.Z))
                _obj.transform.Rotate(Vector3.up, _rotationAngle);
            else if (Input.GetKeyDown(KeyCode.C))
                _obj.transform.Rotate(Vector3.up, -_rotationAngle);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            _isHold = true;
            CreateBluePrintObject(RaycastHit().point);
        }
    }

    private RaycastHit RaycastHit()
    {
        RaycastHit hit;
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, _raycastRange);

        return hit;
    }

    private void SetObjPosition(Vector3 hitPos)
    {
        Vector3 _location = hitPos;
        _location.Set(Mathf.Round(_location.x), Mathf.Round(_location.y / _yGridSize) * _yGridSize, Mathf.Round(_location.z));

        _obj.transform.position = _location;
    }

    private void CreateBluePrintObject(Vector3 pos)
    {
        _obj = Instantiate(_tempPrefab);
        SetObjPosition(pos);
        _originMat = _obj.GetComponentInChildren<Renderer>().material;
        _obj.GetComponentInChildren<Renderer>().material = _previewMat;

        BuildableObject buildableObject = _obj.GetComponentInChildren<BuildableObject>();
        buildableObject.OnRedMatAction += HandleBuildableObjectTriggerEnter;
        buildableObject.OnBluePrintMatAction += HandleBuildableObjectTriggerExit;
    }

    private void SetInitialObject()
    {
        _obj.GetComponentInChildren<Renderer>().material = _originMat;

        // TODO
        _obj.layer = 30;
        for (int i = 0; i < _obj.transform.childCount; ++i)
            _obj.transform.GetChild(i).gameObject.layer = 30;
        //obj.layer = LayerMask.GetMask("Building");
        //obj.transform.GetChild(0).gameObject.layer = groundLayer.value;
    }

    private void HandleBuildableObjectTriggerEnter()
    {
        _canCreateObject = false;
    }
    private void HandleBuildableObjectTriggerExit()
    {
        _canCreateObject = true;
    }
}
