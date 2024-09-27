using UnityEngine;

public class PropertyUnit : MonoBehaviour
{
    [SerializeField] string _propertyUnitId;

    public string PropertyUnitId { get { return _propertyUnitId; } }
}
