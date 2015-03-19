using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueOption : MonoBehaviour {

	//this depends on there being an array of options! i think it would go somewhere else (like on the actual
	//dialogue gameobject) but we can switch that to GetComponent later

	public List<string> options;
	public string optionOutput;

	public Text currentDisplay;

	// Use this for initialization
	void Start () {

		currentDisplay = gameObject.GetComponent<Text>();

		//put options into output string
		for(int i = 0; i < options.Count; i++){
			if(i == options.Count - 1){
				optionOutput += options[i];
			}
			else{
				optionOutput += (options[i] + "\n");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		//display
		currentDisplay.text = optionOutput;
	}

	void OnMouseOver () {
		currentDisplay.color = Color.cyan;
	}
}
