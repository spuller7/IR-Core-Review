using System;
using System.Xml;
using System.Xml.Serialization;

[Serializable]
public class AnswerClass
{
	[XmlText(Type=typeof(string))]
	public string Choices;

	[XmlAttribute("correct")]
	public bool Correct;
}