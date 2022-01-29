using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightToDark : MonoBehaviour
{
    //these are the tilemaps for light and dark, set in the inspector
    public GameObject lightmap;
    public GameObject darkmap;

    // Start is called before the first frame update
    void Start()
    {


    }
    public void ActivateDarkWorld()
    {
        print("light to dark DARK");
    }
    public void ActivateLightWorld()
    {
        print("light to dark LIGHT");
    }
    // Update is called once per frame
    void Update()
    {
    }
}
