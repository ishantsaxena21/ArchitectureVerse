using UnityEngine;


[RequireComponent(typeof(CameraZoomController))]
[RequireComponent(typeof(CameraRotationController))]
public class CameraContoller : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] Transform _target;
    private CameraZoomController _zoomController;
    private CameraRotationController _rotationController;


    private void Start()
    {
        _zoomController = GetComponent<CameraZoomController>();
        _rotationController = GetComponent<CameraRotationController>();
    }
    public void UpdateTargetProperty(Property property)
    {
        _target = property.GetComponentInParent<BoundaryDrawer>().transform;
        _zoomController.UpdateZoom(_target);
        _rotationController.UpdateRotation(_target);
    }


    private void Update()
    {
        _zoomController.UpdateZoom(_target);
        _rotationController.UpdateRotation(_target);
    }
}
