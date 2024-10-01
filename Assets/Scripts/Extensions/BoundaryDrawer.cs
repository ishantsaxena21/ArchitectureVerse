using UnityEngine;

[ExecuteInEditMode][RequireComponent(typeof(Property))] // Ensure the script runs in the Unity Editor
public class BoundaryDrawer : MonoBehaviour
{
    [SerializeField] Material boundaryMaterial; // Color of the boundary
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
            if (boundaryMaterial == null)
            {
                Debug.LogError($"Missing Boundary Color Material on gameobject {this.name}");
            }
            // Set the boundary color
            Gizmos.color = boundaryMaterial.color;

            // Get the bounds of the object
            Bounds bounds = meshRenderer.bounds;

            // Draw a wireframe cube based on the bounds
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }
        else
        {
            Debug.LogWarning("MeshRenderer not found! Unable to draw boundary.");
        }
        
        if (!Application.isPlaying && propertyGameObject!=null)
        {
            if (propertyGameObject != null) {
                propertyGameObject = GetComponent<Property>();
            }
            propertyGameObject.BuildingParentObject = transform.GetChild(0).gameObject; ;
        }
    }
}
