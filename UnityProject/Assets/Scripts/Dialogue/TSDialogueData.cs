using System;
using System.Collections.Generic;

namespace Dialogue {
	public class TSDialogueData {
		protected int _id; public int ID { get { return _id; } }

		public TSDialogueData (int id)
		{
			_id = id;
		}
	}
}
