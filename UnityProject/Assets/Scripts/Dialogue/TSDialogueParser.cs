using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace Dialogue {
	public sealed class TSDialogueParser {
		public static string debug;

		private static readonly TSDialogueParser _instance = new TSDialogueParser();

		private static XmlDocument _doc;
		private static XmlNode _rootNode;
		private static int _lastID = 0;

		private const string kPathToXMLDirectory = "/UnityProject/Assets/Dialogue/XML/";
		private const string kXMLExtension 		 = ".xml";

		private const string kDialogueNodeName   = "dialogue";
		private const string kHeaderNodeName   	 = "header";
		private const string kBodyNodeName 		 = "body";
		private const string kChoiceNodeName 	 = "choice";

		private const string kStaminaAttributeName = "stamina";
			
		private TSDialogueParser ()
		{
			_doc = new XmlDocument();
		}

		public static TSDialogueParser Instance { get { return _instance; } }

		public TSDialogueHeaderData ParseHeader(string filename) {
			InitParse(filename);

			XmlNode headerNode = _rootNode.SelectSingleNode(kHeaderNodeName); debug += headerNode.InnerText + "\n";
			string header = headerNode.InnerText;
			TSDialogueData data = new TSDialogueData(_lastID++);

			return new TSDialogueHeaderData(data, header);
		}

		public TSDialogueBodyData ParseBody(string filename) {
			InitParse(filename);

			XmlNode bodyNode = _rootNode.SelectSingleNode(kBodyNodeName); debug += bodyNode.InnerText + "\n\n";
			string body = bodyNode.InnerText;
			TSDialogueData data = new TSDialogueData(_lastID++);

			return new TSDialogueBodyData(data, body);
		}

		public TSDialogueChoiceData ParseChoices(string filename) {
			InitParse(filename);

			List<string> choices = new List<string>();
			Dictionary<TSDialogueChoiceData.Attributes, int> attributesToValues = new Dictionary<TSDialogueChoiceData.Attributes, int>();
			XmlNodeList choiceNodes = _rootNode.SelectNodes(kChoiceNodeName);
			TSDialogueData data = new TSDialogueData(_lastID++);

			foreach (XmlNode choice in choiceNodes) {
				debug += choice.InnerText;
				choices.Add(choice.InnerText);
				if (choice.Attributes[kStaminaAttributeName] != null) {
					int value = 0;
					Int32.TryParse(choice.Attributes[kStaminaAttributeName].Value, out value);
					attributesToValues.Add(TSDialogueChoiceData.Attributes.STAMINA, value);

					debug += " - " + choice.Attributes[kStaminaAttributeName].Value.ToString() + " stamina\n";
				}
			}

			return new TSDialogueChoiceData(data, choices, attributesToValues);
		}

		private void InitParse(string filename) {
			debug = "";

			// read file
			string path = Directory.GetParent(Environment.CurrentDirectory).FullName + 
							kPathToXMLDirectory + filename + kXMLExtension;
			_doc.Load(path);
			
			_rootNode = _doc.DocumentElement.SelectSingleNode("/" + kDialogueNodeName);
		}
	}
}