using AVerse.Controllers.Gameplay;
using AVerse.Extensions;
using AVerse.Models;
using AVerse.Models;
using System;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


namespace AVerse.Controllers.Behaviors
{
    [ExecuteAlways]
    public class PropertyController : MonoBehaviour
    {
        //Data
        private Property _propertyDetails;

        //Unity Objects
        BoundaryDrawer _boundingBox;
        [SerializeField] Button _buttonPropertyHover;
        [SerializeField] CameraContoller _cameraController;
        [SerializeField] GameObject _buildingParentObject;
        [SerializeField] CameraRotationModel _rotationModel;
        [SerializeField] CameraZoomModel _zoomModel;
        [SerializeField] UnitsController _unitsController;

        public BoundaryDrawer BoundingBox { get { return _boundingBox; } }
        public string PropertyId { get { return _propertyDetails.Id; } }
        public GameObject BuildingParentObject { set { _buildingParentObject = value; } }

        private void Awake()
        {
            Initialize();
        }
        private void Initialize()
        {
            _propertyDetails = GetComponentInChildren<Property>(true);
            if (_propertyDetails == null)
            {
                Debug.LogError($"No Property Object found for gameObject : {name}");
                return;
            }
            
            _boundingBox = GetComponentInChildren<BoundaryDrawer>(true);
            if (_boundingBox == null)
            {
                Debug.LogError($"No Bouding Box found for gameObject : {name}");
                return;
            }

            if (_buttonPropertyHover == null)
            {
                Debug.LogError($"No Property Hover found for gameObject : {name}");
                return;
            }
            _buttonPropertyHover.onClick.AddListener(OnClick_PropertySelection);
            
            if (_cameraController == null)
            {
                Debug.LogError($"No Camera Controller found for gameObject : {name}");
                return;
            }
            _cameraController.Initialize(_boundingBox.transform, _rotationModel, _zoomModel);
            _cameraController.gameObject.SetActive(false);
            
            _unitsController = GetComponentInChildren<UnitsController>(true);
            if (_unitsController == null)
            {
                Debug.LogError($"No UnitController found for gameObject : {name}");
                return;
            }

        }       

        private void Start()
        {
            SetUp();
        }
        public void SetUp()
        {
            _buttonPropertyHover.GetComponentInChildren<TextMeshProUGUI>().SetText(_propertyDetails.Name);
            UnloadProperty();
        }

        private void OnClick_PropertySelection()
        {
            Debug.Log($"Property : {_propertyDetails.Id} : {_propertyDetails.Name} selected");
            GameEvents.PropertySelectionChanged(PropertyId);
        }

        public void LoadProperty()
        {
            _cameraController.gameObject.SetActive(true);
            _unitsController.gameObject.SetActive(_propertyDetails.IsAvailable);
        }

        public void UnloadProperty()
        {
            _cameraController.gameObject.SetActive(false);
            _unitsController.gameObject.SetActive(false);
        }
    }
}