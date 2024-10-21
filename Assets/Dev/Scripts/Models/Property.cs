using System;
using UnityEngine;

namespace AVerse.Models
{
    public class Property : MonoBehaviour
    {
        [SerializeField] string _name;
        [SerializeField] string _id;
        [SerializeField] PropertyType _propertytType;
        [SerializeField] bool _isAvailable;
        [SerializeField] UnitSize _unitSize;
        public string Id { get { return _id; } }
        public string Name { get { return _name; } }
        public PropertyType PropertyType { get { return _propertytType; } }
        public UnitSize UnitSize { get { return _unitSize; } }
        public bool IsAvailable { get { return _isAvailable; } }

        private void Awake()
        {
            var available = UnityEngine.Random.Range(0, 2);
            _id = "U" + UnityEngine.Random.Range(0, 1000000) + "" + UnityEngine.Random.Range(0, 1000000);
            _isAvailable = available >= 1 ? true : false ;

            _unitSize = (UnitSize) UnityEngine.Random.Range(4, 7);
        }
    }

    [Serializable]
    public enum PropertyType { VILLA, BUILDING }

    

}