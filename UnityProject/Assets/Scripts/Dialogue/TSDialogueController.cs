using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Dialogue;

public class TSDialogueController : MonoBehaviour {
	public Text textHeader;
	public Text textBody;
	public Text textChoices;

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
		textBody.text = _dialogueData.Body;
		textHeader.text = _dialogueData.Header;

		textChoices.text = "";
		foreach (string choice in _dialogueData.Choices) {
			textChoices.text += choice;
			textChoices.text += "\n";
		}
		//textOptions.text = 
	}

	public void EndDialogue() {
		ClearUI();
	}

	private void ClearUI() {
		textHeader.text = "";
		textBody.text = "";
		textChoices.text = "";
	}
}
