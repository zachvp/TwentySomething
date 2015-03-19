using UnityEngine;
using System.Collections;

public class Sprinkles : MonoBehaviour {

	public PaintCanvas canvas;
	
	public float width;


	// Use this for initialization
	void Start () {
		canvas = GameObject.Find ("PaintCanvas").GetComponent<PaintCanvas>();

	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnMouseOver() {
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)){
			canvas.holdingPaint = false;
			canvas.holdingSprinkles = true;
			//canvas.sprinkleSprite = GetComponent<SpriteRenderer>().sprite;
			canvas.sprinkleSprite = this;

		}
		
	}

	public void DisableCollider() {
		GetComponent<BoxCollider> ().enabled = false;
	}
}
