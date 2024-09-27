using System;
using Unity.Collections;
using UnityEngine;

[ExecuteAlways]
public class Property : MonoBehaviour
{
    [SerializeField] string _propertyUnitId;
    [ReadOnly] [SerializeField] GameObject _buildingParentObject;

    public string PropertyId { get { return _propertyUnitId; } }
    public GameObject BuildingParentObject { set { _buildingParentObject = value; } } 
    private void Start()
    {
        FindFirstChild();
    }

    private void FindFirstChild()
    {
        if (transform.childCount > 0)
        {
            // Get the first child GameObject
            _buildingParentObject = transform.GetChild(0).gameObject;
        }
        else
        {
            Debug.LogWarning($"No children found for {this.name} GameObject.");
        }
    }
}
