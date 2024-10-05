using UnityEngine;

//[ExecuteInEditMode] // Ensures the script runs in both Edit Mode and Play Mode
[RequireComponent (typeof(Canvas))]
public class FaceCamera : MonoBehaviour
{
    [SerializeField] Camera focusCamera; // Reference to the camera (drag in the Inspector)
    Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }
    void LateUpdate()
    {
        FaceTowardsCamera();
    }

    void FaceTowardsCamera()
    {
        focusCamera = Camera.main;
        _canvas.worldCamera = focusCamera;        
        if (focusCamera == null)
        { 
            Debug.LogError($"No Camera found for gameObject : {transform.parent.name}");
            return;
        }
        Vector3 cameraPosition = focusCamera.transform.position;
        cameraPosition.y = transform.position.y;
        Vector3 directionToCamera = transform.position - cameraPosition;
        transform.rotation = Quaternion.LookRotation(directionToCamera);

        //transform.LookAt(transform.position + focusCamera.transform.rotation * Vector3.forward, focusCamera.transform.rotation * Vector3.up);
    }
}
