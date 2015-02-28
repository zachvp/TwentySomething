using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour {

	public Vector3 startPoint;
	public Vector3 endPoint;
	public float speed = 0.1f;
	public bool isMoving;

	int DirFacing;

	//determines collisions
	bool canMoveNorth;
	bool canMoveEast;
	bool canMoveSouth; 
	bool canMoveWest;

	// Use this for initialization
	void Start () {
		DirFacing = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//Moving Up
		if (Input.GetKeyDown(Up)) {
			DirFacing = 0;
			startPoint = transform.position;
			startTime = Time.time;
			endPoint = new Vector3(transform.position.x,transform.position.y + 1);
		}
		if (Input.KeyDown(Up)) {
			if(CheckTile(0)) {
				isMoving = true;
				float distCovered = (Time.time - startTime) * speed;
				float fracJourney = distCovered / journeyLength;
				transform.position = Vector3.Lerp(startPoint,endPoint,fracJourney);
			}
		}
	}

	bool CheckTile(int Dir) {

	}
}
