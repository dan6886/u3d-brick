using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] group; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Screen.fullScreen = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Screen.fullScreen = true;
        } 
    }
}
