using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Vector3 playerPosition;
    Rigidbody2D rb2d;
    public float speed = 1.2f;

    public AudioSource wing1;
    public AudioSource wing2;
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = FindObjectOfType<PlayerController>().GetComponent<Transform>().position;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void PlayrandomWingSound()
    {
        int randnum = Random.Range(1, 5); //1 2 3 4
        if (randnum == 1)
        {
            wing1.Play();
        }
        else if (randnum == 2)
        {
            wing2.Play();
        }
        //if 3 or 4 is chosen, no sound plays
    }
        // Update is called once per frame
        void Update()
    {
        if(!wing1.isPlaying && !wing2.isPlaying)
        {
            PlayrandomWingSound();
        }
    }

    void FixedUpdate() {
        if(playerPosition != null) {
            var direction = Vector3.Normalize(playerPosition - this.transform.position);
            rb2d.velocity = direction * this.speed;
        }
    }
}
