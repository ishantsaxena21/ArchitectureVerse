using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Dictionary<string, PropertyUnit> _allUnits;
    Dictionary<string, Property> _allProperties;

    [SerializeField] CameraContoller _cameraContoller;
    [SerializeField] Transform _initialTarget;


    private void OnEnable()
    {
        _allProperties = new Dictionary<string, Property>();
        _allUnits = new Dictionary<string, PropertyUnit>();

        GameEvents.PropertySelectionChanged += On_PropertySelectionChanged;
    }
    private void On_PropertySelectionChanged(string propertyId)
    {
        if (_allProperties.ContainsKey(propertyId))
        {
            _cameraContoller.UpdateTargetProperty(_allProperties[propertyId]);
        }
        else
        {
            Debug.Log($"Property with ID : {propertyId}  Not present scene.");
        }
    }

    private void Start()
    {
        var allPropertiesInScen = GameObject.FindObjectsByType<Property>(FindObjectsSortMode.None);     
        CachePropertiesInScene(allPropertiesInScen);
        var allUnitsInScene = GameObject.FindObjectsByType<PropertyUnit>(FindObjectsSortMode.None);     
        CachePropertyUnitsInScene(allUnitsInScene);
    }

    private void OnDisable()
    {
        ClearPropertiesCache();
        ClearPropertyUnitsCache();

        GameEvents.PropertySelectionChanged -= On_PropertySelectionChanged;
    }

    private void CachePropertiesInScene(Property[] propertyUnits)
    {
        if (_allUnits == null) _allUnits = new Dictionary<string, PropertyUnit>();

        foreach (var unit in propertyUnits) _allProperties.Add(unit.PropertyId, unit);
    }

    private void ClearPropertiesCache()
    {
        _allProperties.Clear();
        _allProperties = null;
    }   
    
    private void CachePropertyUnitsInScene(PropertyUnit[] propertyUnits)
    {
        if(_allUnits == null) _allUnits = new Dictionary<string, PropertyUnit>();

        foreach (var unit in propertyUnits) _allUnits.Add(unit.PropertyUnitId, unit);
    }
    private void ClearPropertyUnitsCache()
    {
        _allUnits.Clear();
        _allUnits = null;
    }

}
