using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwaner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject[] prefabs = new GameObject[5];

    public GameObject parent;

    public void spwan()
    {
        var index = Random.Range(0, prefabs.Length - 1);
        Instantiate(prefabs[index], parent.transform);
    }
}