using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private GameObject _tempPrefab;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private int _raycastRange = 20;
    [SerializeField] private float _yGridSize = 0.1f;
    [SerializeField] private int _rotationAngle = 45;

    [SerializeField] private Material _previewMat;
    [SerializeField] private Material _nonBuildableMat;

    private GameObject _obj;
    private BuildableObject _buildableObject;

    private bool _isHold = false;
    private bool _canCreateObject = true;
    private bool _isBreakMode = false;

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
                _isHold = false;
                _obj.GetComponentInChildren<BoxCollider>().isTrigger = false;
                
                _buildableObject.SetInitialObject();
                _buildableObject.DestroyColliderManager();
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
        else if (Input.GetKeyDown(KeyCode.J))
        {
            _isHold = true;
            CreateBluePrintObject(RaycastHit().point);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            if(_isHold)
            {
                Destroy(_obj);
                _isHold = false;
            }
            _isBreakMode = _isBreakMode ? false : true;
            Debug.Log("철거모드");
        }

        //if (_isBreakMode)
        //{
        //    GameObject toBeDestroyedObject = RaycastHit().collider.gameObject;
        //    if (toBeDestroyedObject.layer == 30)
        //    {
        //        toBeDestroyedObject.GetComponentInChildren<BuildableObject>().SetMaterial(_nonBuildableMat);
        //        if (Input.GetMouseButtonDown(0))
        //            Destroy(toBeDestroyedObject);
        //    }
        //}
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

        _buildableObject = _obj.GetComponent<BuildableObject>();
        _buildableObject.SetMaterial(_previewMat);

        BuildableObjectColliderManager buildableObject = _obj.GetComponentInChildren<BuildableObjectColliderManager>();
        buildableObject.OnRedMatAction += HandleBuildableObjectTriggerEnter;
        buildableObject.OnBluePrintMatAction += HandleBuildableObjectTriggerExit;
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
