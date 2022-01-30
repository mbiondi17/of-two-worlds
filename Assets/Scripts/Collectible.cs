using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public LightToDark lighttodark;
    public AudioSource alert;

    private void Start()
    {
        alert = GetComponent<AudioSource>();
        if (lighttodark == null) lighttodark = FindObjectOfType<LightToDark>();
        lighttodark.AddToList(this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            //set playercontroller state to darkWorldCombat
            player.SetGameState(GameManager.States.darkWorldCombat);
            alert.Play();

            //register that the player has picked this up
            var gm = FindObjectOfType<GameManager>();
            gm.collectiblesRetrieved.Add(this.GetComponent<SpriteRenderer>().sprite);

            lighttodark.RemoveFromList(this);
            Destroy(this.gameObject);
        }
        
    }

}
