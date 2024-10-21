using System;
using UnityEngine;

namespace AVerse.Models
{
    public class Property : MonoBehaviour
    {
        [SerializeField] string _name;
        [SerializeField] string _id;
        [SerializeField] PropertyType _type;
        [SerializeField] bool _isAvailable;
        [SerializeField] UnitType type;
        public string Id { get { return _id; } }
        public string Name { get { return _name; } }
        public PropertyType Type { get { return _type; } }
        public bool IsAvailable { get { return _isAvailable; } }

        private void Awake()
        {
            var available = UnityEngine.Random.Range(0, 2);
            _id = "U" + UnityEngine.Random.Range(0, 1000000) + "" + UnityEngine.Random.Range(0, 1000000);
            _isAvailable = available >= 1 ? true : false ;
        }
    }

    [Serializable]
    public enum PropertyType { VILLA, BUILDING }

    

}