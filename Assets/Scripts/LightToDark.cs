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
    public Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ActivateDarkWorld()
    {
        animator.Play("FadetoClear", 0, 0.0f);
        lightmap.SetActive(false);
        darkmap.SetActive(true);
        //add this next
        //start coroutine that does activate light world when time is up
    }
    public void ActivateLightWorld()
    {
        animator.Play("FadetoClear", 0, 0.0f);
        darkmap.SetActive(false);
        lightmap.SetActive(true);
    }
}
