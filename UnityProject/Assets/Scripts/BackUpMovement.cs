using UnityEngine;
using System.Collections;

public class BackUpMovement : MonoBehaviour {

	/* 1) GetKeyDown
	 * 2) Set Direction
	 * 3) Determine Destination
	 * 4) Check Destination
	 * 5) Move to Destination
	 * 6) If key still down repeat from 3)
	 */

	public float _speed = 0.4f;

	//testing out double tap to run
	public float tapSpeed = 0.5f;
	private float lastTapTime = 0f; 

	enum FacingDirection { NEUTRAL, UP, DOWN, LEFT, RIGHT };
	FacingDirection _facingDirection;
	
	enum MoveState { STOPPED, CHECK_MOVE, MOVE, MOVING };
	MoveState _moveState;

	enum SenseState { NONE, INTERACT };
	SenseState _senseState;
	bool runReleased;

	RaycastHit2D _checkMove;
	Vector2 _checkMoveDirection;
	float _checkMoveDistance = 1.0f;
	
	Vector3 _destination;
	float _moveDistance = 1.0f;
	
	float _distanceToDestination = 0.0f;
	float _distanceCoveredToDestination = 0.0f;
	
	void Awake() {
		_moveState = MoveState.STOPPED;
		_senseState = SenseState.NONE;
		lastTapTime = 0f; 
	}
	
	// Use this for initialization
	void Start () {
		_destination = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		//check if player has let go of the move button
		if(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)) {
			runReleased = true;
			if((Time.time - lastTapTime) < tapSpeed){
				_speed = 1f; 
				Debug.Log("Speed = " + _speed); 
			}
			else{
				_speed = 0.2f; 

			}
			lastTapTime = Time.time;
			//_speed /= 3f; 
			Debug.Log("Speed = " + _speed); 
		}
			

		if(Input.GetKeyDown(KeyCode.UpArrow) && _moveState == MoveState.STOPPED) {

			_facingDirection = FacingDirection.UP;
			_moveState = MoveState.CHECK_MOVE;
			_checkMoveDirection = Vector2.up;

		} else if(Input.GetKeyDown(KeyCode.DownArrow) && _moveState == MoveState.STOPPED) {

			_checkMoveDirection = -1.0f * Vector2.up;
			_facingDirection = FacingDirection.DOWN;
			_moveState = MoveState.CHECK_MOVE;

		} else if(Input.GetKeyDown(KeyCode.RightArrow) && _moveState == MoveState.STOPPED) {

			_facingDirection = FacingDirection.RIGHT;
			_moveState = MoveState.CHECK_MOVE;
			_checkMoveDirection = Vector2.right;

		} else if(Input.GetKeyDown(KeyCode.LeftArrow) && _moveState == MoveState.STOPPED) {

			_facingDirection = FacingDirection.LEFT;
			_moveState = MoveState.CHECK_MOVE;
			_checkMoveDirection = -1.0f * Vector2.right;

		}

		// Set the move state
		if (_moveState == MoveState.CHECK_MOVE) {
			runReleased = false;
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
					_senseState = SenseState.NONE;
				}
				if (objectTag.Equals("thing")) {
					_senseState = SenseState.INTERACT;
				}
			}
		}


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

			//check if key is still held down
			if(!runReleased) 
				_moveState = MoveState.CHECK_MOVE;
			else 
				_moveState = MoveState.STOPPED;

		}
	}
	
}
