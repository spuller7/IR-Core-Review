using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("QuestionDatabase")]
public class QuizAppContainer {

	[XmlElement("Questions")]
	public List<QuizApp> Quizes = new List<QuizApp>();

	public static QuizAppContainer Load(string path)
    {
		TextAsset _xml = Resources.Load<TextAsset>(path);

		XmlSerializer serializer = new XmlSerializer(typeof(QuizAppContainer));

		StringReader reader = new StringReader(_xml.text);

		QuizAppContainer Items = serializer.Deserialize(reader) as QuizAppContainer;

		#if UNITY_METRO
		reader.Dispose();
		#else
		reader.Close();
		#endif

		return Items;
    }

	public static QuizAppContainer DownloadedXML(StringReader OnlineXML)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(QuizAppContainer));

		QuizAppContainer Items = serializer.Deserialize(OnlineXML) as QuizAppContainer;

		#if UNITY_METRO
		OnlineXML.Dispose();
		#else
		OnlineXML.Close();
		#endif

		return Items;
	}
}