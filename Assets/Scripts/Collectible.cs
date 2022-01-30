using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public LightToDark lighttodark;

    private void Start()
    {
        if(lighttodark == null) lighttodark = FindObjectOfType<LightToDark>();
        lighttodark.AddToList(this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            //set playercontroller state to darkWorldCombat
            player.SetGameState(GameManager.States.darkWorldCombat);
        }
        lighttodark.RemoveFromList(this);
        Destroy(this.gameObject);
    }

}
