using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class XMLFormat3 : XMLFormat
{
    public int
    QuestionWidth = 250,
    ImageWidth = 200,
    IsTrueWidth = 50,
    FactWidth = 250,
    CorrectAnswerWidth = 110,
    WrongAnswerAWidth = 110,
    WrongAnswerBWidth = 110,
    WrongAnswerCWidth = 110,
    FieldHeight = 30;

    public Vector2 Scroll = Vector2.zero;
    private List<Format3> FormatList = new List<Format3>();
    private bool onStart = false;

    public override void OnGUI()
    {
        if (!this.onStart)
        {
            this.onStart = true;
            if (EditorPrefs.GetString("lastXML_format3") != string.Empty)
                this.XMLLoad(string.Empty);
        }


        EditorGUI.BeginChangeCheck();
        this.OnDrawHeader();
        this.OnDrawColumns();
        this.OnDrawBottomBar();

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(XMLFormat3.Window);
            SaveTempData();
        }
    }

    public override void OnDrawHeader()
    {
        GUI.color = new Color(1, 1, 1, 0.6f);
        GUILayout.BeginHorizontal("", "Wizard Box", GUILayout.Height(20));
        {
            GUI.color = Color.white;

            GUILayout.Label("Question", GUILayout.Width(this.QuestionWidth));
            GUILayout.Space(5);
            GUILayout.Label("Image", GUILayout.Width(this.ImageWidth));
            GUILayout.Space(5);
            GUILayout.Label("IsTrue", GUILayout.Width(this.IsTrueWidth));
            GUILayout.Space(5);
            GUILayout.Label("Fact", GUILayout.Width(this.FactWidth));
            GUILayout.Space(5);
            GUILayout.Label("Correct Answer", GUILayout.Width(this.CorrectAnswerWidth));
            GUILayout.Space(5);
            GUILayout.Label("Wrong Answer", GUILayout.Width(this.WrongAnswerAWidth));
            GUILayout.Space(5);
            GUILayout.Label("Wrong Answer", GUILayout.Width(this.WrongAnswerBWidth));
            GUILayout.Space(5);
            GUILayout.Label("Wrong Answer", GUILayout.Width(this.WrongAnswerCWidth));
            GUILayout.Space(5);
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
    }
    public override void OnDrawColumns()
    {
        this.Scroll = GUILayout.BeginScrollView(this.Scroll);
        {
            for (int i = 0; i < this.FormatList.Count; i++)
            {
                GUILayout.Space(5);

                Format3 format = this.FormatList[i];
                GUILayout.BeginVertical();
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUI.skin.textField.wordWrap = true;

                        // question
                        format.Question = EditorGUILayout.TextField(format.Question, GUILayout.Width(this.QuestionWidth), GUILayout.Height(FieldHeight));
                        GUILayout.Space(5);
                        // image
                        format.Image = EditorGUILayout.TextField(format.Image, GUILayout.Width(this.ImageWidth), GUILayout.Height(FieldHeight));
                        GUILayout.Space(5);
                        // is true
                        format.IsTrue = EditorGUILayout.Toggle(format.IsTrue, GUILayout.Width(this.IsTrueWidth), GUILayout.Height(FieldHeight));
                        GUILayout.Space(5);
                        // fact
                        format.Fact = EditorGUILayout.TextField(format.Fact, GUILayout.Width(this.FactWidth), GUILayout.Height(FieldHeight));
                        GUILayout.Space(5);
                        // correct answer
                        format.CorrectAnswer = EditorGUILayout.TextField(format.CorrectAnswer, GUILayout.Width(this.CorrectAnswerWidth), GUILayout.Height(FieldHeight));
                        GUILayout.Space(5);
                        // wrong answer
                        format.WrongAnswerA = EditorGUILayout.TextField(format.WrongAnswerA, GUILayout.Width(this.WrongAnswerAWidth), GUILayout.Height(FieldHeight));
                        GUILayout.Space(5);
                        // wrong answer
                        format.WrongAnswerB = EditorGUILayout.TextField(format.WrongAnswerB, GUILayout.Width(this.WrongAnswerBWidth), GUILayout.Height(FieldHeight));
                        GUILayout.Space(5);
                        // wrong answer
                        format.WrongAnswerC = EditorGUILayout.TextField(format.WrongAnswerC, GUILayout.Width(this.WrongAnswerCWidth), GUILayout.Height(FieldHeight));
                        GUILayout.Space(5);
                        // delete entry
                        if (GUILayout.Button("X", EditorStyles.toolbarButton, GUILayout.Width(20), GUILayout.Height(FieldHeight)))
                        {
                            this.FormatList.RemoveAt(i);
                        }
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();
            }
        }
        GUILayout.EndScrollView();
    }

    public override void OnDrawBottomBar()
    {
        GUILayout.BeginHorizontal("", EditorStyles.helpBox);
        {
            if (GUILayout.Button("Add", EditorStyles.toolbarButton))
            {
                this.FormatList.Add(new Format3());
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Save to XML", EditorStyles.toolbarButton))
            {
                EditorGUIUtility.editingTextField = false;

                string path = EditorUtility.SaveFilePanel("Save XML", "Assets/", "xmlFile", "xml");

                if (string.IsNullOrEmpty(path)) return;

                string content = this.XMLSave(path);

                File.WriteAllText(path, content);

                AssetDatabase.Refresh();
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Load from XML", EditorStyles.toolbarButton))
            {
                ResetInfo();
                string path = EditorUtility.OpenFilePanel("Open XML", "Assets/", "xml");
                if (string.IsNullOrEmpty(path)) return;
                this.XMLLoad(path);
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Reset", EditorStyles.toolbarButton))
            {
                this.ResetInfo();
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Clear Temp Files", EditorStyles.toolbarButton))
            {
                if (!Directory.Exists(Application.temporaryCachePath + "/temp_xml")) Directory.CreateDirectory(Application.temporaryCachePath + "/temp_xml");

                string[] files = Directory.GetFiles(Application.temporaryCachePath + "/temp_xml");
                if (files.Length > 0)
                    foreach (string s in files)
                    {
                        if (File.Exists(s)) File.Delete(s);
                    }
            }
            GUILayout.Space(5);
            XMLSheetWindow.CurrentFormatType = (XMLSheetWindow.FormatType)EditorGUILayout.EnumPopup(XMLSheetWindow.CurrentFormatType, EditorStyles.toolbarDropDown);
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
    }
    public string XMLSave(string path)
    {
        XmlWriter xmlWriter = XmlWriter.Create(path);
        xmlWriter.WriteStartDocument();

        xmlWriter.WriteStartElement("QuestionDatabase");

        // write question data
        for (int i = 0; i < this.FormatList.Count; i++)
        {
            Format3 format = this.FormatList[i];
            xmlWriter.WriteStartElement("Questions");

            xmlWriter.WriteStartElement("Question");
            xmlWriter.WriteString(format.Question);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Image");
            xmlWriter.WriteString(format.Image);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("IsTrue");
            xmlWriter.WriteString(format.IsTrue ? "1" : "0");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Fact");
            xmlWriter.WriteString(format.Fact);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Answers");

			if (format.CorrectAnswer.Length > 0)
			{
				xmlWriter.WriteStartElement("Choices");
				xmlWriter.WriteAttributeString("correct", "true");
				xmlWriter.WriteString(format.CorrectAnswer);
				xmlWriter.WriteEndElement();

				xmlWriter.WriteStartElement("Choices");
				xmlWriter.WriteAttributeString("correct", "false");
				xmlWriter.WriteString(format.WrongAnswerA);
				xmlWriter.WriteEndElement();

				xmlWriter.WriteStartElement("Choices");
				xmlWriter.WriteAttributeString("correct", "false");
				xmlWriter.WriteString(format.WrongAnswerB);
				xmlWriter.WriteEndElement();

				xmlWriter.WriteStartElement("Choices");
				xmlWriter.WriteAttributeString("correct", "false");
				xmlWriter.WriteString(format.WrongAnswerC);
				xmlWriter.WriteEndElement();
			}

            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
        }

        xmlWriter.WriteEndElement();

        xmlWriter.WriteEndDocument();
        xmlWriter.Close();

        string content = File.ReadAllText(path);
        content = UTF8ByteArrayToString(StringToUTF8ByteArray(content));
        return content;
    }
    public void XMLLoad(string path)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(path == string.Empty ? EditorPrefs.GetString("lastXML_format3") : File.ReadAllText(path));
        int index = 0;
        int answer = 0;
        foreach (XmlNode xmlNode in xmlDoc.DocumentElement)
        {
            Format3 format = new Format3();
            // get all parent nodes
            foreach (XmlNode questionsNode in xmlNode)
            {
                if (questionsNode.Name == "Question")
                {
                    format.Question = questionsNode.InnerText;
                }
                if (questionsNode.Name == "Image")
                {
                    format.Image = questionsNode.InnerText;
                }
                if (questionsNode.Name == "IsTrue")
                {
                    format.IsTrue = questionsNode.InnerText == "0" ? false : true;
                }
                if (questionsNode.Name == "Fact")
                {
                    format.Fact = questionsNode.InnerText;
                }
                if (questionsNode.Name == "Answers")
                {
                    foreach (XmlNode answersNode in questionsNode)
                    {
                        if (answersNode.Name == "Choices")
                        {
                            if (answer == 0) format.CorrectAnswer = answersNode.InnerText;
                            else if (answer == 1) format.WrongAnswerA = answersNode.InnerText;
                            else if (answer == 2) format.WrongAnswerB = answersNode.InnerText;
                            else if (answer == 3) format.WrongAnswerC = answersNode.InnerText;
                            answer++;
                        }
                    }
                }
                answer = 0;
                index++;
            }
            this.FormatList.Add(format);
        }
    }
    public override void ResetInfo()
    {
        EditorGUIUtility.editingTextField = false;
        this.FormatList = new List<Format3>();
    }
    public void SaveTempData()
    {
        if (!Directory.Exists(Application.temporaryCachePath + "/temp_xml")) Directory.CreateDirectory(Application.temporaryCachePath + "/temp_xml");
        string content = XMLSave(Application.temporaryCachePath + "/temp_xml/xml_format3.temp");
        EditorPrefs.SetString("lastXML_format3", content);
    }
}