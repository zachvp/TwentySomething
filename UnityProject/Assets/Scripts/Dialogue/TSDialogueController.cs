using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Dialogue;

public class TSDialogueController : MonoBehaviour {
	public Text textHeader;
	public Text textBody;
	public Text textChoices;

	private TSDialogueHeaderData _headerData;
	private TSDialogueBodyData _bodyData;
	private TSDialogueChoiceData _choiceData;

	private enum State { ENABLED, DISABLED };

	// Use this for initialization
	void Start () {
		ClearUI();
	}
	
	// Update is called once per frame
	void Update () {
	}

	// public methods
	public void Parse(string filename) {
		_headerData = TSDialogueParser.Instance.ParseHeader(filename);
		_bodyData   = TSDialogueParser.Instance.ParseBody(filename);
		_choiceData = TSDialogueParser.Instance.ParseChoices(filename);

		UpdateUI();
	}

	public void EndDialogue() {
		ClearUI();
	}

	private void UpdateUI() {
		textHeader.text = _headerData.Header;
		textBody.text = _bodyData.Body;
		
		textChoices.text = "";
		foreach (string choice in _choiceData.Choices) {
			textChoices.text += choice;
			textChoices.text += "\n";
		}
	}

	private void ClearUI() {
		textHeader.text = "";
		textBody.text = "";
		textChoices.text = "";
	}
}
