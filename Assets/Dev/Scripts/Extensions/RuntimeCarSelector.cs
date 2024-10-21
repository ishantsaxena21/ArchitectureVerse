using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeCarSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int randomCarIndex = Random.Range(0, transform.childCount);
        // Loop through all child transforms of the parent object
        for(int i=0;i< transform.childCount; i++)
        {
            if(i == randomCarIndex)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                Destroy(transform.GetChild(i).gameObject);
            }            
        }

    }

}
