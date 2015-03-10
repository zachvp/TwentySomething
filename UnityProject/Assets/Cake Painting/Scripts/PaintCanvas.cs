using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PaintCanvas : MonoBehaviour {

	public Paintball ball;
	private bool instantiated = false;

	public Material trailMat;

	public List<Paintball> paintballList;

	public float zpos;

	public float currentSize;


	// Use this for initialization
	void Start () {
		paintballList = new List<Paintball> ();
		zpos = 0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver() {
		if (Input.GetMouseButtonDown(0)){
			if(!instantiated){
				Paintball newBall = Instantiate(ball, new Vector3(Input.mousePosition.x, Input.mousePosition.y, zpos), Quaternion.identity) as Paintball;
				newBall.ChangeWidth(currentSize);

				paintballList.Add (newBall);
				
				/*foreach(Paintball b in paintballList)
				{
					float temp = b.transform.position.z - 1.0f;
					b.transform.position = new Vector3(b.transform.position.x, b.transform.position.y, temp);
				}*/

				TrailRenderer trailRenderer = newBall.GetComponent<TrailRenderer> ();
				trailRenderer.material = trailMat;
				instantiated = true;


				zpos -= 0.01f;
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			instantiated = false;
		}
	}
}
