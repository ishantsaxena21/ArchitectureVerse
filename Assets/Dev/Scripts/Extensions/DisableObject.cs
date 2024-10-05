using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    public void DisableGameObject()
    {
        gameObject.SetActive(false);
    }
}
