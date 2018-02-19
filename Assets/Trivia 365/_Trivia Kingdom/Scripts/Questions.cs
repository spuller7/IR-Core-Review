using System;
using System.Xml;
using System.Xml.Serialization;

[Serializable]
public class Questions
{
    [XmlElement("ID")]
    public int ID;

    [XmlElement("Question")]
	public string Question;

	[XmlElement("Image")]
	public string Image;

	[XmlElement("Shuffle")]
	public int Shuffle;

    [XmlElement("NOTA")]
    public int NOTA;

    [XmlArray("Answers")]
	[XmlArrayItem("Choices")]
	public AnswerClass[] Answer = new AnswerClass[0];
}