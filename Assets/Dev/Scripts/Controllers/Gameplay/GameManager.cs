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
        Dictionary<string, PropertyController> _allProperties;
        //Dictionary<string, PropertyUnit> _allUnits;

        [SerializeField] PropertyController _initialTarget;
        Property _currentTarget;


        private void OnEnable()
        {
            _allProperties = new Dictionary<string, PropertyController>();
            //_allUnits = new Dictionary<string, PropertyUnit>();

            GameEvents.PropertySelectionChanged += On_PropertySelectionChanged;
            GameEvents.TransitionCompleted += On_TransitionCompleted;
        }

        private void Start()
        {
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
        }

        private void CachePropertiesInScene(PropertyController[] properties)
        {
            if (_allProperties == null) _allProperties = new Dictionary<string, PropertyController>();

            foreach (var property in properties)
            {

                _allProperties.Add(property.PropertyId, property);
            }
        }

        private void ClearPropertiesCache()
        {
            _allProperties.Clear();
            _allProperties = null;
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
            if (_allProperties.ContainsKey(property.Id))
            {
                foreach (var propertyController in _allProperties.Values)
                {
                    propertyController.UnloadProperty();
                }
                UniversalTranisitionCamera.Instance.StartTransition(
                    _currentTarget.GetComponentInParent<PropertyController>().CameraTransform,
                    _allProperties[property.Id].CameraTransform);

                //_allProperties[property.Id].StartTransition();
                _currentTarget = property;
                //On_TransitionCompleted(property);

                //TODO: _cameraContoller.UpdateTarget(_allProperties[propertyId].BoundingBox.transform);
            }
            else
            {
                Debug.Log($"Property with ID : {property.Id}  Not present scene.");
            }
            
        }

        private void On_TransitionCompleted()
        {
            _allProperties[_currentTarget.Id].LoadProperty();

            if (_currentTarget.Type == PropertyType.BUILDING)
            {
                UIManager.Instance.ShowTopFilter(_currentTarget);
            }
            else if (_currentTarget.Type == PropertyType.VILLA)
            {
                UIManager.Instance.HideTopFilter(_currentTarget);
            }
        }

        //private void CacheUnitsInScene(PropertyUnit[] units)
        //{
        //    if (_allUnits == null) _allUnits = new Dictionary<string, PropertyUnit>();

        //    foreach (var unit in units) _allUnits.Add(unit.PropertyUnitId, unit);
        //}
        //private void ClearUnitsCache()
        //{
        //    _allUnits.Clear();
        //    _allUnits = null;
        //}

    }
}