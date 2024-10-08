using AVerse.Models;
using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace AVerse.Controllers.Gameplay
{
    public class CameraContoller : MonoBehaviour
    {
        [SerializeField] Camera _camera;
        [SerializeField] Transform _target; //Bounding Box -> Initialized By PropertyController in Awake
        [SerializeField] bool _invertX;
        public Camera Camera { get { return _camera; } }

        private Vector2 previousTouchPosition, previousMouseTouchPos;
        private float currentAngle = 0f;

        private float currentZoomDistance;

        private CameraRotationModel _rotationModel;
        private CameraZoomModel _zoomModel;


        private void Start()
        {
            _camera = GetComponent<Camera>();
        }
        public void Initialize(Transform target, CameraRotationModel rotationModel, CameraZoomModel zoomModel)
        {
            _target = target;
            _rotationModel = rotationModel;
            _zoomModel = zoomModel;
        }

        private void Update()
        {
#if UNITY_WEBGL || UNITY_STANDALONE_WIN || UNITY_EDITOR
            HandleMouseInputs();
#else
            HandleTouchInputs();
#endif       
        }

        private void HandleMouseInputs()
        {
            UpdateMouseZoom(_target);
            UpdateMouseRotation(_target);
        }
        private void UpdateMouseZoom(Transform target)
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            currentZoomDistance -= scrollInput * _zoomModel.zoomSpeed * 100;
            currentZoomDistance = Mathf.Clamp(currentZoomDistance, _zoomModel.minZoomDistance, _zoomModel.maxZoomDistance);
            _camera.transform.position = target.position - _camera.transform.forward * currentZoomDistance;
        }
        public void UpdateMouseRotation(Transform target)
        {
            if (Input.GetMouseButtonDown(1))
            {
                previousMouseTouchPos = target.position;
            }
            else if (Input.GetMouseButton(1))
            {
                Vector2 mouseDelta = (Vector2)Input.mousePosition - previousMouseTouchPos;
                float rotationX = mouseDelta.y*_rotationModel.rotationSpeed;
                float rotationY = mouseDelta.x*_rotationModel.rotationSpeed;

                rotationX *= _invertX ? -1 : 1;

                float newXAngle = Mathf.Clamp(currentAngle + rotationX, _rotationModel.minXAngle, _rotationModel.maxXAngle);

                if (newXAngle != currentAngle)
                {
                    transform.RotateAround(target.position, transform.right, newXAngle - currentAngle);
                    currentAngle = newXAngle;
                }

                transform.RotateAround(target.position, Vector3.up, rotationY);
                previousMouseTouchPos = Input.mousePosition;
            }

        }
        

        private void HandleTouchInputs()
        {
            UpdateTouchZoom(_target);
            UpdateTouchRotation(_target);
        }
        public void UpdateTouchRotation(Transform target)
        {
            if (Input.touchCount == 1)
            {
                var touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    previousTouchPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 touchDelta = touch.position - previousTouchPosition;
                    float rotX = touchDelta.y * _rotationModel.rotationSpeed;
                    float rotY = touchDelta.x * _rotationModel.rotationSpeed;

                    rotX *= _invertX? -1:1;
                    
                    float newXAngle = Mathf.Clamp(currentAngle + rotX, _rotationModel.minXAngle, _rotationModel.maxXAngle);

                    if (newXAngle != currentAngle)
                    {
                        transform.RotateAround(target.position, transform.right, newXAngle - currentAngle);
                        currentAngle = newXAngle;
                    }

                    transform.RotateAround(target.position, Vector3.up, rotY);
                    previousTouchPosition = touch.position;
                }
            }
        }
        public void UpdateTouchZoom(Transform target)
        {
            if (Input.touchCount == 2)
            {
                var touchZero = Input.GetTouch(0);
                var touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                currentZoomDistance += deltaMagnitudeDiff * _zoomModel.zoomSpeed;

                currentZoomDistance = Mathf.Clamp(currentZoomDistance, _zoomModel.minZoomDistance, _zoomModel.maxZoomDistance);

                _camera.transform.position = target.position - _camera.transform.forward * currentZoomDistance;
            }
        }
    }
}