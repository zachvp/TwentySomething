using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour {	
	public float _speed = 0.2f;
	bool prepedtoInteract = false;

	enum FacingDirection { NEUTRAL, UP, DOWN, LEFT, RIGHT };
	FacingDirection _facingDirection;

	enum MoveState { STOPPED, CHECK_MOVE, MOVE, MOVING };
	MoveState _moveState;

	RaycastHit2D _checkMove;
	Vector2 _checkMoveDirection;
	float _checkMoveDistance = 1.0f;

	Vector3 _destination;
	float _moveDistance = 1.0f;
	
	float _distanceToDestination = 0.0f;
	float _distanceCoveredToDestination = 0.0f;

	void Awake() {
		_moveState = MoveState.STOPPED;
	}

	// Use this for initialization
	void Start () {
		_destination = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Set the facing direction based on input
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		float axisThreshold = 0.0f;

		if(v > axisThreshold) {
			//Debug.Log(gameObject.name + ": pressed " + KeyCode.UpArrow);
				
			_facingDirection = FacingDirection.UP;
			_moveState = MoveState.CHECK_MOVE;
				
				_checkMoveDirection = Vector2.up;
		} else if (v < axisThreshold) {
				_checkMoveDirection = -1.0f * Vector2.up;
				_facingDirection = FacingDirection.DOWN;
				_moveState = MoveState.CHECK_MOVE;
		} else if (h > axisThreshold) {
			//Debug.Log(gameObject.name + ": pressed " + KeyCode.RightArrow);
				
			_facingDirection = FacingDirection.RIGHT;
			_moveState = MoveState.CHECK_MOVE;
				
			_checkMoveDirection = Vector2.right;
		} else if (h < axisThreshold) {
			//Debug.Log(gameObject.name + ": pressed " + KeyCode.LeftArrow);
				
			_facingDirection = FacingDirection.LEFT;
			_moveState = MoveState.CHECK_MOVE;
				
			_checkMoveDirection = -1.0f * Vector2.right;
		}

		// Set the move state
		if (_moveState == MoveState.CHECK_MOVE) {
			_checkMove = Physics2D.Raycast(gameObject.transform.position, _checkMoveDirection, _checkMoveDistance);
			//Debug.Log(gameObject.name + ": checkMove" + _checkMove.rigidbody.ToString());
			if (_checkMove.rigidbody == null) {
				// can move in the direction
				_moveState = MoveState.MOVE;
			} else {
				// check the type of object in front of the player
				_moveState = MoveState.STOPPED;
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
			}
		}
		
		// Handle the move state
		if (_moveState == MoveState.MOVE) {
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

			_distanceToDestination = Vector3.Distance(gameObject.transform.position, _destination);

			_moveState = MoveState.MOVING;
		} else if (_moveState == MoveState.MOVING) {
			LerpToDestination();
		}

	}

	void LerpToDestination() {
		_distanceCoveredToDestination += Time.deltaTime * _speed;
		float fracJourney = _distanceCoveredToDestination / _distanceToDestination;

		gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, _destination, fracJourney);

		if ((_destination - gameObject.transform.position).sqrMagnitude < 0.01f) {
			gameObject.transform.position = _destination;
			_distanceCoveredToDestination = 0.0f;
			_moveState = MoveState.STOPPED;
		}
	}
}
