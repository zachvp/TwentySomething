using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Dialogue;

public class TSDialogueController : MonoBehaviour {
	public Text _textDialogOutput;

	private TSDialogueData _dialogueData;

	private enum State { ENABLED, DISABLED };

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

	// public methods
	public void Parse(string filename) {
		_dialogueData = TSDialogueParser.Instance.Parse(filename);
		
		// update the UI
		_textDialogOutput.text = TSDialogueParser.debug;
	}

	public void EndDialogue() {
		_textDialogOutput.text = "";
	}
}
