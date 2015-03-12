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

	public Sprite[] sprites; 
	SpriteRenderer myRenderer; 

	//testing out double tap to run
	public float tapSpeed = 0.5f;
	private float lastTapTime = 0f; 

	private enum FacingDirection { NEUTRAL, UP, DOWN, LEFT, RIGHT };
	private FacingDirection _facingDirection;
	
	private enum MoveState { STOPPED, CHECK_MOVE, MOVE, MOVING };
	private  MoveState _moveState;

	private enum SenseState { NONE, CHECK, INTERACT, INTERACTING, END_INTERACTION };
	private SenseState _senseState;

	private RaycastHit2D _checkMove;
	private Vector2 _checkMoveDirection;
	private float _checkMoveDistance = 1.0f;
	
	private Vector3 _destination;
	private float _moveDistance = 1.0f;
	
	private float _distanceToDestination = 0.0f;
	private float _distanceCoveredToDestination = 0.0f;

	// Component references
	private TSDialogueController _dialogueController;

	// Other references
	private TSDialogueThing _dialogueThing;
	
	void Awake() {
		_moveState = MoveState.STOPPED;
		_senseState = SenseState.NONE;
		lastTapTime = 0f; 
	}
	
	// Use this for initialization
	void Start () {
		_destination = gameObject.transform.position;

		myRenderer = GetComponent<SpriteRenderer>(); 
		_dialogueController = GetComponent<TSDialogueController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (_moveState == MoveState.STOPPED) {
			if (_senseState == SenseState.NONE) {
				if(Input.GetKey(KeyCode.UpArrow)) {
					_facingDirection = FacingDirection.UP;
					_moveState = MoveState.CHECK_MOVE;

					_checkMoveDirection = Vector2.up;
				
				} else if(Input.GetKey(KeyCode.DownArrow)) {
					_facingDirection = FacingDirection.DOWN;
					_moveState = MoveState.CHECK_MOVE;

					_checkMoveDirection = -Vector2.up;
				
				} else if(Input.GetKey(KeyCode.RightArrow)) {
					_facingDirection = FacingDirection.RIGHT;
					_moveState = MoveState.CHECK_MOVE;

					_checkMoveDirection = Vector2.right;
				
				} else if(Input.GetKey(KeyCode.LeftArrow)) {
					_facingDirection = FacingDirection.LEFT;
					_moveState = MoveState.CHECK_MOVE;

					_checkMoveDirection = -Vector2.right;
				
				} else {
					// no movement buttons are pressed
				}
			}

			// set sense state
			if (Input.GetKeyDown(KeyCode.Space)) {
				//Debug.Log(gameObject.name + ": pressed space " + _senseState.ToString());
				
				if (_senseState == SenseState.NONE) {
					//Debug.Log(gameObject.name + ": check interaction");
					
					_senseState = SenseState.CHECK;
				} else if (_senseState == SenseState.INTERACTING) {
					//Debug.Log (gameObject.name + ": end interaction");
					
					_senseState = SenseState.END_INTERACTION;
				} else {
					_senseState = SenseState.NONE;
				}
			}

			// set the sprite
			switch (_facingDirection) {
				case FacingDirection.NEUTRAL: 
					myRenderer.sprite = sprites[0]; 
					break;
				case FacingDirection.DOWN: 
					myRenderer.sprite = sprites[0]; 
					break;
				case FacingDirection.UP: 
					myRenderer.sprite = sprites[2]; 
					break; 
				case FacingDirection.LEFT:
					myRenderer.sprite = sprites[3]; 
					break;
				case FacingDirection.RIGHT:
					myRenderer.sprite = sprites[1]; 
					break; 
			}
		}

		// Set the move state
		if (_moveState == MoveState.CHECK_MOVE) {
			CheckFrontObject();

			if (_checkMove) {
				// can move in the direction
				// check the type of object in front of the player
				_moveState = MoveState.STOPPED;
				
				if (_checkMove.collider.CompareTag("wall")) {
					// Debug.Log (gameObject.name + ": wall ahead!");
					_senseState = SenseState.NONE;
				}
			} else {
				//Debug.Log (gameObject.name + ": it's so null!");
				_moveState = MoveState.MOVE;
			}
		} else if (_moveState == MoveState.MOVE) {
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

			// check if entity is at destination
			if ((_destination - gameObject.transform.position).sqrMagnitude < 0.01f) {
				gameObject.transform.position = _destination;
				_distanceCoveredToDestination = 0.0f;

				_moveState = MoveState.STOPPED;
			}
		}

		// check the sense states
		if (_senseState == SenseState.CHECK) {
			CheckFrontObject();

			if (_checkMove) {
				if(_checkMove.collider.CompareTag("thing")) {
					//Debug.Log (gameObject.name + ": Interact with thing");
					_senseState = SenseState.INTERACT;

					_dialogueThing = _checkMove.collider.GetComponent<TSDialogueThing>();
				} else {
					_senseState = SenseState.NONE;
				}
			}
		} else if (_senseState == SenseState.INTERACT) {
			//Debug.Log (gameObject.name + ": Interacting");
			_senseState = SenseState.INTERACTING;

			_dialogueController.Parse(_dialogueThing.filename);
		} else if (_senseState == SenseState.END_INTERACTION) {
			//Debug.Log(gameObject.name + ": End interaction");

			_moveState = MoveState.STOPPED;
			_senseState = SenseState.NONE;

			_dialogueController.EndDialogue();
		}
	}

	private void CheckFrontObject() {
		_checkMove = Physics2D.Raycast(gameObject.transform.position, _checkMoveDirection, _checkMoveDistance);
	}
	
	private void LerpToDestination() {
		_distanceCoveredToDestination += Time.deltaTime * _speed;
		float fracJourney = _distanceCoveredToDestination / _distanceToDestination;
		gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, _destination, fracJourney);

		switch (_facingDirection) {
		case FacingDirection.NEUTRAL: 
			myRenderer.sprite = sprites[0]; 
			break;
		case FacingDirection.DOWN: 
			myRenderer.sprite = sprites[4]; 
			break;
		case FacingDirection.UP: 
			myRenderer.sprite = sprites[6]; 
			break; 
		case FacingDirection.LEFT:
			myRenderer.sprite = sprites[7]; 
			break;
		case FacingDirection.RIGHT:
			myRenderer.sprite = sprites[5]; 
			break;
		}
	}
	
}
