using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightToDark : MonoBehaviour
{

    //these methods are called when the player presses space
    //see code in PlayerController

    //these are the tilemaps for light and dark, set in the inspector
    public GameObject lightmap;
    public GameObject darkmap;

    public void ActivateDarkWorld()
    {
        lightmap.SetActive(false);
        darkmap.SetActive(true);
    }
    public void ActivateLightWorld()
    {
        darkmap.SetActive(false);
        lightmap.SetActive(true);
    }
}
