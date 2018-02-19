using System;
using System.Xml;
using System.Xml.Serialization;

[Serializable]
public class QuizApp
{
	[XmlElement("Question")]
	public string Question;

	[XmlArray("Answers")]
	[XmlArrayItem("Choices")]
	public Answers[] Answer = new Answers[0];

	[XmlElement("Image")]
	public string Image;
}