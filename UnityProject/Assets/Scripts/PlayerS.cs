using UnityEngine;
using System.Collections;

public class PlayerS : MonoBehaviour {

	public Vector3 startPoint;
	public Vector3 endPoint;
	public float speed;
	public bool isMoving;

	private float increment;

	// Use this for initialization
	void Start () {
		startPoint = transform.position;
		endPoint = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (increment <= 1 && isMoving == true) {
			increment += speed / 100;
			Debug.Log ("Moving");
		}
		else {
			isMoving = false;
			Debug.Log("Stopped");
		}
		if(isMoving){ 
			transform.position = Vector3.Lerp(startPoint, endPoint, increment);
		}

		// get ready for some REALLY BAD CODE FOLKS
		// ok i made it a little bit better
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		if(h != 0 && isMoving == false) {
			increment = 0;
			isMoving = true;
			startPoint = transform.position;
			if(h > 0){
				endPoint = new Vector3(transform.position.x + 1,transform.position.y,transform.position.z);
			}
			else{
				endPoint = new Vector3(transform.position.x - 1,transform.position.y,transform.position.z);
			}

		}
		if(v != 0 && isMoving == false) {
			increment = 0;
			isMoving = true;
			startPoint = transform.position;
			if(v > 0){
				endPoint = new Vector3(transform.position.x,transform.position.y + 1,transform.position.z);
			}
			else{
				endPoint = new Vector3(transform.position.x,transform.position.y - 1,transform.position.z);
			}
		}

		// HOPEFULLY I NEVER DO ANYTHING THAT TERRIBLE AGAIN

	}
}
