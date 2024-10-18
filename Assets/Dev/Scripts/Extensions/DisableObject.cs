using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AVerse.Extensions
{
    public class DisableObject : MonoBehaviour
    {
        public void DisableGameObject()
        {
            gameObject.SetActive(false);
        }
    }
}
