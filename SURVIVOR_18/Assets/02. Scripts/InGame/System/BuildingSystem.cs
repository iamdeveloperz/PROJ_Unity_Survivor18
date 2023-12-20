using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private GameObject _tempPrefab;
    [SerializeField] private Material _previewMat;
    [SerializeField] private Material _nonBuildableMat;
    [SerializeField] private PlayerInputs _playerInputs;
    [SerializeField] private LayerMask _groundLayer; // creatingLayer

    [SerializeField] private int _raycastRange = 20;
    [SerializeField] private float _yGridSize = 0.1f;
    [SerializeField] private int _rotationAngle = 45;

    private GameObject _obj;
    private GameObject _lastHitObjectForBreakMode;
    private BuildableObject _buildableObject;

    private bool _isHold = false;
    private bool _isBreakMode = false;
    private bool _canCreateObject = true;
    private int buildingLayer = 30; // deleteLayer , 적용중인 레이어

    private QuickSlotSystem _quickSlotSystem;

    private void Awake()
    {
        _playerInputs = GetComponent<PlayerInputs>();
        _quickSlotSystem = GetComponent<QuickSlotSystem>();
        _cam = Camera.main;
    }

    private void Start()
    {
        _playerInputs.OnInstallArchitectureAction += CreateAndSetArchitecture;

        _playerInputs.OnRotateArchitectureLeftAction += HandleRotateArchitectureLeft;
        _playerInputs.OnRotateArchitectureRightAction += HandleRotateArchitectureRight;
        _playerInputs.OnCancelBuildModeAction += HandleCancelBuildMode;
        _playerInputs.OnBreakModeAction += HandleBreakMode;
        _playerInputs.OnBreakArchitectureAction += HandleBreakArchitecture;
    }

    void Update()
    {
        if (_playerInputs.DoSomething) return;        

        if(_isHold)
        {
            SetObjPosition();
        }

        if(_isBreakMode)
        {

        }
    }

    #region
    private RaycastHit RaycastHit()
    {
        RaycastHit hit;
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, _raycastRange, _groundLayer);

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

    private void CreateBluePrintObject()
    {
        Vector2 pos = RaycastHit().point;
        CreateBluePrintObject(pos);
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
        _isBreakMode = _isBreakMode ? false : true;
    }

    private void HandleBreakArchitecture()
    {
        if (_isBreakMode)
        {
            GameObject toBeDestroyedObject = RaycastHit().collider.gameObject;
            if (toBeDestroyedObject.layer == buildingLayer)
                if (Input.GetMouseButtonDown(0))
                    Destroy(toBeDestroyedObject.transform.parent.gameObject);
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
                HandleCreateBluePrint();
            }
        }
    }
}