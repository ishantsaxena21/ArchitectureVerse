using AVerse.Models;
using UnityEngine;

namespace AVerse.Controllers.Gameplay
{
    public class CameraContoller : MonoBehaviour
    {
        [SerializeField] Camera _camera;
        [SerializeField] Transform _target;
        public Camera Camera { get { return _camera; } }

        private Vector2 previousTouchPosition;
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
            UpdateZoom(_target);
            UpdateRotation(_target);
        }

        public void UpdateRotation(Transform target)
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
        public void UpdateZoom(Transform target)
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