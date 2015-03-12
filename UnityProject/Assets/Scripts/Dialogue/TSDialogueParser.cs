using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace Dialogue {
	public sealed class TSDialogueParser {
		public static string debug;

		private static readonly TSDialogueParser _instance = new TSDialogueParser();

		private static string _path;
		private static int _lastID = 0;

		private const string kPathToXMLDirectory = "/UnityProject/Assets/Dialogue/XML/";
		private const string kXMLExtension 		 = ".xml";

		private const string kDialogueNodeName   = "dialogue";
		private const string kHeaderNodeName   	 = "header";
		private const string kBodyNodeName 		 = "body";
		private const string kChoiceNodeName 	 = "choice";

		private const string kStaminaAttributeName = "stamina";
			
		private TSDialogueParser () {}

		public static TSDialogueParser Instance { get { return _instance; } }

		public TSDialogueData Parse(string filename) {
			debug = "";

			XmlDocument doc = new XmlDocument();
			_path = Directory.GetParent(Environment.CurrentDirectory).FullName + 
				   kPathToXMLDirectory + filename + kXMLExtension;
			doc.Load(_path);

			XmlNode rootNode = doc.DocumentElement.SelectSingleNode("/" + kDialogueNodeName);

			XmlNode titleNode = rootNode.SelectSingleNode(kHeaderNodeName); debug += titleNode.InnerText + "\n";
			XmlNode bodyNode = rootNode.SelectSingleNode(kBodyNodeName); debug += bodyNode.InnerText + "\n\n";
			
			XmlNodeList choiceNodes = rootNode.SelectNodes(kChoiceNodeName);

			string title, body;
			List<string> choices = new List<string>();
			Dictionary<TSDialogueData.Attributes, int> attributesToValues = new Dictionary<TSDialogueData.Attributes, int>();

			title = titleNode.InnerText;
			body = bodyNode.InnerText;

			foreach (XmlNode choice in choiceNodes) {
				debug += choice.InnerText;
				choices.Add(choice.InnerText);
				if (choice.Attributes[kStaminaAttributeName] != null) {
					int value = 0;
					Int32.TryParse(choice.Attributes[kStaminaAttributeName].Value, out value);
					attributesToValues.Add(TSDialogueData.Attributes.STAMINA, value);
					debug += " - " + choice.Attributes[kStaminaAttributeName].Value.ToString() + " stamina\n";
				}
			}

			return new TSDialogueData(_lastID++, title, body, choices, attributesToValues);
		}
	}
}