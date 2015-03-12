using System;

namespace Dialogue
{
	public class TSDialogueBodyData
	{
		private TSDialogueData _data;

		private string _body; public string Body { get { return _body; } }

		public TSDialogueBodyData (TSDialogueData data, string body)
		{
			_data = data;
			_body = body;
		}
	}
}

