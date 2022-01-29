using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            //set playercontroller state to darkWorldCombat
            player.SetGameState(GameManager.States.darkWorldCombat);
        }
        Destroy(this.gameObject);
    }

}
