using System;
using System.Collections.Generic;

namespace Dialogue {
	public class TSDialogueData {
		public enum Attributes { STAMINA };

		private int _id;
		private string _title;
		private string _body;
		private List<string> _choices;
		private Dictionary <Attributes, int> _attributesToValues;

		public TSDialogueData () {

		}

		public TSDialogueData (int id, string title, string body, List<string> choices, 
		                         Dictionary<Attributes, int> attributesToValues) {
			_id = id;
			_title = title;
			_body = body;
			_choices = choices;
			_attributesToValues = attributesToValues;
		}
	}
}
