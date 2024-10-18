using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeCarSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> childObjects = new List<GameObject>();

        int i = 0;
        // Loop through all child transforms of the parent object
        foreach (Transform child in this.transform)
        {
            i++;
            childObjects.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }

        childObjects[Random.Range(0,i)].SetActive(true);
        print($"Total Size: {i}");
    }

}
