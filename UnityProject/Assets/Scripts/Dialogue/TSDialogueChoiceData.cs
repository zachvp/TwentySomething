using System;
using System.Collections.Generic;

namespace Dialogue
{
	public class TSDialogueChoiceData
	{
		private TSDialogueData _data;
		public enum Attributes { STAMINA };

		private List<Choice> _choices;  public List<Choice> Choices { get { return _choices; } }

		public TSDialogueChoiceData (TSDialogueData data, List<Choice> choices) {
			_data = data;
			_choices = choices;
		}
	}

	public struct Choice {
		public string _text;
		public int _attributeValue;

		public Choice (string text, int attributeValue) {
			_text = text;
			_attributeValue = attributeValue;
		}
	};
}

