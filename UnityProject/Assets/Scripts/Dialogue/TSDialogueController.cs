using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Dialogue;

public class TSDialogueController : MonoBehaviour {
	public Text textHeader;
	public Text textBody;

	private TSDialogueChoiceView _choiceView;

	private TSDialogueHeaderData _headerData;
	private TSDialogueBodyData _bodyData;

	private enum State { ENABLED, DISABLED };

	// Use this for initialization
	void Start () {
		_choiceView = gameObject.AddComponent<TSDialogueChoiceView>();

		TSDialogueChoiceController.SelectEvent += HandleSelectEvent;

		ClearUI();
	}
	
	// Update is called once per frame
	void Update () {

	}

	// public methods
	public void Parse(string filename) {
		_headerData = TSDialogueParser.Instance.ParseHeader(filename);
		_bodyData   = TSDialogueParser.Instance.ParseBody(filename);
		_choiceView.Display(filename);

		UpdateUI();
	}

	public void EndDialogue() {
		ClearUI();
	}

	private void UpdateUI() {
		textHeader.text = _headerData.Header;
		textBody.text = _bodyData.Body;
	}

	private void ClearUI() {
		textHeader.text = "";
		textBody.text = "";
		_choiceView.Hide();
	}

	// Event handlers
	void HandleSelectEvent (TSDialogueChoiceController choiceController) {
		EndDialogue();
	}
}
