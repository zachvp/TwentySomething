using UnityEngine;
using System.Collections;

public class Paintball : MonoBehaviour {

	private Vector3 mousePosition;
	public float moveSpeed;

	public bool canFollow = true;

	public PaintCanvas canvas;

	public float distanceTravelled;
	Vector3 lastPosition;


	
	// Use this for initialization
	void Start () {
		lastPosition = transform.position;
	}


	
	// Update is called once per frame
	void Update () {

		distanceTravelled += Vector3.Distance (transform.position, lastPosition);
		lastPosition = transform.position;

		//if (Input.GetMouseButtonDown (0) || Input.GetMouseButton (0) && canFollow) {
		if (Input.GetMouseButton (0) && canFollow) {
			MouseFollow();
		}

		if (Input.GetMouseButtonUp (0)) {
			//rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			canFollow = false;
		}
		
	}
	
	
	void MouseFollow () {
		mousePosition = Input.mousePosition;
		float originalZ = transform.position.z;
		mousePosition.z = 10;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
		transform.position = new Vector3(mousePosition.x, mousePosition.y, originalZ);
	}

	public void ChangeWidth (float f) {
		GetComponent<TrailRenderer> ().startWidth = f;
		GetComponent<TrailRenderer> ().endWidth = f;
	}


	
}