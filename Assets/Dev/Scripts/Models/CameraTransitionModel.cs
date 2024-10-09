using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AVerse.Models
{
    [Serializable]
    public class CameraTransitionModel
    {
        public Transform startTransform;
        public Transform endTransform;
        public float transitionDuration;
    }
}
