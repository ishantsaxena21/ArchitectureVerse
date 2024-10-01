using UnityEngine;

[ExecuteInEditMode] // Ensures the script runs in both Edit Mode and Play Mode
public class FaceCamera : MonoBehaviour
{
    public Camera mainCamera; // Reference to the camera (drag in the Inspector)

    void Start()
    {
        // If no camera is assigned, default to the main camera
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void LateUpdate()
    {
        FaceTowardsCamera();
    }

    // This runs in the editor when something changes in the inspector
    void OnValidate()
    {
        // Ensure it updates even when modifying in the editor
        FaceTowardsCamera();
    }

    // Method to rotate the canvas to face the camera
    void FaceTowardsCamera()
    {
        if (mainCamera != null)
        {
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                             mainCamera.transform.rotation * Vector3.up);
        }
    }
}
