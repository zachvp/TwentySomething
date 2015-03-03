using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TSDialogueController : MonoBehaviour {
	public Text _textDialogOutput;

	// Use this for initialization
	void Start () {
		Dialogue.TSDialogueParser.Instance.Parse("test");
		Debug.Log(gameObject.name + " " +Dialogue.TSDialogueParser.debug);

		// update the UI
		_textDialogOutput.text = Dialogue.TSDialogueParser.debug;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
