using UnityEditor;
using UnityEngine;

public class XMLSheetWindow : EditorWindow
{
    public static XMLSheetWindow WindowInstance;

    public static XMLFormat1 Format1 = new XMLFormat1(); //format 1
    public static XMLFormat2 Format2 = new XMLFormat2(); //format 2
    public static XMLFormat3 Format3 = new XMLFormat3(); //format 3
    public enum FormatType
    {
		QuizMe, TrueOrFalse, TriviaKingdom
    }
    public static FormatType CurrentFormatType;

    /// <summary>
    /// Open the window
    /// </summary>
    [MenuItem("Window/QuizApp XML Editor")]
    public static void OpenWindow()
    {
        WindowInstance = GetWindow<XMLSheetWindow>(true, "QuizApp XML Editor");
        WindowInstance.Show();
		WindowInstance.minSize = new Vector2(1295, 200);
	}

    /// <summary>
    /// Display the content based on which format we are in
    /// </summary>
    private void OnGUI()
    {
        WindowInstance = this;

		if (CurrentFormatType == FormatType.QuizMe)
			Format1.OnGUI();
		else if (CurrentFormatType == FormatType.TrueOrFalse)
            Format2.OnGUI();
		else if (CurrentFormatType == FormatType.TriviaKingdom)
            Format3.OnGUI();
    }
}