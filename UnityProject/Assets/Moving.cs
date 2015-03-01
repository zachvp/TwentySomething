using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour {

	public Vector3 startPoint;
	public Vector3 endPoint;
	public float speed = 0.1f;
	public bool isMoving;

	enum FacingDirection { NEUTRAL, UP, DOWN, LEFT, RIGHT };
	FacingDirection _facingDirection = FacingDirection.NEUTRAL;

	enum MoveState { STOPPED, CHECK_MOVE, MOVING };
	MoveState _moveState;

	RaycastHit2D _checkMove;
	Vector2 _checkMoveDirection;
	float _checkMoveDistance = 32.0f;

	Vector2 _destination;
	float _moveDistance = 1.0f;

	float startTime; 
	float journeyLength;

	//determines collisions
	bool canMove; 

	void Awake() {

	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		//Set the facing direction based on input
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			//Debug.Log(gameObject.name + ": pressed " + KeyCode.UpArrow);

			_facingDirection = FacingDirection.UP;
			_moveState = MoveState.CHECK_MOVE;

			_checkMoveDirection = Vector2.up;
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			//Debug.Log(gameObject.name + ": pressed " + KeyCode.DownArrow);

			_facingDirection = FacingDirection.DOWN;
			_moveState = MoveState.CHECK_MOVE;

			_checkMoveDirection = -1.0f * Vector2.up;
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			//Debug.Log(gameObject.name + ": pressed " + KeyCode.LeftArrow);

			_facingDirection = FacingDirection.LEFT;
			_moveState = MoveState.CHECK_MOVE;

			_checkMoveDirection = -1.0f * Vector2.right;
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			//Debug.Log(gameObject.name + ": pressed " + KeyCode.RightArrow);

			_facingDirection = FacingDirection.RIGHT;
			_moveState = MoveState.CHECK_MOVE;

			_checkMoveDirection = Vector2.right;
		}

		// Set the move state
		if (_moveState == MoveState.CHECK_MOVE) {
			_checkMove = Physics2D.Raycast(gameObject.transform.position, _checkMoveDirection, _checkMoveDistance);
			//Debug.Log(gameObject.name + ": checkMove" + _checkMove.rigidbody.ToString());
			if (_checkMove.rigidbody == null) {
				// can move in the direction
				_moveState = MoveState.MOVING;
			} else {
				// check the type of object in front of the player
			}
		}

		// Handle the move state
		if (_moveState == MoveState.MOVING) {
			_destination = gameObject.transform.position;
			if (_facingDirection == FacingDirection.UP) {
				_destination.y += _moveDistance;
			} else if (_facingDirection == FacingDirection.DOWN) {
				_destination.y -= _moveDistance;
			} else if (_facingDirection == FacingDirection.LEFT) {
				_destination.x -= _moveDistance;
			} else if (_facingDirection == FacingDirection.RIGHT) {
				_destination.x += _moveDistance;
			}

			_moveState = MoveState.STOPPED;
			LerpToDestination();
		}

	}

	void LerpToDestination() {
		gameObject.transform.position = _destination;
	}

	bool CheckTile(int Dir) {
		Vector2 direction = Vector2.zero;
		//this is here to get directions for the raycast.
		if (Dir == 0) direction = Vector2.up;
		if (Dir == 1) direction = Vector2.right;
		if (Dir == 2) direction = Vector2.up * -1;
		if (Dir == 3) direction = Vector2.right * -1;


		RaycastHit2D checkForward = Physics2D.Raycast (gameObject.transform.position, direction, 1.0f);
		Debug.Log (gameObject.name + ": checkforward " + checkForward.ToString());
		if (checkForward.collider.CompareTag ("wall") || checkForward.collider.CompareTag("thing")) {
			canMove = false; 
			Debug.Log("I'M RUNNING INTO A WALL!"); 
		}
		else
		{
			canMove = true;
		}

		if (checkForward.collider.CompareTag("thing")){
			//code for interactibe objects will have to go here. Case by case basis. 
		}
		return canMove;
	}
}
