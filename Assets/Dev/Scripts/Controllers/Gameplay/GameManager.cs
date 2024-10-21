using AVerse.Controllers.Behaviors;
using AVerse.Models;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace AVerse.Controllers.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        Dictionary<string, PropertyController> _allPropertiesByPropertyId;
        Dictionary<UnitSize, List<PropertyController>> _allPropertiesByUnitSize;
        //Dictionary<string, PropertyUnit> _allUnits;

        [SerializeField] PropertyController _initialTarget;
        [SerializeField] Property _currentTarget;
        List<UnitSize> _activeSizeFilters;

        private void OnEnable()
        {
            _allPropertiesByPropertyId = new Dictionary<string, PropertyController>();
            _allPropertiesByUnitSize = new Dictionary<UnitSize, List<PropertyController>>();
            //_allUnits = new Dictionary<string, PropertyUnit>();

            GameEvents.PropertySelectionChanged += On_PropertySelectionChanged;
            GameEvents.TransitionCompleted += On_TransitionCompleted;
            GameEvents.FilterSelectorChanged += On_FilterSelectionChange;
        }

        private void Start()
        {
            _activeSizeFilters = new List<UnitSize>();
            var allPropertiesInScen = FindObjectsByType<PropertyController>(FindObjectsSortMode.None);
            CachePropertiesInScene(allPropertiesInScen);
            //var allUnitsInScene = GameObject.FindObjectsByType<PropertyUnit>(FindObjectsSortMode.None);
            //CacheUnitsInScene(allUnitsInScene);
            _initialTarget.LoadProperty();
            _currentTarget = _initialTarget.PropertyDetails;
        }

        private void OnDisable()
        {
            ClearPropertiesCache();
            //ClearUnitsCache();

            GameEvents.PropertySelectionChanged -= On_PropertySelectionChanged;
            GameEvents.TransitionCompleted -= On_TransitionCompleted;
            GameEvents.FilterSelectorChanged -= On_FilterSelectionChange;
        }

        private void CachePropertiesInScene(PropertyController[] properties)
        {
            if (_allPropertiesByPropertyId == null) _allPropertiesByPropertyId = new Dictionary<string, PropertyController>();
            if (_allPropertiesByUnitSize == null) _allPropertiesByUnitSize = new Dictionary<UnitSize, List<PropertyController>>();

            foreach (var property in properties)
            {
                _allPropertiesByPropertyId.Add(property.PropertyId, property);
                if (!_allPropertiesByUnitSize.ContainsKey(property.UnitSize))
                {
                    _allPropertiesByUnitSize.Add(property.UnitSize, new List<PropertyController>());
                }
                _allPropertiesByUnitSize[property.UnitSize].Add(property);

            }
        }

        private void ClearPropertiesCache()
        {
            _allPropertiesByPropertyId.Clear();
            _allPropertiesByPropertyId = null;
        }

        private void On_PropertySelectionChanged(Property property)
        {
            if(_currentTarget.Id == property.Id)
            {
                UIManager.Instance.TriggerUnitDetails(property, true);
                return;
            }
            else
            {
                UIManager.Instance.TriggerUnitDetails(property, false);
            }
            if (_allPropertiesByPropertyId.ContainsKey(property.Id))
            {
                _allPropertiesByPropertyId[_currentTarget.Id].UnloadProperty();
                if (_activeSizeFilters.Count>0 && !_activeSizeFilters.Contains(_currentTarget.UnitSize))
                {
                    Debug.Log($"Disabling Availabity for Property Id since filter not active: {_currentTarget.Id}");
                    _allPropertiesByPropertyId[_currentTarget.Id].ShowAvailability(false);
                }
                UniversalTranisitionCamera.Instance.StartTransition(
                    _currentTarget.GetComponentInParent<PropertyController>().CameraTransform,
                    _allPropertiesByPropertyId[property.Id].CameraTransform);

                _currentTarget = property;
            }
            else
            {
                Debug.Log($"Property with ID : {property.Id}  Not present scene.");
            }
            
        }

        private void On_TransitionCompleted()
        {
            _allPropertiesByPropertyId[_currentTarget.Id].LoadProperty();

            if (_currentTarget.PropertyType == PropertyType.BUILDING)
            {
                //UIManager.Instance.ShowTopFilter(_currentTarget);
            }
            else if (_currentTarget.PropertyType == PropertyType.VILLA)
            {
                //UIManager.Instance.HideTopFilter(_currentTarget);
            }
        }

        private void On_FilterSelectionChange(UnitSize size)
        {
            if (_activeSizeFilters.Count == 0)
            {
                foreach (var propertyController in _allPropertiesByPropertyId.Values)
                {
                    propertyController.ShowAvailability(false);
                }
                ActivateFilter(size);
                return;
            }
            else if (_activeSizeFilters.Contains(size)) {
                foreach (var property in _allPropertiesByUnitSize[size])
                {
                    if (_currentTarget.Id == property.PropertyId)
                    {
                        Debug.Log("Not Actiavting Currently Selected Property");
                        continue;
                    }
                    property.ShowAvailability(false);
                }

                _activeSizeFilters.Remove(size);
                
                if (_activeSizeFilters.Count == 0)
                {
                    foreach (var property in _allPropertiesByPropertyId.Values)
                    {
                        if (_currentTarget.Id == property.PropertyId)
                        {
                            Debug.Log("Not Actiavting Currently Selected Property");
                            continue;
                        }
                        property.ShowAvailability(true);
                    }
                }
            }
            else
            {
                ActivateFilter(size);
                return;
            }
           
        }

        private void ActivateFilter(UnitSize size)
        {
            _activeSizeFilters.Add(size);
            foreach (var property in _allPropertiesByUnitSize[size])
            {
                if (_currentTarget.Id == property.PropertyId)
                {
                    Debug.Log("Not Actiavting Currently Selected Property");
                    continue;
                }
                property.ShowAvailability(true);
            }
        }
    }
}