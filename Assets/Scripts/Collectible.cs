using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("collided with player");
            PlayerController player = FindObjectOfType<PlayerController>();
            player.currentState = PlayerController.States.darkWorldCombat;
            //spawn enemies
            //lock player in x/y position until enemies are gone
            //allow player to rotate
        }
    }

}
