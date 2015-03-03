﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TSDialogueController : MonoBehaviour {
	public Text _textDialogOutput;

	// Use this for initialization
	void Start () {
		TSDialogueParser.Instance.Parse("test");
		Debug.Log(gameObject.name + " " + TSDialogueParser.debug);

		// update the UI
		_textDialogOutput.text = TSDialogueParser.debug;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}