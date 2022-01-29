using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Vector3 playerPosition;
    Rigidbody2D rb2d;
    public float speed = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = FindObjectOfType<PlayerController>().GetComponent<Transform>().position;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if(playerPosition != null) {
            var direction = Vector3.Normalize(playerPosition - this.transform.position);
            rb2d.velocity = direction * this.speed;
        }
    }
}
