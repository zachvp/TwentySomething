using System;

namespace Dialogue
{
	public class TSDialogueHeaderData
	{
		private TSDialogueData _data;
		protected string _header; public string Header { get { return _header; } }

		public TSDialogueHeaderData (TSDialogueData data, string header)
		{
			_data = data;
			_header = header;
		}
	}
}

