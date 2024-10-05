using UnityEngine;

namespace AVerse.Extensions
{
    [ExecuteInEditMode] // Runs the script even when not in Play Mode
    public class CameraLikeGizmos : MonoBehaviour
    {
        // Define the color for the gizmo lines
        public Color gizmoColor = Color.green;

        // Define the size of the gizmo
        public float gizmoSize = 0.5f;

        // Optionally add a field for a specific icon
        public Texture2D gizmoIcon;

        void OnDrawGizmos()
        {
            // Set the Gizmo color
            Gizmos.color = gizmoColor;

            // Draw a representation of the GameObject like a camera
            DrawCameraGizmo(transform.position, transform.forward, gizmoSize);

            // Optionally draw an icon at the GameObject's position
            if (gizmoIcon != null)
            {
                Gizmos.DrawGUITexture(new Rect(transform.position.x - gizmoSize / 2, transform.position.y - gizmoSize / 2, gizmoSize, gizmoSize), gizmoIcon);
            }
        }

        // This draws when the object is selected in the Scene View
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            DrawCameraGizmo(transform.position, transform.forward, gizmoSize * 1.5f); // Slightly larger when selected
        }

        // Function to draw a camera-like gizmo
        private void DrawCameraGizmo(Vector3 position, Vector3 forward, float size)
        {
            // Draw the forward direction as a line from the GameObject
            Gizmos.DrawLine(position, position + forward * size);

            // Draw a frustum-like pyramid to represent the camera view
            Vector3 right = Vector3.Cross(Vector3.up, forward).normalized * size;
            Vector3 up = Vector3.Cross(forward, right).normalized * size * 0.5f;

            // Frustum points
            Vector3 topRight = position + forward * size + right + up;
            Vector3 topLeft = position + forward * size - right + up;
            Vector3 bottomRight = position + forward * size + right - up;
            Vector3 bottomLeft = position + forward * size - right - up;

            // Draw the pyramid-like lines
            Gizmos.DrawLine(position, topRight);
            Gizmos.DrawLine(position, topLeft);
            Gizmos.DrawLine(position, bottomRight);
            Gizmos.DrawLine(position, bottomLeft);

            // Connect the lines between the frustum points
            Gizmos.DrawLine(topRight, topLeft);
            Gizmos.DrawLine(topLeft, bottomLeft);
            Gizmos.DrawLine(bottomLeft, bottomRight);
            Gizmos.DrawLine(bottomRight, topRight);
        }
    }
}