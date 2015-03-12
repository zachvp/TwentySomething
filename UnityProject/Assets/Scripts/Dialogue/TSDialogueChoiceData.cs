using System;
using System.Collections.Generic;

namespace Dialogue
{
	public class TSDialogueChoiceData
	{
		private TSDialogueData _data;
		public enum Attributes { STAMINA };

		private List<string> _choices;  public List<string> Choices { get { return _choices; } }
		private Dictionary <Attributes, int> _attributesToValues; // choices

		public TSDialogueChoiceData (TSDialogueData data, 
		                             List<string> choices, Dictionary<Attributes, int> attributesToValues)
		{
			_data = data;
			_choices = choices;
			_attributesToValues = attributesToValues;
		}
	}
}

