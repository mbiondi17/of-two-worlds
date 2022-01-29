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
	
	private Vector2 currInputVector;
	private Vector2 smoothInputVelocity;
	
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
		
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();

		currInputVector = Vector2.SmoothDamp(currInputVector, input, ref smoothInputVelocity, smoothInputSpeed);

		Vector2 movementDelta = currInputVector * speed * Time.deltaTime;
		this.transform.position += new Vector3(movementDelta.x, movementDelta.y, 0); 
    }
}
