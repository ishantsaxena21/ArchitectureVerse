using AVerse.Controllers.Gameplay;
using AVerse.Extensions;
using AVerse.Models;
using System;
using System.Collections;
using TMPro;
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
        [SerializeField] Canvas _canvas;
        [SerializeField] Button _buttonPropertyHover;
        [SerializeField] CameraContoller _cameraController;
        [SerializeField] GameObject _buildingParentObject;
        [SerializeField] CameraRotationModel _rotationModel;
        [SerializeField] CameraZoomModel _zoomModel;
        [SerializeField] UnitsController _unitsController;
        [SerializeField] bool _requireCanvas;

        float _transitionProgress;
        bool _isTransitioning;
        public BoundaryDrawer BoundingBox { get { return _boundingBox; } }
        public Property PropertyDetails {  get { return _propertyDetails; } }
        public string PropertyId { get { return _propertyDetails.Id; } }
        public GameObject BuildingParentObject { set { _buildingParentObject = value; } }
        public Transform CameraTransform { get { return _cameraController.transform; } }

        private void Awake()
        {           
            Initialize();
        }

        private void OnEnable()
        {
            GameEvents.ToggleUI += HideUI;
        }

        private void OnDisable()
        {
            GameEvents.ToggleUI -= HideUI;
        }
        private void Initialize()
        {
            _propertyDetails = GetComponentInChildren<Property>(true);
            if (_propertyDetails == null)
            {
                Debug.LogError($"No Property Object found for gameObject : {name}");
                return;
            }

            _canvas = GetComponentInChildren<Canvas>(true);
            if (_canvas == null)
            {
                Debug.LogError($"No Canvas found for gameObject : {name}");
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
            _canvas.gameObject.SetActive(_requireCanvas);
            _buttonPropertyHover.GetComponentInChildren<TextMeshProUGUI>().SetText(_propertyDetails.Name);
            _unitsController.transform.SetPositionAndRotation(_propertyDetails.transform.position, _propertyDetails.transform.rotation);
            UnloadProperty();
        }

        private void OnClick_PropertySelection()
        {
            Debug.Log($"Property : {_propertyDetails.Id} : {_propertyDetails.Name} selected");
            GameEvents.PropertySelectionChanged(_propertyDetails);
        }

        public void LoadProperty()
        {
            _cameraController.gameObject.SetActive(true);
            _unitsController.gameObject.SetActive(false);
            
        }

        public void UnloadProperty()
        {
            _cameraController.gameObject.SetActive(false);
            ShowAvailability(true);
        }
        private void HideUI(bool isEnabled)
        {
            _canvas.gameObject.SetActive(isEnabled);
            ShowAvailability(isEnabled);
        }

        public void ShowAvailability(bool show)
        {
            if(!show)
                _unitsController.gameObject.SetActive(false);
            else
                _unitsController.gameObject.SetActive(_propertyDetails.IsAvailable);

        }

    }
}