using System;
using System.Xml;
using System.Xml.Serialization;

[Serializable]
public class Answers
{
	[XmlText(Type=typeof(string))]
	public string Choices;

	[XmlAttribute("correct")]
	public bool Correct;
}