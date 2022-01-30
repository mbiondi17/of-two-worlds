using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]

    private InputAction toggleAction; //get input to toggle between light and dark
    private bool collidingWithObstacle;

    private LightToDark lighttodark;


    public enum States { lightWorld, darkWorldCombat, darkWorld };
    private States currentState;

    public void SetState(States state)
    {
        currentState = state;
    }
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
	#endregion
	private Rigidbody2D rb2d;
	private GameManager gm;
	private ParticleSystem particles;
	
	#region Things that Should Not Be
	private bool wasInCombat = false;
	#endregion

    public States GetState()
    {
        return currentState;
    }

	// Start is called before the first frame update
    void Start()
    {
        lighttodark = FindObjectOfType<LightToDark>();
        float charHeight = GetComponent<SpriteRenderer>().bounds.extents.y;
		float charWidth = GetComponent<SpriteRenderer>().bounds.extents.x;

		playerInput = GetComponent<PlayerInput>();
		moveAction = playerInput.actions["Movement"];
        toggleAction = playerInput.actions["ToggleLightDark"];

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
			var shape = particles.shape;
			shape.enabled = true;
			shape.rotation = new Vector3(0, 0, angleOfEmission);
		}
		if(gm.GetState() != GameManager.States.darkWorldCombat) {
			if(wasInCombat) {
				//in the first frame back from combat, stop the particles, and any other work that needs to be done.
				var em = particles.emission;
				em.enabled = false;
				wasInCombat = false;
			}
		}

	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if(toggleAction.triggered) //if key to switch between light and dark is pressed
        {
            if(currentState == PlayerController.States.lightWorld)
            {
                currentState = PlayerController.States.darkWorld;
                lighttodark.ActivateDarkWorld();
            }
            else if (currentState == PlayerController.States.darkWorld)
            {
                currentState = PlayerController.States.lightWorld;
                lighttodark.ActivateLightWorld();
            }
        }
        Vector2 input = moveAction.ReadValue<Vector2>();
		if(gm.GetState() != GameManager.States.darkWorldCombat) {
			currInputVector = Vector2.SmoothDamp(currInputVector, input, ref smoothInputVelocity, smoothInputSpeed);
			Vector2 movementDelta = currInputVector * speed * Time.deltaTime;
			//rb2d.velocity = new Vector2(movementDelta.x, movementDelta.y );
			rb2d.MovePosition(rb2d.position + movementDelta);

			//animation control logic
			resetDirection();
			if(input != Vector2.zero) {
				if(input.x != 0) {
					if(Mathf.Abs(input.x) > Mathf.Abs(input.y)) {
						isMovingLeft = input.x < 0;
						isMovingRight = input.x > 0;
					} else {
						isMovingUp = input.y > 0;
						isMovingDown = input.y < 0;
					}
				}
				else if(input.y != 0) {
					//don't need to account for both directions here
					isMovingUp = input.y > 0;
					isMovingDown = input.y < 0;
				}
			}
			anim.SetBool("isMovingUp", isMovingUp);
			anim.SetBool("isMovingDown", isMovingDown);
			anim.SetBool("isMovingLeft", isMovingLeft);
			anim.SetBool("isMovingRight", isMovingRight);
		}
    }

	//MVB: obviously an event-based system would be better here, but no time.
	public void SetGameState(GameManager.States newState) {
		gm.SetState(newState);
		resetDirection();
		var em = particles.emission;
		em.enabled = true;
		if(newState == GameManager.States.darkWorldCombat) 
		{
			wasInCombat = true;
			rb2d.velocity = Vector2.zero;
		}
	}

	private void resetDirection() {
		isMovingDown = false;
		isMovingUp = false;
		isMovingLeft = false;
		isMovingRight = false;
		anim.SetBool("isMovingUp", false);
		anim.SetBool("isMovingDown", false);
		anim.SetBool("isMovingLeft", false);
		anim.SetBool("isMovingRight", false);
	}

	void OnParticleCollision(GameObject other) {
		if(other.tag == "Enemy") {
			Destroy(other.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Enemy") {
			Destroy(other.gameObject);
			Destroy(this.gameObject); // actually should respawn & kick out of the light
		}
	}
	
}
