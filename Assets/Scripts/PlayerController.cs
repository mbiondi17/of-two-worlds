using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	[SerializeField]
	private float speed = 5.5f;

	private PlayerInput playerInput;
	private InputAction moveAction;
	private bool collidingWithObstacle;
	
	private Vector2 currInputVector;
	private Vector2 smoothInputVelocity;

	private Rigidbody2D rb2d;
	
	[SerializeField] private float smoothInputSpeed = .05f;

    public enum States { lightWorld, darkWorldCombat, darkWorld };
    private States currentState;

    public void SetState(States state)
    {
        currentState = state;
    }

	// Start is called before the first frame update
    void Start()
    {
        float charHeight = GetComponent<SpriteRenderer>().bounds.extents.y;
		float charWidth = GetComponent<SpriteRenderer>().bounds.extents.x;

		playerInput = GetComponent<PlayerInput>();
		moveAction = playerInput.actions["Movement"];

		rb2d = GetComponent<Rigidbody2D>();
		
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();

		currInputVector = Vector2.SmoothDamp(currInputVector, input, ref smoothInputVelocity, smoothInputSpeed);

		Vector2 movementDelta = currInputVector * speed;

		//if(!this.collidingWithObstacle) {
			rb2d.velocity = new Vector2(movementDelta.x, movementDelta.y ); 
		//}		
    }

	// void OnCollisionEnter2D(Collision2D other) {

	// 	if (other.gameObject.tag == "Obstacle")
	// 		UnityEngine.Debug.Log("hit something");
	// 		this.collidingWithObstacle = true;
	// 		this.forbiddenDirection = 
	// }
}
