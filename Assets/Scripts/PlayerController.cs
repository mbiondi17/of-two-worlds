using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	#region Movement and Controls
	[SerializeField]
	private float speed = 5.5f;
	[SerializeField] private float smoothInputSpeed = .05f;
	private PlayerInput playerInput;
	private InputAction moveAction;
	private Vector2 currInputVector;
	private Vector2 smoothInputVelocity;
	#endregion

	#region Animation Control
	private Animator anim;
	bool isMovingUp = false;
	bool isMovingDown = false;
	bool isMovingLeft = false;
	bool isMovingRight = false;
	bool isNotMoving = false;
	#endregion
	private Rigidbody2D rb2d;
	private GameManager gm;

	private ParticleSystem particles;
	

	// Start is called before the first frame update
    void Start()
    {
		gm = FindObjectOfType<GameManager>();
		playerInput = GetComponent<PlayerInput>();
		moveAction = playerInput.actions["Movement"];
		rb2d = GetComponent<Rigidbody2D>();
		particles = GetComponent<ParticleSystem>();
		anim = GetComponent<Animator>();
    }

	void Update() {		
		if(particles.isEmitting) {
			var mousePos = new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0);
			mousePos = FindObjectOfType<Camera>().ScreenToWorldPoint(mousePos);
			bool greaterThanOneEighty = mousePos.x > this.transform.position.x;
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y) - new Vector2(this.transform.position.x, this.transform.position.y);
			var angleOfEmission = Vector2.Angle(Vector2.up, mousePos2D);
			if(greaterThanOneEighty) {
				angleOfEmission = 360 - angleOfEmission;
			}
			UnityEngine.Debug.Log(angleOfEmission);
			var shape = particles.shape;
			shape.enabled = true;
			shape.rotation = new Vector3(0, 0, angleOfEmission);
		}
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		if(gm.GetState() != GameManager.States.darkWorldCombat) {
			Vector2 input = moveAction.ReadValue<Vector2>();
			currInputVector = Vector2.SmoothDamp(currInputVector, input, ref smoothInputVelocity, smoothInputSpeed);
			Vector2 movementDelta = currInputVector * speed * Time.deltaTime;
			//rb2d.velocity = new Vector2(movementDelta.x, movementDelta.y );
			rb2d.MovePosition(rb2d.position + movementDelta);

			//animation control logic
			resetDirection();
			if(input != Vector2.zero) {
				if(input.x != 0) {
					if(Mathf.Abs(input.x) > Mathf.Abs(input.y) {
						is
					})
				}
				else if(input.y != 0) {
					//don't need to account for both directions here
					isMovingUp = input.y > 0;
					isMovingDown = input.y < 0;
				}
			} else {
				isNotMoving = true;
			}

			else if(input.x < 0) isMovingLeft = true;
			else if(input.y > 0) isMovingUp = true;
			else if(input.y < 0) isMovingDown = true;
			else isNotMoving = true;
		}
    }

	//MVB: obviously an event-based system would be better here, but no time.
	public void SetGameState(GameManager.States newState) {
		gm.SetState(newState);
		if(newState == GameManager.States.darkWorldCombat) 
		{
			rb2d.velocity = Vector2.zero;
		}
	}

	private void resetDirection() {
		isMovingDown = false;
		isMovingUp = false;
		isMovingLeft = false;
		isMovingRight = false;
	}
	
}
