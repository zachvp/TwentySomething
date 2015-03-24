using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

using Dialogue;

public class TSDialogueChoiceView : MonoBehaviour {
	//public RectTransform panel;
	//public Text textChoices;

	private RectTransform panel;
	private List<Text> _UIChoices;

	private TSDialogueChoiceData _choiceData;

	private enum State { INACTIVE, ACTIVE };
	private State _state;

	private int _choiceIndex;

	void Awake() {
		_UIChoices = new List<Text>();

		TSDialogueChoiceController.CycleForwardEvent += HandleCycleForwardEvent;
		TSDialogueChoiceController.CycleBackwardEvent += HandleCycleBackwardEvent;
		TSDialogueChoiceController.SelectEvent += HandleSelectEvent;

		_choiceIndex = 0;
	}

	// Use this for initialization
	void Start () {
		panel = GameObject.FindGameObjectWithTag("choicePanel").GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Display (string filename) {
		_choiceData = TSDialogueParser.Instance.ParseChoices(filename);
		_state = State.ACTIVE;

		float offset = 16;
		Vector2 adjustedPosition = panel.position;
		adjustedPosition.y += offset;

		foreach (Choice choice in _choiceData.Choices) {
			//textChoices.text += choice;
			//textChoices.text += "\n";
			Text choiceText = Instantiate(Resources.Load("DialogueChoice", typeof(Text))) as Text;

			choiceText.text = choice._text;

			choiceText.transform.SetParent(panel);
			choiceText.transform.position = adjustedPosition;

			adjustedPosition.y -= choiceText.rectTransform.rect.height;
			
			_UIChoices.Add(choiceText);
		}

		SelectChoice(_choiceIndex);
	}

	public void Hide() {
		ClearUI();
		_state = State.INACTIVE;
	}

	// delegate handlers
	private void HandleCycleForwardEvent(TSDialogueChoiceController choiceController) {
		if (_state == State.ACTIVE) {
			_choiceIndex += 1;
			_choiceIndex %= _UIChoices.Count;

			UpdateUI();
		}
	}

	private void HandleCycleBackwardEvent(TSDialogueChoiceController choiceController) {
		if (_state == State.ACTIVE) {
			_choiceIndex -= 1;

			if(_choiceIndex < 0)
				_choiceIndex = _UIChoices.Count - 1;
			
			UpdateUI();
		}
	}

	private void HandleSelectEvent(TSDialogueChoiceController choiceController) {
		if (_state == State.ACTIVE) {
			Debug.Log(gameObject.name + ": Choice is " + _UIChoices[_choiceIndex].text + 
			          ", +" + _choiceData.Choices[_choiceIndex]._attributeValue + " stamina");
		}
	}

	private void SelectChoice(int index) {
		for (int i = 0; i < _UIChoices.Count; ++i) {
			if (i == index) {
				_UIChoices[i].color = Color.red;
			} else {
				_UIChoices[i].color = Color.white;
			}
		}
	}

	private void UpdateUI() {
		SelectChoice(_choiceIndex);
	}

	private void ClearUI() {
		foreach (Text text in _UIChoices) {
			text.text = "";
			Destroy(text.gameObject);
		}

		Resources.UnloadUnusedAssets();
		_UIChoices.Clear();
	}
}
