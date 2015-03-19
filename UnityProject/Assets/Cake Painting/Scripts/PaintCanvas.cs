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

	public bool holdingPaint;
	public bool holdingSprinkles;

	public Sprinkles sprinkleSprite;

	public bool placed;


	// Use this for initialization
	void Start () {
		paintballList = new List<Paintball> ();
		zpos = 0f;
		holdingPaint = false;
		holdingSprinkles = false;
		placed = false; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver() {
		if (Input.GetMouseButtonDown(0)){
			if(!instantiated){
				if(holdingPaint){
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
				}
				if(holdingSprinkles){
					print ("adjkfjsd");
					/*Vector3 v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					v3.z = zpos;
					v3 = Camera.main.ScreenToWorldPoint(v3);*/
					Vector3 v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zpos);
					v3 = Camera.main.ScreenToWorldPoint(v3);
					v3.z = zpos;
					Sprinkles newSprinkles = Instantiate (sprinkleSprite, v3, Quaternion.identity) as Sprinkles;
					newSprinkles.DisableCollider();
				}


				zpos -= 0.01f;
				instantiated = true;
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			instantiated = false;
		}
	}
}
