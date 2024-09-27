using UnityEngine;

[ExecuteInEditMode][RequireComponent(typeof(Property))] // Ensure the script runs in the Unity Editor
public class BoundaryDrawer : MonoBehaviour
{
    public Color boundaryColor = Color.red; // Color of the boundary
    Property propertyGameObject;
    private void Start()
    {
        propertyGameObject = GetComponent<Property>();
    }
    // This method will draw the boundary only when the object is selected
    private void OnDrawGizmos()
    {
        // Get the MeshRenderer component
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer != null)
        {
            // Set the boundary color
            Gizmos.color = boundaryColor;

            // Get the bounds of the object
            Bounds bounds = meshRenderer.bounds;

            // Draw a wireframe cube based on the bounds
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }
        else
        {
            Debug.LogWarning("MeshRenderer not found! Unable to draw boundary.");
        }
        
        if (!Application.isPlaying)
        {
            propertyGameObject.BuildingParentObject = transform.GetChild(0).gameObject; ;
        }
    }
}
