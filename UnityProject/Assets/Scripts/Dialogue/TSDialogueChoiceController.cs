using UnityEngine;
using System.Collections;

public class TSDialogueChoiceController : MonoBehaviour {
	public delegate void CycleForwardAction(TSDialogueChoiceController choiceController);
	public static event CycleForwardAction CycleForwardEvent;

	public delegate void CycleBackwardAction(TSDialogueChoiceController choiceController);
	public static event CycleForwardAction CycleBackwardEvent;

	public delegate void SelectAction(TSDialogueChoiceController choiceController);
	public static event SelectAction SelectEvent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
			if (CycleForwardEvent != null) {
				CycleForwardEvent(this);
			}
		} else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			if (CycleBackwardEvent != null) {
				CycleBackwardEvent(this);
			}
		}

		if (Input.GetKeyDown(KeyCode.Return)) {
			if (SelectEvent != null) {
				SelectEvent(this);
			}
		}
	}

	void OnDestroy () {
		SelectEvent = null;
	}
}
