using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Dialogue;

public class TSDialogueController : MonoBehaviour {
	public Text _textDialogOutput;
	public string _filename;

	private TSDialogueData _dialogueData;

	// Use this for initialization
	void Start () {
		_dialogueData = TSDialogueParser.Instance.Parse(_filename);

		// update the UI
		_textDialogOutput.text = TSDialogueParser.debug;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
