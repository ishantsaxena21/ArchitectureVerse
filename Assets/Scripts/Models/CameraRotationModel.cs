using System;

namespace AVerse.Models
{
    [Serializable]
    public class CameraRotationModel
    {
        //Rotation Variables
        public float rotationSpeed = 0.2f;
        public float minXAngle = -30f;
        public float maxXAngle = 60f;
    }
}