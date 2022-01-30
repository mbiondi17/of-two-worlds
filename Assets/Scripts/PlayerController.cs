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
    }

	void Update() {		
		var mousePos = new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0);
		mousePos = FindObjectOfType<Camera>().ScreenToWorldPoint(mousePos);
		UnityEngine.Debug.Log(mousePos);
		var cos = 1 / (mousePos - this.transform.position).normalized.magnitude;
		var angle = Mathf.Acos(cos) * Mathf.Rad2Deg;
		var shape = particles.shape;
		shape.enabled = true;
		shape.rotation = new Vector3(0, 0, angle);
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		if(gm.GetState() != GameManager.States.darkWorldCombat) {
			Vector2 input = moveAction.ReadValue<Vector2>();

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
