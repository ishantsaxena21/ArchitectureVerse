using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Vector3 pos;
    private void Start()
    {
        pos = Camera.main.transform.position;
    }
    public void OnClick_Reset()
    {
        Debug.Log("Reset");
        Camera.main.transform.position = pos;
    }
}
