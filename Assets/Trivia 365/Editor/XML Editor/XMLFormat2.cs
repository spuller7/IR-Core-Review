using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class XMLFormat2 : XMLFormat
{
    public int
        QuestionWidth = 500,
        IsTrueWidth = 50,
        DifficultyWidth = 100,
        FactWidth = 500,
        FieldHeight = 30;

    public Vector2 Scroll = Vector2.zero;
    private List<Format2> FormatList = new List<Format2>();
    private bool onStart = false;

    public override void OnGUI()
    {
        if (!this.onStart)
        {
            this.onStart = true;
            if(EditorPrefs.GetString("lastXML_format2") != string.Empty)
                this.XMLLoad(string.Empty);
        }


        EditorGUI.BeginChangeCheck();
        OnDrawHeader();
        OnDrawColumns();
		OnDrawBottomBar();
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(XMLFormat2.Window);
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
            GUILayout.Label("IsTrue", GUILayout.Width(this.IsTrueWidth));
            GUILayout.Space(5);
            GUILayout.Label("Difficulty", GUILayout.Width(this.DifficultyWidth));
            GUILayout.Space(5);
            GUILayout.Label("Fact", GUILayout.Width(this.FactWidth));
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

                Format2 format = this.FormatList[i];
                GUILayout.BeginVertical();
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUI.skin.textField.wordWrap = true;

                        // question
                        format.Question = EditorGUILayout.TextField(format.Question, GUILayout.Width(this.QuestionWidth), GUILayout.Height(FieldHeight));
                        GUILayout.Space(5);
                        // is true
                        format.IsTrue = EditorGUILayout.Toggle(format.IsTrue, GUILayout.Width(this.IsTrueWidth), GUILayout.Height(FieldHeight));
                        GUILayout.Space(5);
                        // difficulty
						format._Difficulty = (DifficultyLevel)EditorGUILayout.EnumPopup(format._Difficulty, GUILayout.Width(this.DifficultyWidth), GUILayout.Height(FieldHeight));
                        GUILayout.Space(5);
                        // fact
                        format.Fact = EditorGUILayout.TextField(format.Fact, GUILayout.Width(this.FactWidth), GUILayout.Height(FieldHeight));
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
                this.FormatList.Add(new Format2());
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
            Format2 format = this.FormatList[i];
            xmlWriter.WriteStartElement("Questions");

            xmlWriter.WriteStartElement("Question");
            xmlWriter.WriteString(format.Question);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("IsTrue");
            xmlWriter.WriteString(format.IsTrue ? "1" : "0");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Difficulty");
            xmlWriter.WriteString(format._Difficulty.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Fact");
            xmlWriter.WriteString(format.Fact);
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
        xmlDoc.LoadXml(path == string.Empty ? EditorPrefs.GetString("lastXML_format2") : File.ReadAllText(path));
        int index = 0;
        foreach (XmlNode xmlNode in xmlDoc.DocumentElement)
        {
            Format2 format = new Format2();
            // get all parent nodes
            foreach (XmlNode questionsNode in xmlNode)
            {
                if (questionsNode.Name == "Question")
                {
                   format.Question = questionsNode.InnerText;
                }
                if (questionsNode.Name == "IsTrue")
                {
                    format.IsTrue = questionsNode.InnerText == "0" ? false : true;
                }
                if (questionsNode.Name == "Difficulty")
                {
                    string inner = questionsNode.InnerText;
					DifficultyLevel dif = DifficultyLevel.Easy;
					dif = inner == "Easy" ? DifficultyLevel.Easy :
						inner == "Medium" ? DifficultyLevel.Medium :
						inner == "Hard" ? DifficultyLevel.Hard :
						DifficultyLevel.Bonus;
                    format._Difficulty = dif;
                }
                if (questionsNode.Name == "Fact")
                {
                    format.Fact = questionsNode.InnerText;
                }
                index++;
            }
            this.FormatList.Add(format);
        }
    }
    public override void ResetInfo()
    {
        EditorGUIUtility.editingTextField = false;
        this.FormatList = new List<Format2>();

    }
    public void SaveTempData()
    {
        if (!Directory.Exists(Application.temporaryCachePath + "/temp_xml")) Directory.CreateDirectory(Application.temporaryCachePath + "/temp_xml");
        string content = XMLSave(Application.temporaryCachePath + "/temp_xml/xml_format2.temp");
        EditorPrefs.SetString("lastXML_format2", content);
    }
}