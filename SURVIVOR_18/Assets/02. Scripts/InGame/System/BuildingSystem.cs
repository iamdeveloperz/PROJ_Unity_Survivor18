using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BuildingSystem : MonoBehaviour
{
    private Camera _cam;
    [SerializeField] private GameObject _tempPrefab;
    [SerializeField] private Material _previewMat;
    [SerializeField] private Material _nonBuildableMat;
    private Material _origionmat;

    private PlayerInputs _playerInputs;
    [SerializeField] private LayerMask _buildableLayer; // creatingLayer
    [SerializeField] private LayerMask _destroyLayer;
    private LayerMask _currentLayer;

    [SerializeField] private int _raycastRange = 20;
    [SerializeField] private float _yGridSize = 0.1f;
    [SerializeField] private int _rotationAngle = 45;

    [SerializeField] private GameObject _obj;
    private GameObject _lastHitObjectForBreakMode;
    private BuildableObject _buildableObject;

    private bool _isHold = false;
    private bool _isBreakMode = false;
    private bool _canCreateObject = true;
    private int buildingLayer = 30; // deleteLayer , 적용중인 레이어

    private QuickSlotSystem _quickSlotSystem;
    private bool validHIt = false;

    private void Awake()
    {
        _playerInputs = GetComponent<PlayerInputs>();
        _quickSlotSystem = GetComponent<QuickSlotSystem>();
        _cam = Camera.main;
    }

    private void Start()
    {
        _playerInputs.OnInstallArchitectureAction += CreateAndSetArchitecture;
        _playerInputs.OnInstallArchitectureAction += DestroyArchitecture;

        _playerInputs.OnRotateArchitectureLeftAction += HandleRotateArchitectureLeft;
        _playerInputs.OnRotateArchitectureRightAction += HandleRotateArchitectureRight;
        _playerInputs.OnCancelBuildModeAction += HandleCancelBuildMode;
        _playerInputs.OnBreakModeAction += HandleBreakMode;
    }

    void Update()
    {
        if (_playerInputs.DoSomething) return;

        if (_isHold)
        {
            SetObjPosition();
        }

        if (_isBreakMode)
        {
            HandleBreakArchitecture();
        }
    }

    #region
    private RaycastHit RaycastHit()
    {
        RaycastHit hit;
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        validHIt = Physics.Raycast(ray, out hit, _raycastRange, _currentLayer);
        return hit;
    }

    private void SetObjPosition(Vector3 hitPos)
    {
        Vector3 _location = hitPos;
        _location.Set(Mathf.Round(_location.x), Mathf.Round(_location.y / _yGridSize) * _yGridSize, Mathf.Round(_location.z));

        _obj.transform.position = _location;
    }

    private void SetObjPosition()
    {
        Vector3 _location = RaycastHit().point;
        _location.Set(Mathf.Round(_location.x), Mathf.Round(_location.y / _yGridSize) * _yGridSize, Mathf.Round(_location.z));

        _obj.transform.position = _location;
    }

    private void CreateBluePrintObject(Vector2 pos)
    {
        _obj = Instantiate(_tempPrefab);
        SetObjPosition(pos);

        _buildableObject = _obj.GetComponent<BuildableObject>();
        _buildableObject.SetMaterial(_previewMat);

        BuildableObjectColliderManager buildableObject = _obj.GetComponentInChildren<BuildableObjectColliderManager>();
        buildableObject.OnRedMatAction += HandleBuildableObjectTriggerEnter;
        buildableObject.OnBluePrintMatAction += HandleBuildableObjectTriggerExit;
    }

    private void CreateBluePrintObject(GameObject go)
    {
        _obj = go;
        //SetObjPosition(pos);

        _buildableObject = _obj.GetComponent<BuildableObject>();
        _buildableObject.SetMaterial(_previewMat);

        BuildableObjectColliderManager buildableObject = _obj.GetComponentInChildren<BuildableObjectColliderManager>();
        buildableObject.OnRedMatAction += HandleBuildableObjectTriggerEnter;
        buildableObject.OnBluePrintMatAction += HandleBuildableObjectTriggerExit;
    }

    private void SetMaterialForDeletion()
    {
        RaycastHit hit = RaycastHit();
        if (hit.collider != null && hit.collider.gameObject.layer == buildingLayer)
        {
            if (_lastHitObjectForBreakMode != null && _lastHitObjectForBreakMode != hit.collider.gameObject)
                _lastHitObjectForBreakMode.GetComponentInParent<BuildableObject>().SetOriginMaterial();

            _lastHitObjectForBreakMode = hit.collider.gameObject;
            _lastHitObjectForBreakMode.GetComponentInParent<BuildableObject>().SetMaterial(_nonBuildableMat);
        }
    }

    #endregion

    #region Player Input Action Handle

    private void HandleCreateBluePrint()
    {
        if (!_isHold)
        {
            _isHold = true;
            _currentLayer = _buildableLayer;
            CreateBluePrintObject(RaycastHit().point);
        }
    }

    private void HandleRotateArchitectureLeft()
    {
        if (_isHold)
            _obj.transform.Rotate(Vector3.up, -_rotationAngle);
    }

    private void HandleRotateArchitectureRight()
    {
        if (_isHold)
            _obj.transform.Rotate(Vector3.up, _rotationAngle);
    }

    private void HandleCancelBuildMode()
    {
        if (_isHold)
        {
            Destroy(_obj);
            _isHold = false;
        }
    }

    private void HandleInstallArchitecture()
    {
        if (_isHold && _canCreateObject && !_isBreakMode)
        {
            _isHold = false;
            _obj.GetComponentInChildren<BoxCollider>().isTrigger = false;

            _buildableObject.SetInitialObject();
            _buildableObject.DestroyColliderManager();
        }
    }

    private void HandleBreakMode()
    {
        if (_isHold)
        {
            HandleCancelBuildMode();
        }
        _currentLayer = _destroyLayer;
        _isBreakMode = _isBreakMode ? false : true;
    }

    private void HandleBreakArchitecture()
    {
        RaycastHit();

        if (!validHIt)
        {
            if (_obj)
            {
                VacateObj();
            }
            return;
        }

        if (_obj)
        {
            // 같은 놈이면 리턴
            if (_obj == RaycastHit().collider.gameObject) return;

            // 다른 놈이면
            VacateObj();
            GetObjToRay();
        }
        else
        {
            // 등록된 놈이 없으면
            GetObjToRay();
        }
    }

    #endregion

    #region Architecture Collider Action Handle

    private void HandleBuildableObjectTriggerEnter()
    {
        _canCreateObject = false;
    }

    private void HandleBuildableObjectTriggerExit()
    {
        _canCreateObject = true;
    }

    #endregion

    private void CreateAndSetArchitecture()
    {
        if (_isHold)
        {
            // Set            
            HandleInstallArchitecture();
        }
        else if (_isBreakMode == false)
        {
            // Create            
            var handItemData = _quickSlotSystem.HandleItem.itemData as HandleItemData;            
            if (handItemData.handType == HandableType.Building)
            {
                _tempPrefab = Managers.Resource.GetPrefab(handItemData.searchName, Literals.PATH_STRUCTURE);
                HandleCreateBluePrint();
            }
        }
    }

    private void DestroyArchitecture()
    {
        if (_isBreakMode && _obj)
        {
            Destroy(_obj.transform.parent.gameObject);
        }
    }

    private void VacateObj()
    {
        _obj.GetComponentInParent<BuildableObject>().SetMaterial(_origionmat);
        _playerInputs.gameObject.GetComponent<interactionManager>().promptText.gameObject.SetActive(false);
        _obj = null;
    }

    private void GetObjToRay()
    {
        GameObject toBeDestroyedObject = RaycastHit().collider.gameObject;
        _obj = toBeDestroyedObject;
        _origionmat = _obj.GetComponentInParent<BuildableObject>().GetMaterial();
        toBeDestroyedObject.GetComponentInParent<BuildableObject>().SetMaterial(_nonBuildableMat);

        var promptText = _playerInputs.gameObject.GetComponent<interactionManager>().promptText;
        promptText.gameObject.SetActive(true);
        promptText.text = "파괴하기";
    }
}