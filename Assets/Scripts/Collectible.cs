using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private LightToDark lighttodark;

    private void Start()
    {
        lighttodark = FindObjectOfType<LightToDark>();
        lighttodark.AddToList(this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            //set playercontroller state to darkWorldCombat
            player.SetState(PlayerController.States.darkWorldCombat);

        }
    }

}
