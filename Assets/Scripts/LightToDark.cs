using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightToDark : MonoBehaviour
{

    public GameManager gm;
    //these methods are called when the player presses space
    //see code in PlayerController

    //these are the tilemaps for light and dark, set in the inspector
    public GameObject lightmap;
    public GameObject darkmap;
    public Animator animator;

    public AudioSource lightAudio;
    public AudioSource darkAudio;
    public AudioSource worldtransition;

    public bool inDarkWorld;
    public float lastDarkSwitch = 0.0f;
    public float darkTimer = 5.0f;

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
    
    public void RemoveFromList(Collectible obj)
    {
        collectiblelist.Remove(obj);
    }

    public void ActivateDarkWorld()
    {
        animator.Play("FadetoClear", 0, 0.0f);
        worldtransition.Play();
        lightmap.SetActive(false);
        darkmap.SetActive(true);
        darkmap.GetComponentInChildren<CompositeCollider2D>().GenerateGeometry();
        lightAudio.volume = 0;
        darkAudio.volume = 0.5f;
        foreach (Collectible item in collectiblelist) //collectibles active in dark world
        {
            item.gameObject.SetActive(true);
        }
        inDarkWorld = true;
        lastDarkSwitch = Time.time;
    }
    public void ActivateLightWorld()
    {
        worldtransition.Play();
        inDarkWorld = false; //this should run either after 5 seconds or on timer interval elapse;
        animator.Play("FadetoClear", 0, 0.0f);
        darkmap.SetActive(false);
        lightmap.SetActive(true);
        lightmap.GetComponentInChildren<CompositeCollider2D>().GenerateGeometry();
        lightAudio.volume = 0.5f;
        darkAudio.volume = 0;
        foreach (Collectible item in collectiblelist)
        {
            item.gameObject.SetActive(false);
        }  
    }

    // IEnumerator Wait()
    // {
    //     print("in coroutine");
    //     //yield return new WaitForSeconds(1000f); //wait for 2 seconds
    //     print(Time.time);
    //     yield return new WaitForSecondsRealtime(5);
    //     print(Time.time);
    //     print("after coroutine");
    //     ActivateLightWorld();
    // }

    public void Update()
    {
        //only need to switch back if they're still in the dark world. If they switch back themselves, nbd.
        if(inDarkWorld && gm.GetState() != GameManager.States.darkWorldCombat) {
            if(Time.time >= lastDarkSwitch + darkTimer) {
                ActivateLightWorld();
                gm.SetState(GameManager.States.lightWorld);
            }
        }
    }
}
