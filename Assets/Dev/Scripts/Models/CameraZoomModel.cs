using System;
using UnityEngine;

namespace AVerse.Models
{
    [Serializable]
    public class CameraZoomModel
    {
        public float zoomSpeed = 0.1f;
        public float minZoomDistance = 2.0f;
        public float maxZoomDistance = 25.0f;
    }
}