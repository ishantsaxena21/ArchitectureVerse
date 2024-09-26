using UnityEngine;

public class CameraRotationController : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 0.2f;
    public float minXAngle = -30f;
    public float maxXAngle = 60f;

    private Vector2 previousTouchPosition;
    private float currentAngle=0f;

    private void Start()
    {
        if(target == null)
        {
            Debug.LogError("Target Not Set for CameraRotationController");
        }
    }

    private void Update()
    {
        if (Input.touchCount == 1) { 
            var touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                previousTouchPosition = touch.position;
            }
            else if(touch.phase == TouchPhase.Moved)
            {
                Vector2 touchDelta = touch.position - previousTouchPosition;
                float rotX = touchDelta.y * rotationSpeed;  
                float rotY = touchDelta.x * rotationSpeed;

                float newXAngle = Mathf.Clamp(currentAngle + rotX, minXAngle, maxXAngle);

                if(newXAngle != currentAngle)
                {
                    transform.RotateAround(target.position, transform.right, newXAngle - currentAngle);
                    currentAngle = newXAngle;
                }

                transform.RotateAround(target.position, Vector3.up, rotY);
                previousTouchPosition = touch.position;
            }
        }
    }

}
