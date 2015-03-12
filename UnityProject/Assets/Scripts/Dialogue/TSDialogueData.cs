using System;
using System.Collections.Generic;

namespace Dialogue {
	public class TSDialogueData {
		public enum Attributes { STAMINA };

		private int _id;				public int ID 				{ get { return _id; } }
		private string _header; 		public string Header 		{ get { return _header; } }
		private string _body;   		public string Body   		{ get { return _body; } }
		private List<string> _choices;  public List<string> Choices { get { return _choices; } }

		private Dictionary <Attributes, int> _attributesToValues;

		public TSDialogueData () {

		}

		public TSDialogueData (int id, string header, string body, List<string> choices, 
		                         Dictionary<Attributes, int> attributesToValues) {
			_id = id;
			_header = header;
			_body = body;
			_choices = choices;
			_attributesToValues = attributesToValues;
		}
	}
}
