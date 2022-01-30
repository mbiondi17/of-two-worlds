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

	private Rigidbody2D rb2d;
	private GameManager gm;
	

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
    }

	void Update() {

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

			Vector2 movementDelta = currInputVector * speed;

			rb2d.velocity = new Vector2(movementDelta.x, movementDelta.y ); 
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
	
}
