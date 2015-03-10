using UnityEngine;
using System.Collections;

public class Reload : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.X)) {
			Application.LoadLevel(Application.loadedLevel);
		}
	
	}
}
