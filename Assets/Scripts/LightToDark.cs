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

    public List<Collectible> collectiblelist;


    public void Start()
    {
        animator = GetComponent<Animator>();
        collectiblelist = new List<Collectible>();
    }

    public void AddToList(Collectible obj)
    {
        collectiblelist.Add(obj);
    }

    public void ActivateDarkWorld()
    {
        animator.Play("FadetoClear", 0, 0.0f);
        lightmap.SetActive(false);
        darkmap.SetActive(true);
        foreach (Collectible item in collectiblelist) //collectibles active in dark world
        {
            item.gameObject.SetActive(true);
        }
        //add this next
        //start coroutine that does activate light world when time is up
        print("before coroutine");
        StartCoroutine(Wait());
    }
    public void ActivateLightWorld()
    {
        animator.Play("FadetoClear", 0, 0.0f);
        darkmap.SetActive(false);
        lightmap.SetActive(true);
        foreach (Collectible item in collectiblelist)
        {
            item.gameObject.SetActive(false);
        }  
    }

    IEnumerator Wait()
    {
        print("in coroutine");
        //yield return new WaitForSeconds(1000f); //wait for 2 seconds
        print(Time.time);
        yield return new WaitForSecondsRealtime(5);
        print(Time.time);
        print("after coroutine");
        ActivateLightWorld();
    }

    public void Update()
    {

    }
}
