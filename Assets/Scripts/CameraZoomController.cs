using UnityEngine;

public class CameraZoomController : MonoBehaviour
{
    public Camera _camera;
    public Transform target;
    public float zoomSpeed = 0.1f;
    public float minZoomDistance = 2.0f;
    public float maxZoomDistance = 25.0f;

    private float currentZoomDistance;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target Not Set for CameraZoomController");
        }
        currentZoomDistance = Vector3.Distance(_camera.transform.position, target.position);
    }
    private void Update()
    {
        if(Input.touchCount == 2)
        {
            var touchZero = Input.GetTouch(0);
            var touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            currentZoomDistance += deltaMagnitudeDiff*zoomSpeed;

            currentZoomDistance = Mathf.Clamp(currentZoomDistance, minZoomDistance, maxZoomDistance);

            _camera.transform.position = target.position - _camera.transform.forward * currentZoomDistance;
        }
    }
}
