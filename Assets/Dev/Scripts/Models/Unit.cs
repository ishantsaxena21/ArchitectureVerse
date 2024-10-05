using UnityEngine;

namespace AVerse.Models
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] string _unitId;

        public string PropertyUnitId { get { return _unitId; } }
    }
}
