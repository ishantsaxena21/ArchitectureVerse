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
        public string Id { get { return _id; } }
        public string Name { get { return _name; } }
        public PropertyType Type { get { return _type; } }
        public bool IsAvailable { get { return _isAvailable; } }
    }

    [Serializable]
    public enum PropertyType { VILLA, BUILDING }
}