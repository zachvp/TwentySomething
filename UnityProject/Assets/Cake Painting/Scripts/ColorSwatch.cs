using UnityEngine;
using System.Collections;

public class ColorSwatch : MonoBehaviour {

	public PaintCanvas canvas;

	public string makeupName;

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
			canvas.trailMat = renderer.material;
			canvas.currentSize = width;
		}

	}
}
