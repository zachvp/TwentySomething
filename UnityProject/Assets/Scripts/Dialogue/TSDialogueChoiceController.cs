using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

using Dialogue;

public class TSDialogueChoiceController : MonoBehaviour {
	//public RectTransform panel;
	//public Text textChoices;

	private RectTransform panel;
	private List<Text> _UIChoices;

	private TSDialogueChoiceData _choiceData;

	void Awake() {
		_UIChoices = new List<Text>();
	}

	// Use this for initialization
	void Start () {
		//Parse("test");
		//UpdateUI();
		panel = GameObject.FindGameObjectWithTag("choicePanel").GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Parse (string filename) {
		_choiceData = TSDialogueParser.Instance.ParseChoices(filename);

		float offset = 16;
		Vector2 adjustedPosition = panel.position;
		adjustedPosition.y += offset;

		foreach (string choice in _choiceData.Choices) {
			//textChoices.text += choice;
			//textChoices.text += "\n";
			Text choiceText = Instantiate(Resources.Load("DialogueChoice", typeof(Text))) as Text;

			choiceText.text = choice;

			choiceText.transform.SetParent(panel);
			choiceText.transform.position = adjustedPosition;

			adjustedPosition.y -= choiceText.rectTransform.rect.height;
			
			_UIChoices.Add(choiceText);
		}
	}

	private void UpdateUI() {
		//textChoices.text = "";

	}

	public void ClearUI() {
		foreach (Text text in _UIChoices) {
			text.text = "";
			Destroy(text.gameObject);
		}

		Resources.UnloadUnusedAssets();
		_UIChoices.Clear();
	}
}
