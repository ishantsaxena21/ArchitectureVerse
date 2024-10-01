using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Vector3 pos;
    private void Start()
    {
        pos = Camera.main.transform.position;
    }

    private void OnEnable()
    {
        
    }
    public void OnClick_Reset()
    {
        Debug.Log("Reset");
        Camera.main.transform.position = pos;
    }
    public void OnClick_PorpertyId(string id)
    {
        GameEvents.PropertySelectionChanged?.Invoke(id);
    }
}
