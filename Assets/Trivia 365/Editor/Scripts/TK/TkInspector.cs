#if (UNITY_ANDROID || UNITY_IOS)
#define SUPPORTED_PLATFORM
#endif

using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Mintonne.QuizApp;

[CustomEditor(typeof(MainController))]
public class TkInspector : Editor
{
    private GUIStyle m_Background;

#if USE_DOTWEEN

    MainController LocScript;

#if SUPPORTED_PLATFORM
    private bool ShowOnesignal = false;
    private bool ShowAdmob = false;
    private static readonly char[] defineSeperators = new char[] { ';', ',', ' ' };
#endif

#if (USE_ADMOB && SUPPORTED_PLATFORM)
	SerializedProperty InterstitialAdUnitId = null;
	SerializedProperty AdFrequency = null;
	SerializedProperty TestMode = null;
	SerializedProperty AdmobTestID = null;
#endif

#if (USE_ONESIGNAL && SUPPORTED_PLATFORM)
	SerializedProperty AppId = null;
#endif

    SerializedProperty SplashScreen = null;
    SerializedProperty MenuCanvas = null;
    SerializedProperty GameCanvas = null;
    SerializedProperty PauseCanvas = null;
    SerializedProperty GameOverCanvas = null;
    SerializedProperty ReviewCanvas = null;
    SerializedProperty ReviewTestCanvas = null;

    SerializedProperty RetrySplashButton = null;
    SerializedProperty SplashScreenLoading = null;

    SerializedProperty QuestionTypes = null;
    SerializedProperty QuizMode = null;
    SerializedProperty SoundImage = null;
    SerializedProperty ActiveSoundImage = null;
    SerializedProperty VibrateImage = null;
    SerializedProperty ActiveVibrateImage = null;
    SerializedProperty MenuHandler = null;
    SerializedProperty Confirmation = null;

    SerializedProperty ImageLoadingAnimation = null;
    SerializedProperty ImageErrorPanel = null;
    SerializedProperty QuestionDisplay = null;
    SerializedProperty AnswerList = null;
    SerializedProperty ImageDisplay = null;
    SerializedProperty ResultDisplay = null;
    SerializedProperty QuizCounter = null;
    SerializedProperty QuitPanel = null;
    SerializedProperty ReturnPanel = null;
    SerializedProperty StarButton = null;
    SerializedProperty ActiveStar = null;
    SerializedProperty InactiveStar = null;

    SerializedProperty GameOverResult = null;
    SerializedProperty GameOverTip = null;
    SerializedProperty PointsText = null;
    SerializedProperty BonusPointsText = null;
    SerializedProperty GameOverPercentage = null;
    SerializedProperty GameOverCircle = null;

    SerializedProperty Mode = null;
    SerializedProperty OnlinePath = null;
    SerializedProperty OfflinePath = null;
    SerializedProperty OfflinePathFree = null;
    SerializedProperty FreeVersion = null;
    SerializedProperty EnableShuffle = null;
    SerializedProperty TimeOut = null;
    SerializedProperty QuestionCount = null;
    SerializedProperty PointsPerAnswer = null;
    SerializedProperty TimeBonusPoints = null;
    SerializedProperty TimerAmount = null;
    SerializedProperty CorrectColor = null;
    SerializedProperty FailColor = null;
    SerializedProperty ButtonColor = null;
    SerializedProperty CorrectSound = null;
    SerializedProperty FailSound = null;
    SerializedProperty RedToggle = null;
    SerializedProperty GreenToggle = null;
    SerializedProperty Avatars = null;
    SerializedProperty EnablePausing = null;

    SerializedProperty GameQuestions = null;
    SerializedProperty AllQuestions = null;
    SerializedProperty FormerQuestions = null;
    SerializedProperty NewQuestions = null;
    SerializedProperty StarredQuestions = null;
    SerializedProperty AnswersAvailable = null;
#endif

    void OnEnable()
    {
#if USE_DOTWEEN
        LocScript = ((MainController)target);
#endif

        m_Background = new GUIStyle();
        m_Background.margin = new RectOffset(2, 2, 2, 2);
        m_Background.normal.background = MakeTex(new Color(0.3f, 0.3f, 0.3f, 0.5f));

#if USE_ADMOB && SUPPORTED_PLATFORM
		InterstitialAdUnitId = serializedObject.FindProperty ("InterstitialAdUnitId");
		AdFrequency = serializedObject.FindProperty ("AdFrequency");
		TestMode = serializedObject.FindProperty ("TestMode");
		AdmobTestID = serializedObject.FindProperty ("AdmobTestID");
#endif

#if (USE_ONESIGNAL && SUPPORTED_PLATFORM)
		AppId = serializedObject.FindProperty ("AppId");
#endif

#if USE_DOTWEEN
        SplashScreen = serializedObject.FindProperty("SplashScreen");
        MenuCanvas = serializedObject.FindProperty("MenuCanvas");
        GameCanvas = serializedObject.FindProperty("GameCanvas");
        PauseCanvas = serializedObject.FindProperty("PauseCanvas");
        GameOverCanvas = serializedObject.FindProperty("GameOverCanvas");
        ReviewCanvas = serializedObject.FindProperty("ReviewCanvas");
        ReviewTestCanvas = serializedObject.FindProperty("ReviewTestCanvas");

        RetrySplashButton = serializedObject.FindProperty("RetrySplashButton");
        SplashScreenLoading = serializedObject.FindProperty("SplashScreenLoading");

        QuestionTypes = serializedObject.FindProperty("QuestionTypes");
        QuizMode = serializedObject.FindProperty("QuizMode");
        SoundImage = serializedObject.FindProperty("SoundImage");
        ActiveSoundImage = serializedObject.FindProperty("ActiveSoundImage");
        VibrateImage = serializedObject.FindProperty("VibrateImage");
        ActiveVibrateImage = serializedObject.FindProperty("ActiveVibrateImage");
        MenuHandler = serializedObject.FindProperty("MenuHandler");
        Confirmation = serializedObject.FindProperty("Confirmation");


        ImageLoadingAnimation = serializedObject.FindProperty("ImageLoadingAnimation");
        ImageErrorPanel = serializedObject.FindProperty("ImageErrorPanel");
        QuestionDisplay = serializedObject.FindProperty("QuestionDisplay");
        AnswerList = serializedObject.FindProperty("AnswerList");
        ImageDisplay = serializedObject.FindProperty("ImageDisplay");
        ResultDisplay = serializedObject.FindProperty("ResultDisplay");
        QuizCounter = serializedObject.FindProperty("QuizCounter");
        QuitPanel = serializedObject.FindProperty("QuitPanel");
        ReturnPanel = serializedObject.FindProperty("ReturnPanel");
        StarButton = serializedObject.FindProperty("StarButton");
        ActiveStar = serializedObject.FindProperty("ActiveStar");
        InactiveStar = serializedObject.FindProperty("InactiveStar");

        GameOverResult = serializedObject.FindProperty("GameOverResult");
        GameOverTip = serializedObject.FindProperty("GameOverTip");
        PointsText = serializedObject.FindProperty("PointsText");
        BonusPointsText = serializedObject.FindProperty("BonusPointsText");
        GameOverPercentage = serializedObject.FindProperty("GameOverPercentage");
        GameOverCircle = serializedObject.FindProperty("GameOverCircle");

        Mode = serializedObject.FindProperty("Mode");
        OnlinePath = serializedObject.FindProperty("OnlinePath");
        OfflinePath = serializedObject.FindProperty("OfflinePath");
        OfflinePathFree = serializedObject.FindProperty("OfflinePathFree");
        FreeVersion = serializedObject.FindProperty("FreeVersion");
        EnableShuffle = serializedObject.FindProperty("EnableShuffle");
        TimeOut = serializedObject.FindProperty("TimeOut");
        QuestionCount = serializedObject.FindProperty("QuestionCount");
        PointsPerAnswer = serializedObject.FindProperty("PointsPerAnswer");
        TimeBonusPoints = serializedObject.FindProperty("TimeBonusPoints");
        TimerAmount = serializedObject.FindProperty("TimerAmount");
        CorrectColor = serializedObject.FindProperty("CorrectColor");
        FailColor = serializedObject.FindProperty("FailColor");
        ButtonColor = serializedObject.FindProperty("ButtonColor");
        CorrectSound = serializedObject.FindProperty("CorrectSound");
        FailSound = serializedObject.FindProperty("FailSound");
        RedToggle = serializedObject.FindProperty("RedToggle");
        GreenToggle = serializedObject.FindProperty("GreenToggle");
        Avatars = serializedObject.FindProperty("Avatars");
        EnablePausing = serializedObject.FindProperty("EnablePausing");

        GameQuestions = serializedObject.FindProperty("GameQuestions");
        AllQuestions = serializedObject.FindProperty("AllQuestions");
        FormerQuestions = serializedObject.FindProperty("FormerQuestions");
        NewQuestions = serializedObject.FindProperty("NewQuestions");
        StarredQuestions = serializedObject.FindProperty("StarredQuestions");
        AnswersAvailable = serializedObject.FindProperty("AnswersAvailable");
#endif
    }

    public override void OnInspectorGUI()
    {
#if UNITY_5_6_OR_NEWER
        serializedObject.UpdateIfRequiredOrScript();
#else
		serializedObject.UpdateIfDirtyOrScript();
#endif

        GUIStyle myStyle = new GUIStyle("label");
        myStyle.richText = true;
        myStyle.wordWrap = true;

#if USE_DOTWEEN
        Undo.RecordObject(LocScript, " ");
#endif

        GUILayout.BeginHorizontal();
        {
            GUILayout.BeginVertical();
            {
                GUILayout.Space(10f);

                Texture2D Logo = AssetDatabase.LoadAssetAtPath("Assets/Trivia 365/Editor/EditorUI/Tk.png", typeof(Texture2D)) as Texture2D;
                if (Logo)
                    GUILayout.Label(Logo);
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            {
                string Title = "<size=25>" + "Trivia Kingdom Template" + "</size>";
                string Copyright = "<size=10.5>" + "Copyright © 2017 Mintonne. All rights reserved." + "</size>";

                GUILayout.Label(Title, myStyle, GUILayout.Height(35f));
                GUILayout.Label(Copyright, myStyle);
            }
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.TextArea("", GUI.skin.horizontalSlider);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUILayout.Button(new GUIContent("Documentation", "Hot off the press."), GUILayout.Height(30)))
            Application.OpenURL("https://docs.google.com/document/d/1S0AQb6gYlFJ4PVpSk0PT14_C1Y50scdcclilF1jX2_I/edit?usp=sharing");

        if (GUILayout.Button(new GUIContent("Official Forum", "Stay in the loop."), GUILayout.Height(30)))
            Application.OpenURL("https://forum.unity3d.com/threads/quizapp-ultimate-trivia-template.402269/");

        if (GUILayout.Button(new GUIContent("Contact Us", "Please leave a message after the beep."), GUILayout.Height(30)))
        {
            if (EditorUtility.DisplayDialog("Support", "Wohoo. We thought we'd never hear from you.\n\nDrop us an email and we will get back to you asap, unless it is a Friday afternoon or a Monday morning :)\n\n-A friend.", "Send email", "Cancel"))
                Application.OpenURL("mailto:mintonne@gmail.com");
        }

        if (GUILayout.Button(new GUIContent("Rate this asset", "Share your experience with the world."), GUILayout.Height(30)))
            Application.OpenURL("http://u3d.as/oQJ");

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.Space(10f);

#if !USE_DOTWEEN

		GUI.color = Color.green;
		if (GUILayout.Button (new GUIContent ("Setup QuizApp"), GUILayout.Height (30)))
		WelcomeTour.ShowWindow();
		GUI.color = Color.white;

#else

        GUILayout.BeginHorizontal(GUI.skin.FindStyle("Toolbar"), GUILayout.ExpandWidth(true));
        GUILayout.Label("Dashboard");

        if (GUILayout.Button("Expand All", EditorStyles.toolbarButton, GUILayout.Width(65)))
        {
#if SUPPORTED_PLATFORM
            ShowOnesignal = true;
            ShowAdmob = true;
#endif
            MenuCanvas.isExpanded = true;
            RetrySplashButton.isExpanded = true;
            QuestionTypes.isExpanded = true;
            QuestionDisplay.isExpanded = true;
            AnswerList.isExpanded = true;
            EnablePausing.isExpanded = true;
            GameOverResult.isExpanded = true;
        }

        if (GUILayout.Button("Collapse All", EditorStyles.toolbarButton, GUILayout.Width(71)))
        {
#if SUPPORTED_PLATFORM
            ShowOnesignal = false;
            ShowAdmob = false;
#endif
            MenuCanvas.isExpanded = false;
            RetrySplashButton.isExpanded = false;
            QuestionTypes.isExpanded = false;
            QuestionDisplay.isExpanded = false;
            AnswerList.isExpanded = false;
            GameOverResult.isExpanded = false;
            EnablePausing.isExpanded = false;
        }
        GUILayout.Space(5);
        GUILayout.EndHorizontal();

#if (SUPPORTED_PLATFORM)
        GUILayout.BeginVertical(m_Background);
        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        GUILayout.Space(8);
        ShowOnesignal = EditorGUILayout.Foldout(ShowOnesignal, "OneSignal Configuration");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        if (ShowOnesignal)
        {
#if (USE_ONESIGNAL && SUPPORTED_PLATFORM)
			EditorGUILayout.PropertyField(AppId);

			GUILayout.Space(20);

			GUI.color = Color.red;
			if (GUILayout.Button ("Disable push notifications", GUILayout.Height (20)))
			{
				EditorUtility.DisplayDialog("Disable Push Notifications","Do you want to disable push notifications?","Yes","No");
				{
#if UNITY_ANDROID
					BuildTargetGroup TargetPlatform = BuildTargetGroup.Android;
#elif UNITY_IOS
					BuildTargetGroup TargetPlatform = BuildTargetGroup.iOS;
#endif

					var DefineTarget = PlayerSettings.GetScriptingDefineSymbolsForGroup(TargetPlatform);

					if (DefineTarget.Contains ("USE_ONESIGNAL"))
					{
						string[] curDefineSymbols = DefineTarget.Split (defineSeperators, StringSplitOptions.RemoveEmptyEntries);
						List<string>	newDefineSymbols = new List<string> (curDefineSymbols);
						newDefineSymbols.Remove ("USE_ONESIGNAL");
						PlayerSettings.SetScriptingDefineSymbolsForGroup (TargetPlatform, string.Join (";", newDefineSymbols.ToArray ()));
					}
				}
			}
			GUI.color = Color.white;

#elif (!USE_ONESIGNAL && SUPPORTED_PLATFORM)
            GUI.color = Color.green;
            if (GUILayout.Button("Enable push notifications", GUILayout.Height(20)))
            {
                var option = EditorUtility.DisplayDialogComplex("Enable Push Notifications", "Have you imported the OneSignal Unity Plugin?", "Yes", "No", "Cancel");
                switch (option)
                {

                    case 0:
#if UNITY_ANDROID
                        BuildTargetGroup TargetPlatform = BuildTargetGroup.Android;
#elif UNITY_IOS
						BuildTargetGroup TargetPlatform = BuildTargetGroup.iOS;
#endif

                        var Defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(TargetPlatform);
                        if (Defines.Contains("USE_ONESIGNAL"))
                            return;
                        else
                        {
                            string[] curDefineSymbols = Defines.Split(defineSeperators, StringSplitOptions.RemoveEmptyEntries);
                            List<string> newDefineSymbols = new List<string>(curDefineSymbols);
                            newDefineSymbols.Add("USE_ONESIGNAL");
                            PlayerSettings.SetScriptingDefineSymbolsForGroup(TargetPlatform, string.Join(";", newDefineSymbols.ToArray()));
                        }

                        break;

                    case 1:
                        Application.OpenURL("https://github.com/OneSignal/OneSignal-Unity-SDK/releases");
                        break;
                }
            }

            GUI.color = Color.white;
#endif
        }

        GUILayout.EndVertical();

        GUILayout.BeginVertical(m_Background);
        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        GUILayout.Space(8);
        ShowAdmob = EditorGUILayout.Foldout(ShowAdmob, "Google Admob Configuration");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        if (ShowAdmob)
        {
#if (USE_ADMOB && SUPPORTED_PLATFORM)
			EditorGUILayout.HelpBox ("The ad appears when the game ends based on the frequency you set below.", MessageType.Info);
			EditorGUILayout.PropertyField(InterstitialAdUnitId);
			EditorGUILayout.PropertyField(AdFrequency);

			GUILayout.Space(10);

			EditorGUILayout.PropertyField(TestMode);

			if(TestMode.boolValue)
				EditorGUILayout.PropertyField(AdmobTestID);

			GUILayout.Space(20);

			GUI.color = Color.red;
			if (GUILayout.Button ("Disable Admob Ads", GUILayout.Height (20)))
			{
				EditorUtility.DisplayDialog("Disable Admob Ads","Do you want to disable Admob Ads?","Yes","No");
				{

#if UNITY_ANDROID
					BuildTargetGroup TargetPlatform = BuildTargetGroup.Android;
#elif UNITY_IOS
					BuildTargetGroup TargetPlatform = BuildTargetGroup.iOS;
#endif

					var DefineTarget = PlayerSettings.GetScriptingDefineSymbolsForGroup(TargetPlatform);

					if (DefineTarget.Contains ("USE_ADMOB"))
					{
						string[] curDefineSymbols = DefineTarget.Split (defineSeperators, StringSplitOptions.RemoveEmptyEntries);
						List<string>	newDefineSymbols = new List<string> (curDefineSymbols);
						newDefineSymbols.Remove ("USE_ADMOB");
						PlayerSettings.SetScriptingDefineSymbolsForGroup (TargetPlatform, string.Join (";", newDefineSymbols.ToArray ()));
					}
				}
			}
			GUI.color = Color.white;
#elif (!USE_ADMOB && SUPPORTED_PLATFORM)
            GUI.color = Color.green;
            if (GUILayout.Button("Enable Admob Ads", GUILayout.Height(20)))
            {
                var option = EditorUtility.DisplayDialogComplex("Enable Admob Ads", "Have you imported the Admob Unity Plugin?", "Yes", "No", "Cancel");
                switch (option)
                {
                    case 0:
#if UNITY_ANDROID
                        BuildTargetGroup TargetPlatform = BuildTargetGroup.Android;
#elif UNITY_IOS
						BuildTargetGroup TargetPlatform = BuildTargetGroup.iOS;
#endif

                        var Defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(TargetPlatform);
                        if (Defines.Contains("USE_ADMOB"))
                            return;
                        else
                        {
                            string[] curDefineSymbols = Defines.Split(defineSeperators, StringSplitOptions.RemoveEmptyEntries);
                            List<string> newDefineSymbols = new List<string>(curDefineSymbols);
                            newDefineSymbols.Add("USE_ADMOB");
                            PlayerSettings.SetScriptingDefineSymbolsForGroup(TargetPlatform, string.Join(";", newDefineSymbols.ToArray()));
                        }
                        break;
                    case 1:
                        Application.OpenURL("https://github.com/googleads/googleads-mobile-unity/releases");
                        break;
                }
            }

            GUI.color = Color.white;
#endif
        }

        GUILayout.EndVertical();
#endif

        GUILayout.BeginVertical(m_Background);
        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        GUILayout.Space(5);
        MenuCanvas.isExpanded = EditorGUILayout.Foldout(MenuCanvas.isExpanded, "Game Canvases");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        if (MenuCanvas.isExpanded)
        {
            EditorGUILayout.PropertyField(SplashScreen);
            EditorGUILayout.PropertyField(MenuCanvas);
            EditorGUILayout.PropertyField(GameCanvas);
            EditorGUILayout.PropertyField(PauseCanvas);
            EditorGUILayout.PropertyField(GameOverCanvas);
            EditorGUILayout.PropertyField(ReviewCanvas);
            EditorGUILayout.PropertyField(ReviewTestCanvas);
        }

        GUILayout.EndVertical();

        GUILayout.BeginVertical(m_Background);
        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        GUILayout.Space(5);
        RetrySplashButton.isExpanded = EditorGUILayout.Foldout(RetrySplashButton.isExpanded, "Splash Screen UI");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        if (RetrySplashButton.isExpanded)
        {
            EditorGUILayout.PropertyField(RetrySplashButton);
            EditorGUILayout.PropertyField(SplashScreenLoading);
        }

        GUILayout.EndVertical();

        GUILayout.BeginVertical(m_Background);
        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        GUILayout.Space(5);
        QuestionTypes.isExpanded = EditorGUILayout.Foldout(SoundImage.isExpanded, "Menu UI");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        if (QuestionTypes.isExpanded)
        {
            EditorGUILayout.PropertyField(QuestionTypes);
            EditorGUILayout.PropertyField(QuizMode);
            EditorGUILayout.PropertyField(SoundImage);
            EditorGUILayout.PropertyField(ActiveSoundImage);
            EditorGUILayout.PropertyField(VibrateImage);
            EditorGUILayout.PropertyField(ActiveVibrateImage);
            EditorGUILayout.PropertyField(MenuHandler);
            EditorGUILayout.PropertyField(Confirmation);
        }

        GUILayout.EndVertical();

        GUILayout.BeginVertical(m_Background);
        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        GUILayout.Space(5);
        QuestionDisplay.isExpanded = EditorGUILayout.Foldout(QuestionDisplay.isExpanded, "Game UI");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        if (QuestionDisplay.isExpanded)
        {
            EditorGUILayout.PropertyField(ImageLoadingAnimation);
            EditorGUILayout.PropertyField(ImageErrorPanel);
            EditorGUILayout.PropertyField(QuestionDisplay);
            EditorGUILayout.PropertyField(AnswerList);
            EditorGUILayout.PropertyField(ImageDisplay);
            EditorGUILayout.PropertyField(ResultDisplay);
            EditorGUILayout.PropertyField(QuizCounter);
            EditorGUILayout.PropertyField(QuitPanel);
            EditorGUILayout.PropertyField(ReturnPanel);
            EditorGUILayout.PropertyField(StarButton);
            EditorGUILayout.PropertyField(ActiveStar);
            EditorGUILayout.PropertyField(InactiveStar);
        }

        GUILayout.EndVertical();

        GUILayout.BeginVertical(m_Background);
        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        GUILayout.Space(5);
        GameOverResult.isExpanded = EditorGUILayout.Foldout(GameOverResult.isExpanded, "GameOver UI");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        if (GameOverResult.isExpanded)
        {
            EditorGUILayout.PropertyField(GameOverResult);
            EditorGUILayout.PropertyField(GameOverTip);
            EditorGUILayout.PropertyField(PointsText);
            EditorGUILayout.PropertyField(BonusPointsText);
            EditorGUILayout.PropertyField(GameOverPercentage);
            EditorGUILayout.PropertyField(GameOverCircle);
        }
        GUILayout.EndVertical();

        GUILayout.BeginVertical(m_Background);
        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        GUILayout.Space(5);
        EnablePausing.isExpanded = EditorGUILayout.Foldout(EnablePausing.isExpanded, "Game Variables");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        if (EnablePausing.isExpanded)
        {
            EditorGUILayout.PropertyField(Mode);

            if (LocScript.Mode == DownloadMode.Online)
            {
                GUI.color = Color.red;

                if (LocScript.OnlinePath.Length < 1)
                    EditorGUILayout.HelpBox("Enter the URL to the XML in the field below", MessageType.None);
                else
                {
                    GUI.color = Color.green;
                    EditorGUILayout.HelpBox("Download URL is set", MessageType.None);
                }

                GUI.color = Color.white;

                EditorGUILayout.PropertyField(OnlinePath);
            }
            else if (LocScript.Mode == DownloadMode.Offline)
            {

                GUI.color = Color.red;

                if (LocScript.OfflinePath.Length < 1)
                    EditorGUILayout.HelpBox("Enter the Resources folder path to the XML in the field below", MessageType.None);
                else
                {
                    GUI.color = Color.green;
                    EditorGUILayout.HelpBox("Resources path is set", MessageType.None);
                }

                GUI.color = Color.white;
                EditorGUILayout.PropertyField(OfflinePath);
            }
            else
            {
                EditorGUILayout.HelpBox("Enter the URL (optional) & Resources folder path (required) to the XML respectively in the fields below", MessageType.Info);
                EditorGUILayout.PropertyField(OnlinePath);
                EditorGUILayout.PropertyField(OfflinePath);
            }
            EditorGUILayout.PropertyField(FreeVersion);
            EditorGUILayout.PropertyField(EnableShuffle);
            EditorGUILayout.PropertyField(TimeOut);
            EditorGUILayout.PropertyField(QuestionCount);
            EditorGUILayout.PropertyField(PointsPerAnswer);
            EditorGUILayout.PropertyField(TimeBonusPoints);
            EditorGUILayout.PropertyField(TimerAmount);
            EditorGUILayout.PropertyField(CorrectColor);
            EditorGUILayout.PropertyField(FailColor);
            EditorGUILayout.PropertyField(ButtonColor);
            EditorGUILayout.PropertyField(CorrectSound);
            EditorGUILayout.PropertyField(FailSound);
            EditorGUILayout.PropertyField(RedToggle);
            EditorGUILayout.PropertyField(GreenToggle);
            EditorGUILayout.PropertyField(EnablePausing);

            EditorGUILayout.PropertyField(AnswersAvailable, true);
        }

        GUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
#endif
    }

    private Texture2D MakeTex(Color col)
    {
        Color[] pix = new Color[1 * 1];

        for (int i = 0; i < pix.Length; i++)
            pix[i] = col;

        Texture2D result = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        result.hideFlags = HideFlags.HideAndDontSave;
        result.SetPixels(pix);
        result.Apply();

        return result;
    }

    private void OnInspectorUpdate()
    {
        this.Repaint();
    }
}