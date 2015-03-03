using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour {

	public Vector3 startPoint;
	public Vector3 endPoint;
	public float speed = 0.1f;
	public bool isMoving;
	public bool canMove = true;
	bool prepedtoInteract = false;

	enum FacingDirection { NEUTRAL, UP, DOWN, LEFT, RIGHT };
	FacingDirection _facingDirection;

	enum MoveState { STOPPED, CHECK_MOVE, MOVING };
	MoveState _moveState;

	RaycastHit2D _checkMove;
	Vector2 _checkMoveDirection;
	float _checkMoveDistance = 1.0f;

	Vector2 _destination;
	float _moveDistance = 1.0f;

	float startTime; 
	float journeyLength;

	void Awake() {

	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

<<<<<<< Updated upstream
			//Set the facing direction based on input
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
=======
		//Set the facing direction based on input
		/*
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		if (v != 0) {
			if(v > 0) {
>>>>>>> Stashed changes
				//Debug.Log(gameObject.name + ": pressed " + KeyCode.UpArrow);
				
				_facingDirection = FacingDirection.UP;
				_moveState = MoveState.CHECK_MOVE;
<<<<<<< Updated upstream
				
				_checkMoveDirection = Vector2.up;
			} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
=======

				_checkMoveDirection = Vector2.up;
			}
			else {
>>>>>>> Stashed changes
				//Debug.Log(gameObject.name + ": pressed " + KeyCode.DownArrow);
				
				_facingDirection = FacingDirection.DOWN;
				_moveState = MoveState.CHECK_MOVE;
<<<<<<< Updated upstream
				
				_checkMoveDirection = -1.0f * Vector2.up;
			} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				//Debug.Log(gameObject.name + ": pressed " + KeyCode.LeftArrow);
				
				_facingDirection = FacingDirection.LEFT;
				_moveState = MoveState.CHECK_MOVE;
				
				_checkMoveDirection = -1.0f * Vector2.right;
			} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
=======

				_checkMoveDirection = -1.0f * Vector2.up;
			}
		}
		if (h !=0 ) {
			if (h > 0) {
>>>>>>> Stashed changes
				//Debug.Log(gameObject.name + ": pressed " + KeyCode.RightArrow);
				
				_facingDirection = FacingDirection.RIGHT;
				_moveState = MoveState.CHECK_MOVE;
				
				_checkMoveDirection = Vector2.right;
			}
<<<<<<< Updated upstream
=======
			else {
				//Debug.Log(gameObject.name + ": pressed " + KeyCode.LeftArrow);
				
				_facingDirection = FacingDirection.LEFT;
				_moveState = MoveState.CHECK_MOVE;
				
				_checkMoveDirection = -1.0f * Vector2.right;
			}
		}*/


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
>>>>>>> Stashed changes


		// Set the move state
		if (_moveState == MoveState.CHECK_MOVE) {
			_checkMove = Physics2D.Raycast(gameObject.transform.position, _checkMoveDirection, _checkMoveDistance);
			//Debug.Log(gameObject.name + ": checkMove" + _checkMove.rigidbody.ToString());
			if (_checkMove.rigidbody == null) {
				// can move in the direction
				_moveState = MoveState.MOVING;
			} else {
				// check the type of object in front of the player
				string objectTag = _checkMove.rigidbody.tag;
				if (objectTag.Equals("wall")) {

				}
				if (objectTag.Equals("thing")) {
					prepedtoInteract = true;
				}
			}
		}

		//
		if(prepedtoInteract) {
			if(Input.GetKeyDown(KeyCode.Space)) {
				
				_checkMove.transform.GetComponent<Notes>().Display();
				canMove = false;
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
}
