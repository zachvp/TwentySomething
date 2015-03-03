using UnityEngine;
using System.Collections;

public class Notes : MonoBehaviour {
	int currentLine;
	public TextAsset notes;
	public string[] Lines;
	private string myLine;
	bool end = false;
	bool active = false;


	void Start () {
		currentLine = 0;

		if(notes != null) {
			Lines = ( notes.text.Split('^'));
			myLine = Lines[currentLine];
		}

		//Debug.Log(myLine);
	}
	

	void Update () {
		// gameObject.GetComponent<TextMesh>().text = myLine;
		//Debug.Log(myLine);
		if(active && Input.GetKeyDown(KeyCode.Space) && !end) {
			NextLine();
		}
	}

	void NextLine() {

		if(currentLine > Lines.Length) 
		{
			myLine = "END";
			end = true;
			active = false;
		}
		else 
		{	
			myLine = Lines[currentLine++];
			//Debug.Log(myLine);
		}
	}

	public void Display() {
		Debug.Log(myLine);
		active = true;
	}

}
