#if (UNITY_ANDROID || UNITY_IOS)
#define SUPPORTED_PLATFORM
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
#define USE_ANDROID
#endif

#if !USE_DOTWEEN
#pragma warning disable 0414
#endif

using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
using Mintonne.QuizApp;
using MaterialUI;
using System.Text;
using System.Linq;

#if USE_ONESIGNAL
using OneSignalPush.MiniJSON;
#endif
#if USE_DOTWEEN
using DG.Tweening;
#endif

[XmlRoot("QuestionDatabase")]
public class TKContainer
{
	[XmlElement("Questions")]
	public List<Questions> Quizes = new List<Questions>();

    public static TKContainer Load(string path)
	{
		TextAsset _xml = Resources.Load<TextAsset>(path);

		XmlSerializer serializer = new XmlSerializer(typeof(TKContainer));

		StringReader reader = new StringReader(_xml.text);

		TKContainer Items = serializer.Deserialize(reader) as TKContainer;

#if UNITY_METRO
		reader.Dispose();
#else
		reader.Close();
#endif

		return Items;
	}

	public static TKContainer DownloadedXML(StringReader OnlineXML)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(TKContainer));

		TKContainer Items = serializer.Deserialize(OnlineXML) as TKContainer;

#if UNITY_METRO
		OnlineXML.Dispose();
#else
		OnlineXML.Close();
#endif

		return Items;
	}
}

public class MainController : MonoBehaviour
{

	#if USE_ADMOB
	

#if UNITY_ANDROID
	[SerializeField]
	private string InterstitialAdUnitId = "INSERT_INTERSTITIAL_AD_UNIT_ID_HERE";

#elif UNITY_IPHONE
	[SerializeField]
	private string InterstitialAdUnitId = "INSERT_INTERSTITIAL_AD_UNIT_ID_HERE";

#else
	[SerializeField]
	private const string InterstitialAdUnitId = "unexpected_platform";
#endif
	
	[Tooltip("How often should the ad appear?")]
	[Range(1, 30)]
	public int AdFrequency = 2;

	[Tooltip("Enable Admob Test Mode.")]
	public bool TestMode = false;

	[Tooltip("The Test Device ID for Admob")]
	public String AdmobTestID = "ENTER_TEST_DEVICE_ID_FOR_ADMOB";

	private int AdCount;
#endif

	#if (USE_ONESIGNAL && SUPPORTED_PLATFORM)
	[Tooltip("Your OneSignal App ID")]
	[SerializeField]
	private string AppId = "ENTER_YOUR_ONESIGNAL_APP_ID";
#endif

	[Tooltip("The Splash Screen")]
	public GameObject SplashScreen;
	[Tooltip("The Menu Canvas")]
	public GameObject MenuCanvas;
	[Tooltip("The Game Canvas")]
	public GameObject GameCanvas;
	[Tooltip("The Pause Canvas")]
	public GameObject PauseCanvas;
	[Tooltip("The GameOver Canvas")]
	public GameObject GameOverCanvas;
    [Tooltip("The Review Canvas")]
    public GameObject ReviewCanvas;
    [Tooltip("The Review Canvas")]
    public GameObject ReviewTestCanvas;

    [Tooltip("The Retry Button on the splash screen")]
	public GameObject RetrySplashButton;
	[Tooltip("The loading animation on the splash screen")]
	public Animation SplashScreenLoading;
    
    
	[Tooltip("Question Types Radio")]
	public GameObject QuestionTypes;
	[Tooltip("Quiz Mode Radio")]
	public GameObject QuizMode;
	[Tooltip("The sound button image")]
	public GameObject SoundImage;
    [Tooltip("The sound button image")]
    public GameObject ActiveSoundImage;
    [Tooltip("The vibrate button image")]
	public GameObject VibrateImage;
    [Tooltip("The vibrate button image")]
    public GameObject ActiveVibrateImage;
    [Tooltip("The main menu handler")]
    public MainMenuHandler MenuHandler;
    [Tooltip("The confirmation handler")]
    public GameObject Confirmation;

    [Tooltip("The Loading Animation gameoject")]
	public Animation ImageLoadingAnimation;
	[Tooltip("The Retry Button")]
	public GameObject ImageErrorPanel;
	[Tooltip("Text component used to display the question")]
	public Text QuestionDisplay;
    [Tooltip("Image component used to display the image")]
	public Image ImageDisplay;
    [Tooltip("Text component used to display the result of the question")]
	public Text ResultDisplay;
	[Tooltip("Text component used to display the number of questions answered")]
	public Text QuizCounter;
    [Tooltip("Four answers - No image choice buttons")]
    public GameObject AnswerList;
    [Tooltip("The quit game panel")]
	public GameObject QuitPanel;
    [Tooltip("The star button")]
    public Image StarButton;
    [Tooltip("The active star button")]
    public Sprite ActiveStar;
    [Tooltip("The inactive star button")]
    public Sprite InactiveStar;

    [Tooltip("Text component used to display the lives left")]
	public Text GameOverResult;
	[Tooltip("Text component used to display the time left")]
	public Text GameOverTip;
	[Tooltip("Text component used to display the points")]
	public Text PointsText;
	[Tooltip("Text component used to display the bonus time points")]
	public Text BonusPointsText;
	[Tooltip("Text component used to display the percentage score")]
	public Text GameOverPercentage;
	[Tooltip("The fill circle image")]
	public Image GameOverCircle;

	[Tooltip("Where should we load the questions from")]
	public DownloadMode Mode = DownloadMode.Offline;
    [Tooltip("Whether app is free version or not")]
    public bool FreeVersion;
    [Tooltip("The download link to the XML file")]
    public String OnlinePath;
    [Tooltip("The XML File path")]
    public String OfflinePath = "XML/Offline XML";
    [Tooltip("The Free XML File path")]
    public String OfflinePathFree = "XML/Offline XML Free";
    [Tooltip("Enable or disable shuffling the questions?")]
	public bool EnableShuffle = true;
	[Tooltip("The time in seconds before a download is declared to have failed. Make sure to factor devices with slow internet.")]
	[Range(5, 30)]
	public int TimeOut = 10;
	[Tooltip("The number of question to unlock the next level")]
	public int QuestionCount;
	[Tooltip("The number of points for every correct answer")]
	public int PointsPerAnswer;
	[Tooltip("The number of points to multiply with the time left")]
	public int TimeBonusPoints;
	[Tooltip("The amount of time allowed for each level")]
	public float TimerAmount;
	[Tooltip("The color to mark the button with the correct answer")]
	public Color CorrectColor = Color.green;
	[Tooltip("The color to mark the button with the wrong answer")]
	public Color FailColor = Color.red;
    [Tooltip("The color to mark the button with the correct answer")]
    public Color HighlightedColor;
    [Tooltip("The color to mark the button with the correct answer")]
    public Color NormalTextColor;
    [Tooltip("The color to mark the button with the correct answer")]
    public Color DisabledColor;
    [Tooltip("The original color of the button")]
	public Color ButtonColor;
	[Tooltip("An array that holds the correct answer sound effects")]
	public AudioClip CorrectSound;
	[Tooltip("An array that holds the fail sound effects")]
	public AudioClip FailSound;
	[Tooltip("The image to display when the game sound is muted")]
	public Sprite RedToggle;
	[Tooltip("The image to display when the game sound is ON")]
	public Sprite GreenToggle;
	[Tooltip("Enable pausing in-game")]
	public bool EnablePausing = true;

	[HideInInspector]
	public Dictionary<int, Questions> GameQuestions = new Dictionary<int, Questions>();

    [HideInInspector]
    public List<int> AllQuestions;

    [HideInInspector] 
    public List<int> NewQuestions;

    [HideInInspector]
    public List<int> FormerQuestions;

    [HideInInspector]
    public List<int> StarredQuestions;

    [HideInInspector]
	public List<AnswerClass> AnswersAvailable;
    [HideInInspector]
    public List<AnswerClass> OriginalAnswersAvailable;

    [HideInInspector]
    public List<int> TestQuestions = new List<int>();

    public GameObject questionScrollRect;
    public GameObject answerPrefab;
    public GameObject submitPrefab;
    public GameObject questionControllerPrefab;
    public GameObject questionControllerSubmitPrefab;
    public GameObject noAnswerPopup;
    public GameObject fade;
    public General general;
    public GameObject numberOfQuestionsErrorPanel;
    public bool hasAnswered;
    public Scroller ScrollerComponent;
	private List<String> ImageLinks = new List<string>();
	private List<String> FaultyLinks = new List<string>();
	private AudioSource SoundFX;
    
	private int Sound = 0;
	private int CorrectAnswer = 0;
	private int CorrectlyAnswered = 0;
	private int QuestionsAsked;
	private int CurrentQuestion;
	private int ActiveAvatar;
	private bool IsCorrect;
	private bool IsGameOver;
	private bool Paused;
	private bool isMenu;
	private bool Playing;
	private bool OnlineMode;
    private string quizMode;
    private string currentQuestionType;
    private int AnswersCount;
    private bool hasImage;
    private int selectedAnswer;
    private GameObject QuestionController;
    private GameObject nextButton;
    private GameObject submitButton;
    private GameObject previousButton;
	private Text AnswerA, AnswerB, AnswerC, AnswerD, AnswerE, AnswerF, AnswerG, AnswerH;
	#if (UNITY_EDITOR || SUPPORTED_PLATFORM)
	private bool CanVibrate;
	#endif
	private string Fact;

    private Transform A, B, C, D, E, F, G, H;

    internal bool Homepage = true;

	private bool CacheFailed;

	private static readonly char[] RemoveChar = new char[]{ ' ', ';', ',', '/', '=', '"' };

	#if USE_DOTWEEN
	void Start()
	{
        Screen.fullScreen = false;
        ApplicationChrome.statusBarState = ApplicationChrome.navigationBarState = ApplicationChrome.States.TranslucentOverContent;
        Application.targetFrameRate = 60;
        //SaveLoad.Delete();
        SaveLoad.Load();
        DOTween.Init();
        hasAnswered = false;

#if (USE_ONESIGNAL && SUPPORTED_PLATFORM)
		//Enable line below to enable logging if you are having issues setting up OneSignal. (logLevel, visualLogLevel)
		//OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.INFO, OneSignal.LOG_LEVEL.INFO);

		//Initiate the App Id
		OneSignal.StartInit(AppId).InFocusDisplaying(OneSignal.OSInFocusDisplayOption.None).EndInit();
#endif

#if USE_ADMOB

		if(TestMode)
		{
		AdmobManager.instance.TestMode = true;
		AdmobManager.instance.AdmobTestID = AdmobTestID;
		}
		else
		AdmobManager.instance.TestMode = false;
#endif

		//PlayerPrefs.DeleteAll();
		Time.timeScale = 1;

        //Get the audiosource component
        SoundFX = GetComponent<AudioSource>();
		//Get the sound state( 1 - Sound on. 0 - Sound off)
		Sound = PlayerPrefs.GetInt("Sound", 1);

		if (Sound == 0)
		{
			AudioListener.pause = true;
            SoundImage.SetActive(true);
            ActiveSoundImage.SetActive(false);
        }
		else if (Sound == 1)
		{
			AudioListener.pause = false;
            SoundImage.SetActive(false);
            ActiveSoundImage.SetActive(true);
        }
		else
		{
			PlayerPrefs.SetInt("Sound", 1);
            SoundImage.SetActive(false);
            ActiveSoundImage.SetActive(true);
            AudioListener.pause = false;
		}

#if (UNITY_EDITOR || SUPPORTED_PLATFORM)
		int VibrationState = PlayerPrefs.GetInt("Vibration", 1);

        if (VibrationState == 0)
		{
			CanVibrate = false;
            VibrateImage.SetActive(true);
            ActiveVibrateImage.SetActive(false);
        }
		else if (VibrationState == 1)
		{
			CanVibrate = true;
            VibrateImage.SetActive(false);
            ActiveVibrateImage.SetActive(true);
        }
		else
		{
			PlayerPrefs.SetInt("Vibration", 1);
            VibrateImage.SetActive(false);
            ActiveVibrateImage.SetActive(true);
            CanVibrate = true;
		}
#else
		VibrateImage.transform.parent.gameObject.SetActive(false);
#endif
		CurrentQuestion = 0;
		//Reset timescale
		Time.timeScale = 1;

		if (GameCanvas)
			GameCanvas.SetActive(false);
		if (GameOverCanvas)
			GameOverCanvas.SetActive(false);
		if (PauseCanvas)
			PauseCanvas.SetActive(false);
		if (MenuCanvas)
			MenuCanvas.SetActive(false);
		if (SplashScreen)
			SplashScreen.SetActive(false);

		//Invoke the load question coroutine
		if (Mode == DownloadMode.Offline)
			StartCoroutine(LoadQuestions());
		else if (Mode == DownloadMode.Online)
			StartCoroutine(DownlaodXML());
		else
			StartCoroutine(HybridLoader());
	}

	private void Update()
	{

		//Handle the back key press
		if (Input.GetKeyDown(KeyCode.Escape) && !IsGameOver && !Paused && Playing)
			PauseGame();
		else if (Input.GetKeyDown(KeyCode.Escape) && !IsGameOver && Paused && Playing)
			ResumeGame();
		else if (Input.GetKeyDown(KeyCode.Escape) && IsGameOver && !Paused && !isMenu && !Playing)
			ShowMenu();
		else if (Input.GetKeyDown(KeyCode.Escape) && IsGameOver && !Paused && isMenu && !Playing && !Homepage)
			ScrollerComponent.LerpToPage(0);
		else if (Input.GetKeyDown(KeyCode.Escape) && IsGameOver && !Paused && isMenu && !Playing && Homepage)
			Application.Quit();
	}

	//Change sound state
	public void ToggleSound()
	{

		//Get sound state
		Sound = PlayerPrefs.GetInt("Sound", 1);

		//Mute or unmute depending on the current state
		if (Sound == 0)
		{
			AudioListener.pause = false;
			PlayerPrefs.SetInt("Sound", 1);
        }
		else if (Sound == 1)
		{
			AudioListener.pause = true;
			PlayerPrefs.SetInt("Sound", 0);
        }

		SoundFX.Stop();
	}

	#if (UNITY_EDITOR || SUPPORTED_PLATFORM)
	public void ToggleVibration()
	{
		int VibrationState = PlayerPrefs.GetInt("Vibration", 1);
		if (VibrationState == 0)
		{
			CanVibrate = true;
            PlayerPrefs.SetInt("Vibration", 1);

		}
		else if (VibrationState == 1)
		{
			CanVibrate = false;
            PlayerPrefs.SetInt("Vibration", 0);

		}
	}
	#endif

	public void ShowMenu()
    { 
        SplashScreenLoading.Stop();
		SplashScreenLoading.gameObject.SetActive(false);
		RetrySplashButton.SetActive(false);

        if (GameCanvas)
			GameCanvas.SetActive(false);
		if (GameOverCanvas)
			GameOverCanvas.SetActive(false);
		if (PauseCanvas)
			PauseCanvas.SetActive(false);
		if (SplashScreen)
			SplashScreen.SetActive(false);
		if (MenuCanvas)
			MenuCanvas.SetActive(true);
        if (ReviewCanvas)
            ReviewCanvas.SetActive(false);
        if (ReviewTestCanvas)
            ReviewTestCanvas.SetActive(false);


        
        isMenu = true;
		Playing = false;
		Paused = false;
		IsGameOver = true;

        StopAllCoroutines();
	}

    public void ScrollToTop()
    {
        questionScrollRect.GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 1);
    }

    public void UpdateLayout()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(AnswerList.transform.parent.GetComponent<RectTransform>());
    }

    public void ShowNoAnswerPopup()
    {
        noAnswerPopup.SetActive(true);
    }

    public void HideNoAnswerPopup()
    {
        noAnswerPopup.SetActive(false);
    }

    public void ShowReviewPage()
    {
        if (GameCanvas)
            GameCanvas.SetActive(false);
        if (GameOverCanvas)
            GameOverCanvas.SetActive(false);
        if (PauseCanvas)
            PauseCanvas.SetActive(false);
        if (MenuCanvas)
            MenuCanvas.SetActive(false);

        StartCoroutine(QuestionReviewContent());

    }
    private IEnumerator QuestionReviewContent()
    {
        if (SplashScreen)
        {
            SplashScreen.SetActive(true);
            SplashScreenLoading.gameObject.SetActive(true);
            SplashScreenLoading.Play();
        }
        
        yield return new WaitForSeconds(2);
        ReviewCanvas.GetComponent<ReviewQuestions>().loadQuestions();
        ReviewCanvas.SetActive(true);
        
        if (SplashScreen)
        {
            SplashScreen.SetActive(false);
            SplashScreenLoading.gameObject.SetActive(false);
            SplashScreenLoading.Stop();
        }
        ReviewCanvas.GetComponent<ReviewQuestions>().displayTheQuestions();
    }

    public void ShowTestReviewPage(bool starred = false)
    {
        if (GameCanvas)
            GameCanvas.SetActive(false);
        if (GameOverCanvas)
            GameOverCanvas.SetActive(false);
        if (PauseCanvas)
            PauseCanvas.SetActive(false);
        if (SplashScreen)
            SplashScreen.SetActive(false);
        if (MenuCanvas)
            MenuCanvas.SetActive(false);
        if (ReviewCanvas)
            ReviewCanvas.SetActive(false);

        StartCoroutine(TestQuestionReviewContent(starred));
    }

    private IEnumerator TestQuestionReviewContent(bool starred)
    {
        if (SplashScreen)
        {
            SplashScreen.SetActive(true);
            SplashScreenLoading.gameObject.SetActive(true);
            SplashScreenLoading.Play();
        }

        yield return new WaitForSeconds(2);
        ReviewTestCanvas.GetComponent<ReviewTestQuestions>().LoadQuestions(starred);
        ReviewTestCanvas.SetActive(true);

        if (SplashScreen)
        {
            SplashScreen.SetActive(false);
            SplashScreenLoading.gameObject.SetActive(false);
            SplashScreenLoading.Stop();
        }
        ReviewTestCanvas.GetComponent<ReviewTestQuestions>().displayTheQuestions();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
	{
		if (!EnablePausing)
		{
			QuitPanel.SetActive(true);
			return;
		}

		if (GameCanvas)
			GameCanvas.SetActive(false);
		if (GameOverCanvas)
			GameOverCanvas.SetActive(false);
		if (PauseCanvas)
			PauseCanvas.SetActive(true);
		if (MenuCanvas)
			MenuCanvas.SetActive(false);
        if (ReviewCanvas)
            ReviewCanvas.SetActive(false);
        if (ReviewTestCanvas)
            ReviewTestCanvas.SetActive(false);
        Paused = true;
	}

    public void ReturnHome()
    {
        ReviewTestCanvas.SetActive(false);
        ReviewCanvas.SetActive(false);
        Start();
    }

	public void ResumeGame()
	{
        hasAnswered = false;
		if (GameCanvas)
			GameCanvas.SetActive(true);
		if (GameOverCanvas)
			GameOverCanvas.SetActive(false);
		if (PauseCanvas)
			PauseCanvas.SetActive(false);
		if (MenuCanvas)
			MenuCanvas.SetActive(false);
        if (ReviewCanvas)
            ReviewCanvas.SetActive(false);
        if (ReviewTestCanvas)
            ReviewTestCanvas.SetActive(false);
        Paused = false;
	}

	//Close the Quit Panel
	public void CloseQuitPanel()
	{
		QuitPanel.SetActive(false);
	}
    public void CloseConfirmationPanel()
    {
        Confirmation.SetActive(false);
    }

    public void ShowNumberOfQuestionsError()
    {
        numberOfQuestionsErrorPanel.SetActive(true);
    }

    public void HideNumberOfQuestionsError()
    {
        numberOfQuestionsErrorPanel.SetActive(false);
    }

    public void PlayGame()
    {
        if (general.getTotalTest() >= 8)
        {
            Confirmation.SetActive(true);
            return;
        }

        ContinuePlayGame();
    }

    public void ContinuePlayGame()
    {

        int numOfQuestions = MenuHandler.getNumberOfQuestions();
        currentQuestionType = MenuHandler.getQuestionType();

        if (numOfQuestions <= 0)
        {
            numOfQuestions = 1;
        }

        if (currentQuestionType == "All")
        {
            if (AllQuestions.Count < numOfQuestions)
            {
                numOfQuestions = AllQuestions.Count;
            }
        }
        else if (currentQuestionType == "Former")
        {
            if(FormerQuestions.Count == 0)
            {
                ShowNumberOfQuestionsError();
                return;
            }

            if (FormerQuestions.Count < numOfQuestions)
            {
                numOfQuestions = FormerQuestions.Count;
            }
        }
        else if (currentQuestionType == "New")
        {
            if (NewQuestions.Count == 0)
            {
                ShowNumberOfQuestionsError();
                return;
            }

            if (NewQuestions.Count < numOfQuestions)
            {
                numOfQuestions = NewQuestions.Count;
            }
        }
        else if (currentQuestionType == "Starred")
        {
            if (StarredQuestions.Count == 0)
            {
                ShowNumberOfQuestionsError();
                return;
            }
            if (StarredQuestions.Count < numOfQuestions)
            {
                numOfQuestions = StarredQuestions.Count;
            }
        }

        hasAnswered = false;
        SplashScreen.SetActive(true);
        CloseConfirmationPanel();
        TestQuestions.Clear();
        
        quizMode = MenuHandler.getQuizMode();
       
        AllQuestions.ShuffleArray();

        for (int i = 0; i < AllQuestions.Count; i++)
        {
            if (numOfQuestions == TestQuestions.Count)
            {
                break;
            }

            Questions possibleQuestion = GameQuestions[AllQuestions[i]];

            int id = possibleQuestion.ID;

            if (currentQuestionType == "Former")
            {
                if (FormerQuestions.Contains(id))
                {
                    TestQuestions.Add(id);
                }
            }
            else if (currentQuestionType == "New")
            {
                if (!FormerQuestions.Contains(id))
                {
                    TestQuestions.Add(id);
                }
            }
            else if (currentQuestionType == "Starred")
            {
                if (StarredQuestions.Contains(id))
                {
                    TestQuestions.Add(id);
                }
            }
            else
            {
                TestQuestions.Add(id);
            }
        }
		//Increment the current question counter if we have more questions
		if (CurrentQuestion < GameQuestions.Count - 1)
        {
            CurrentQuestion=0;
            Test.current = new Test();
            Test.current.setTestID();
            Test.current.setTestDate();
            Test.current.setNumberOfQuestions(numOfQuestions);
            Test.current.setQuestionList(TestQuestions);
            Test.current.setQuestionType(MenuHandler.getQuestionType());
            Test.current.setTestType(MenuHandler.getQuizMode());
            Test.current.setCompleteStatus(false);
            Test.current.setCurrentQuestion(1);

            SaveLoad.Save();
        }
        QuestionCount = numOfQuestions;
		CorrectlyAnswered = 0;
		QuestionsAsked = 1;

		StartCoroutine(NextQuestion());

		//Hide the quit panel
		QuitPanel.SetActive(false);

		//Show game canvas
		if (GameCanvas)
        {
            GameCanvas.SetActive(true);
            SplashScreen.SetActive(false);
        }
		if (GameOverCanvas)
			GameOverCanvas.SetActive(false);
		if (PauseCanvas)
			PauseCanvas.SetActive(false);
		if (MenuCanvas)
			MenuCanvas.SetActive(false);
        if (ReviewTestCanvas)
            ReviewTestCanvas.SetActive(false);
        if (ReviewCanvas)
            ReviewCanvas.SetActive(false);

        noAnswerPopup.SetActive(false);
        isMenu = false;
		Playing = true;
		IsGameOver = false;
		Paused = false;

		if (ResultDisplay)
			ResultDisplay.gameObject.SetActive(false);

		ImageDisplay.gameObject.SetActive(false);
        AnswerList.gameObject.SetActive(false);
        QuestionDisplay.gameObject.SetActive(false);

#if (USE_ADMOB && SUPPORTED_PLATFORM)
		AdmobManager.instance.RequestInterstitial(InterstitialAdUnitId);
#endif

        if (OnlineMode)
		{
			ImageLinks.Clear();

			for (int a = 1; a < GameQuestions.Count; a++)
				if (GameQuestions[a].Image.Length > 1)
					ImageLinks.Add(GameQuestions[a].Image);

			StartCoroutine(CacheImages());
		}
	}

    public void ResetTest()
    {
        TestQuestions = Test.current.getQuestionList();
        int numOfQuestions = Test.current.getNumberOfQuestions();
        quizMode = Test.current.getTestType();
        currentQuestionType = Test.current.getQuestionType();

        TestQuestions.ShuffleArray();
        CurrentQuestion = 0;
        //Increment the current question counter if we have more questions
        if (CurrentQuestion < GameQuestions.Count - 1)
        {
            CurrentQuestion = 0;
            Test.current.setTestDate();
            Test.current.setQuestionList(TestQuestions);
            Test.current.setCompleteStatus(false);
            Test.current.setCurrentQuestion(1);
            Test.current.clearQuestionAnswer();

            SaveLoad.Save();
        }
        QuestionCount = Test.current.getNumberOfQuestions();
        CorrectlyAnswered = 0;
        QuestionsAsked = 1;

        StartCoroutine(NextQuestion());

        //Hide the quit panel
        QuitPanel.SetActive(false);

        //Show game canvas
        if (GameCanvas)
            GameCanvas.SetActive(true);
        if (GameOverCanvas)
            GameOverCanvas.SetActive(false);
        if (PauseCanvas)
            PauseCanvas.SetActive(false);
        if (MenuCanvas)
            MenuCanvas.SetActive(false);
        if (ReviewTestCanvas)
            ReviewTestCanvas.SetActive(false);
        if (ReviewCanvas)
            ReviewCanvas.SetActive(false);

        MenuHandler.resumeTestPanel.SetActive(false);
        MenuHandler.reviewTestPanel.SetActive(false);

        isMenu = false;
        Playing = true;
        IsGameOver = false;
        Paused = false;

        if (ResultDisplay)
            ResultDisplay.gameObject.SetActive(false);

        ImageDisplay.gameObject.SetActive(false);
        AnswerList.gameObject.SetActive(false);
        QuestionDisplay.gameObject.SetActive(false);

#if (USE_ADMOB && SUPPORTED_PLATFORM)
		AdmobManager.instance.RequestInterstitial(InterstitialAdUnitId);
#endif

        if (OnlineMode)
        {
            ImageLinks.Clear();

            for (int a = 1; a < GameQuestions.Count; a++)
                if (GameQuestions[a].Image.Length > 1)
                    ImageLinks.Add(GameQuestions[a].Image);

            StartCoroutine(CacheImages());
        }
    }

    public void ResumeTest()
    {
        TestQuestions = Test.current.getQuestionList();
        
        int numOfQuestions = Test.current.getNumberOfQuestions();
        quizMode = Test.current.getTestType();
        currentQuestionType = Test.current.getQuestionType();
        CurrentQuestion = Test.current.getCurrentQuestion();
        QuestionCount = Test.current.getNumberOfQuestions();
        CorrectlyAnswered = Test.current.getCorrectQuestions().Count;
        QuestionsAsked = Test.current.getCurrentQuestion()+1;

        if(CurrentQuestion == 0)
        {
            CurrentQuestion = 1;
        }

        StartCoroutine(NextQuestion());

        //Hide the quit panel
        QuitPanel.SetActive(false);

        //Show game canvas
        if (GameCanvas)
            GameCanvas.SetActive(true);
        if (GameOverCanvas)
            GameOverCanvas.SetActive(false);
        if (PauseCanvas)
            PauseCanvas.SetActive(false);
        if (MenuCanvas)
            MenuCanvas.SetActive(false);
        if (ReviewTestCanvas)
            ReviewTestCanvas.SetActive(false);
        if (ReviewCanvas)
            ReviewCanvas.SetActive(false);

        MenuHandler.resumeTestPanel.SetActive(false);
        MenuHandler.reviewTestPanel.SetActive(false);

        isMenu = false;
        Playing = true;
        IsGameOver = false;
        Paused = false;

        if (ResultDisplay)
            ResultDisplay.gameObject.SetActive(false);

        ImageDisplay.gameObject.SetActive(false);
        AnswerList.gameObject.SetActive(false);
        QuestionDisplay.gameObject.SetActive(false);

#if (USE_ADMOB && SUPPORTED_PLATFORM)
		AdmobManager.instance.RequestInterstitial(InterstitialAdUnitId);
#endif

        if (OnlineMode)
        {
            ImageLinks.Clear();

            for (int a = 1; a < GameQuestions.Count; a++)
                if (GameQuestions[a].Image.Length > 1)
                    ImageLinks.Add(GameQuestions[a].Image);

            StartCoroutine(CacheImages());
        }
    }

    //Load the question from the xml to the questions array
    private IEnumerator LoadQuestions()
	{
		//Show the splash screen
		SplashScreen.SetActive(true);

		//Hide the retry button
		RetrySplashButton.SetActive(false);

		//Activate the splash screen loading animation and start playing it
		SplashScreenLoading.gameObject.SetActive(true);
		SplashScreenLoading.Play();

        //load the questions from the XML
        string loader = OfflinePath;

        if (FreeVersion)
        {
            loader = OfflinePathFree;
        }
        TKContainer ic = TKContainer.Load(loader);

        AllQuestions.Clear();
        FormerQuestions.Clear();
        StarredQuestions.Clear();
        NewQuestions.Clear();

        if (GameQuestions.Count > 0)
        {
            GameQuestions.Clear();
        }
        //GeneralData.Delete();
        GeneralData.Load();
        if(GeneralData.general == null)
        {
            GeneralData.general = new General();
        }

        general = GeneralData.general;

        if(general.getAnsweredQuestions() != null)
        {
            FormerQuestions = general.getAnsweredQuestions();
            StarredQuestions = general.getStarredQuestions();
        }
        //Parse the questions from the XML file to the list 
        foreach (Questions item in ic.Quizes)
        {
            GameQuestions.Add(item.ID, item);
            AllQuestions.Add(item.ID);
            if(!FormerQuestions.Contains(item.ID))
            {
                NewQuestions.Add(item.ID);
            }
        }
        MenuHandler.questionTypeRadio.triggerToggle(0);
        MenuHandler.quizModeRadio.triggerToggle(0);
        MenuHandler.changeLimit("All");

		OnlineMode = false;

		yield return new WaitForSeconds(2);
     
        MenuHandler.displayPreviousTests();
		ShowMenu();

#if USE_ADMOB
		AdCount = AdFrequency;
#endif
	}

	//Load the question from the xml we download
	private IEnumerator DownlaodXML()
	{
		//Show the splash screen
		SplashScreen.SetActive(true);

		//Initialize a new bool and set it to false. We will use this to retry a download in case it fails on the first try
		bool retried = false;

		//Hide the retry button
		RetrySplashButton.SetActive(false);

		//Activate the splash screen loading animation and start playing it
		SplashScreenLoading.gameObject.SetActive(true);
		SplashScreenLoading.Play();

		//We will use this if we need to retry a download in case it fails on the first try
		TryAgain:

		// Start a download of the given URL
		WWW www = new WWW(OnlinePath);

		//Set the timeout to the set time (in seconds)
		int timeout = TimeOut;

		//Check if the download is done before the timeout countdown runs out
		while (!www.isDone && www.error == null)
		{
			//Wait for one second
			yield return new WaitForSeconds(1);

			//Reduce the countdown time by one
			timeout--;

			//If the countdown runs out, dispose the download and show the retry button
			if (timeout <= 0)
			{

				SplashScreenLoading.Stop();
				SplashScreenLoading.gameObject.SetActive(false);
				RetrySplashButton.SetActive(true);

				www.Dispose();

				yield break;
			}
		}

		//If the download is successful, start parsing the questions
		if (www.error == null && www.isDone)
		{

			//Send the file to the parser.
			StringReader reader = new StringReader(www.text);
			TKContainer Qac = TKContainer.DownloadedXML(reader);

			//Clear List<QuizApp> contents
			if (GameQuestions.Count > 0)
				GameQuestions.Clear();

			//Copy the questions from the XML file to the List<>
			foreach (Questions item in Qac.Quizes)
				GameQuestions.Add(item.ID, item);

			OnlineMode = true;

			//Show the main menu
			ShowMenu();
		}
		else
		{
			//If the download failed and we have not retried it, restart the download in 2 seconds
			if (!retried)
			{
				yield return new WaitForSeconds(2);
				retried = true;
				goto TryAgain;
			}

			//If the download failed and we have retried, show the retry button and hide the animation.
			SplashScreenLoading.Stop();
			SplashScreenLoading.gameObject.SetActive(false);
			RetrySplashButton.SetActive(true);
		}
	}

    public int getTotal(string type)
    {
        if(type == "All")
        {
            return AllQuestions.Count;
        }
        else if(type == "Former")
        {
            return FormerQuestions.Count;
        }
        else if(type == "New")
        {
            return NewQuestions.Count;
        }
        else if(type == "Starred")
        {
            return StarredQuestions.Count;
        }
        return 0;
    }
		
	//Closes the error panel
	public void RetryDownload()
	{
		StartCoroutine(DownlaodXML());
	}

	public IEnumerator HybridLoader()
	{
		//Show the splash screen
		SplashScreen.SetActive(true);

		//Hide the retry button
		RetrySplashButton.SetActive(false);

		//Activate the splash screen loading animation and start playing it
		SplashScreenLoading.gameObject.SetActive(true);
		SplashScreenLoading.Play();

		// Start a download of the given URL
		WWW www = new WWW(OnlinePath);

		//Set the timeout to the set time (in seconds)
		int timeout = TimeOut;

		//Check if the download is done before the timeout countdown runs out
		while (!www.isDone && www.error == null)
		{
			//Wait for one second
			yield return new WaitForSeconds(1);

			//Reduce the countdown time by one
			timeout--;

			//If the countdown runs out, dispose the download and show the retry button
			if (timeout <= 0)
			{
				www.Dispose();

                //load the questions from the XML
                string loader = OfflinePath;

                if (FreeVersion)
                {
                    loader = OfflinePathFree;
                }
                TKContainer ic = TKContainer.Load(loader);

                //Clear List<QuizApp> contents
                if (GameQuestions.Count > 0)
                {
                    GameQuestions.Clear();
                    AllQuestions.Clear();
                }
					

				//Parse the questions from the XML file to the list 
				foreach (Questions item in ic.Quizes)
                {
                    GameQuestions.Add(item.ID, item);
                    AllQuestions.Add(item.ID);
                }

				OnlineMode = false;

				ShowMenu();

				yield break;
			}
		}

		//If the download is successful, start parsing the questions
		if (www.error == null && www.isDone)
		{

			//Send the file to the parser.
			StringReader reader = new StringReader(www.text);
			TKContainer Qac = TKContainer.DownloadedXML(reader);

			//Clear List<QuizApp> contents
			if (GameQuestions.Count > 0)
            {
                GameQuestions.Clear();
                AllQuestions.Clear();
            }
				

			//Copy the questions from the XML file to the List<>
			foreach (Questions item in Qac.Quizes)
            {
                GameQuestions.Add(item.ID, item);
                AllQuestions.Add(item.ID);
            }

			OnlineMode = true;

			//Show the main menu
			ShowMenu();
		}
		else
		{
			www.Dispose();

            //load the questions from the XML
            string loader = OfflinePath;

            if (FreeVersion)
            {
                loader = OfflinePathFree;
            }
            TKContainer ic = TKContainer.Load(loader);

            //Clear List<QuizApp> contents
            if (GameQuestions.Count > 0)
            {
                GameQuestions.Clear();
                AllQuestions.Clear();
            }
				

			//Parse the questions from the XML file to the list 
			foreach (Questions item in ic.Quizes)
            {
                GameQuestions.Add(item.ID, item);
                AllQuestions.Add(item.ID);
            }

			OnlineMode = false;

			ShowMenu();
		}
	}

	private IEnumerator CacheImages()
	{
		//Initialization
		WWW CacheWWW;

		CacheFailed = false;

		if (ImageLinks.Count <= 0 || IsGameOver)
			yield break;

		var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ImageLinks[0]);

		string EncodedString = System.Convert.ToBase64String(plainTextBytes);

		string[] temp = EncodedString.Split(RemoveChar, StringSplitOptions.RemoveEmptyEntries);

		EncodedString = string.Join("", temp);

		string filePath = Application.temporaryCachePath + "/" + EncodedString;

		if (filePath.Length > 150)
			filePath = filePath.Substring(0, 150);

		//Check if the image already exists
		if (!System.IO.File.Exists(filePath))
		{
			//Initialize a new bool and set it to false. We will use this to retry a download in case it fails on the first try
			int retried = 0;

			//We will use this if we need to retry a download in case it fails on the first try
			TryAgain:

			//Start a new download from the provided URL
			CacheWWW = new WWW(ImageLinks[0]);

			//Reset the timeout amount to the default
			int timeout = 10;

			//Check if the download is done before the timeout countdown runs out
			while (!CacheWWW.isDone && CacheWWW.error == null)
			{
				//Wait for a second
				yield return new WaitForSeconds(1);

				//Reduce the timeout time by one
				timeout--;

				//If the countdown runs out, dispose the download and show the retry button
				if (timeout <= 0)
				{
					Debug.Log("Caching Timed Out");
					break;
				}
			}

			//Check if the downloaded successfully
			if (CacheWWW.error == null && CacheWWW.isDone)
			{
				if (CacheWWW.texture.width == 8 && CacheWWW.texture.height == 8)
				{
					Debug.LogWarning("Unsupported image was downloaded. Deleting file...");
					File.Delete(filePath);

					FaultyLinks.Add(ImageLinks[0]);

					if (ImageLinks.Count > 0)
					{
						ImageLinks.RemoveAt(0);
						StartCoroutine(CacheImages());
					}
				}
				else
				{
					File.WriteAllBytes(filePath, CacheWWW.bytes);
					ImageLinks.RemoveAt(0);
					StartCoroutine(CacheImages());
				}
			}
			else
			{
				//If the download failed, retry again in 2 seconds if we haven't retried it before
				if (retried < 2)
				{
					yield return new WaitForSeconds(1);
					retried++;
					goto TryAgain;
				}
				else
				{
					Debug.Log("Caching stopped due to error.");
					CacheWWW.Dispose();
					CacheFailed = true;
				}
			}
		}
		else
		{
			ImageLinks.RemoveAt(0);
			StartCoroutine(CacheImages());
		}
	}

	

	//Shuffles the answer list
	private void ShuffleAnswers()
	{
        int numOfAnswers = AnswersAvailable.Count;

        if (GameQuestions[TestQuestions[CurrentQuestion]].NOTA == 1)
        {
            numOfAnswers = numOfAnswers - 1;
        }
        // Go through all the answers and shuffle them
        for (int index = 0; index < numOfAnswers; index++)
		{
			// Hold the answers in a temporary variable
			AnswerClass tempNumber = AnswersAvailable[index];

			// Choose a random index from the text list
			int randomIndex = UnityEngine.Random.Range(index, numOfAnswers);

			// Assign a random text from the list
			AnswersAvailable[index] = AnswersAvailable[randomIndex];

			// Assign the temporary text to the random answer we chose
			AnswersAvailable[randomIndex] = tempNumber;
		}
    }

	//Receives the player's answer and then checks if it is correct
	public void MultipleChoiceAnswer(int ButtonIndex)
	{
        if(hasAnswered && Test.current.getTestType() == "Tutor")
        {
            return;
        }

        int lastSelected = selectedAnswer;
        AnswerA.transform.parent.gameObject.GetComponent<Image>().color = ButtonColor;
        AnswerA.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = ButtonColor;
        AnswerA.color = NormalTextColor;
        AnswerB.transform.parent.gameObject.GetComponent<Image>().color = ButtonColor;
        AnswerB.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = ButtonColor;
        AnswerB.color = NormalTextColor;
        AnswerC.transform.parent.gameObject.GetComponent<Image>().color = ButtonColor;
        AnswerC.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = ButtonColor;
        AnswerC.color = NormalTextColor;
        AnswerD.transform.parent.gameObject.GetComponent<Image>().color = ButtonColor;
        AnswerD.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = ButtonColor;
        AnswerD.color = NormalTextColor;
        if (AnswerE)
        {
            AnswerE.transform.parent.gameObject.GetComponent<Image>().color = ButtonColor;
            AnswerE.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = ButtonColor;
            AnswerE.color = NormalTextColor;
        }
        if(AnswerF)
        {
            AnswerF.transform.parent.gameObject.GetComponent<Image>().color = ButtonColor;
            AnswerF.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = ButtonColor;
            AnswerF.color = NormalTextColor;
        }
        if(AnswerG)
        {
            AnswerG.transform.parent.gameObject.GetComponent<Image>().color = ButtonColor;
            AnswerG.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = ButtonColor;
            AnswerG.color = NormalTextColor;
        }
        if(AnswerH)
        {
            AnswerH.transform.parent.gameObject.GetComponent<Image>().color = ButtonColor;
            AnswerH.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = ButtonColor;
            AnswerH.color = NormalTextColor;
        }

        if (ButtonIndex == 0)
        {
            AnswerA.transform.parent.gameObject.GetComponent<Image>().color = HighlightedColor;
            AnswerA.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = HighlightedColor;
            AnswerA.color = ButtonColor;
            selectedAnswer = 0;
        }
        else if (ButtonIndex == 1)
        {
            AnswerB.transform.parent.gameObject.GetComponent<Image>().color = HighlightedColor;
            AnswerB.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = HighlightedColor;
            AnswerB.color = ButtonColor;
            selectedAnswer = 1;
        }
        else if (ButtonIndex == 2)
        {
            AnswerC.transform.parent.gameObject.GetComponent<Image>().color = HighlightedColor;
            AnswerC.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = HighlightedColor;
            AnswerC.color = ButtonColor;
            selectedAnswer = 2;
        }
        else if (ButtonIndex == 3)
        {
            AnswerD.transform.parent.gameObject.GetComponent<Image>().color = HighlightedColor;
            AnswerD.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = HighlightedColor;
            AnswerD.color = ButtonColor;
            selectedAnswer = 3;
        }
        else if (ButtonIndex == 4)
        {
            AnswerE.transform.parent.gameObject.GetComponent<Image>().color = HighlightedColor;
            AnswerE.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = HighlightedColor;
            AnswerE.color = ButtonColor;
            selectedAnswer = 4;
        }
        else if (ButtonIndex == 5)
        {
            AnswerF.transform.parent.gameObject.GetComponent<Image>().color = HighlightedColor;
            AnswerF.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = HighlightedColor;
            AnswerF.color = ButtonColor;
            selectedAnswer = 5;
        }
        else if (ButtonIndex == 6)
        {
            AnswerG.transform.parent.gameObject.GetComponent<Image>().color = HighlightedColor;
            AnswerG.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = HighlightedColor;
            AnswerG.color = ButtonColor;
            selectedAnswer = 6;
        }
        else if (ButtonIndex == 7)
        {
            AnswerH.transform.parent.gameObject.GetComponent<Image>().color = HighlightedColor;
            AnswerH.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = HighlightedColor;
            AnswerH.color = ButtonColor;
            selectedAnswer = 7;
        }

        if ("Test" == Test.current.getTestType())
        {
            if (nextButton)
            {
                nextButton.GetComponent<Image>().color = HighlightedColor;
                nextButton.GetComponent<RippleConfig>().normalColor = HighlightedColor;
                nextButton.transform.Find("Text").GetComponent<Text>().color = ButtonColor;
            }

            SubmitAnswer(ButtonIndex, lastSelected);
        }
        else
        {
            if (submitButton)
            {
                submitButton.GetComponent<Image>().color = ButtonColor;
                submitButton.GetComponent<RippleConfig>().normalColor = ButtonColor;
            }
        }

    }

    public void SubmitAnswer(int ButtonIndex, int lastSelected, bool causeEffect = true)
    {

        if (selectedAnswer < 0)
        {
            ShowNoAnswerPopup();
            return;
        }
        Transform correctButton = null;
        hasAnswered = true;
        

        if (causeEffect)
        {
            if (GameQuestions[TestQuestions[CurrentQuestion]].Shuffle == 1)
            {
                for (int i = 0; i < OriginalAnswersAvailable.Count; i++)
                {
                    if (AnswersAvailable[selectedAnswer] == OriginalAnswersAvailable[i])
                    {

                        Test.current.setQuestionAnswer(CurrentQuestion, i);
                    }
                }
            }
            else
            {
                Test.current.setQuestionAnswer(CurrentQuestion, selectedAnswer);
            }
        }

        if (!general.getAnsweredQuestions().Contains(TestQuestions[CurrentQuestion]))
        {
            general.answerQuestion(TestQuestions[CurrentQuestion]);
        }

        //Increment the current question counter if we have more questions
        if (CurrentQuestion > (QuestionCount - 1))
        {
            CurrentQuestion = 0;
        }

        if (lastSelected >= 0)
        {
            if(CorrectAnswer == lastSelected)
            {
                CorrectlyAnswered--;
            }
            else
            {
                Test.current.removeIncorrectQuestion(TestQuestions[CurrentQuestion]);
            }
        }

        if(Test.current.getTestType() == "Tutor")
        {
            AnswerA.color = NormalTextColor;
            AnswerB.color = NormalTextColor;
            AnswerC.color = NormalTextColor;
            AnswerD.color = NormalTextColor;
            if(AnswersCount > 4)
            {
                AnswerE.color = NormalTextColor;
                if(AnswersCount > 5)
                {
                    AnswerF.color = NormalTextColor;
                    if (AnswersCount > 6)
                    {
                        AnswerG.color = NormalTextColor;
                        if (AnswersCount > 7)
                        {
                            AnswerH.color = NormalTextColor;
                        }
                    }
                }
            }
        }

        if (CorrectAnswer == ButtonIndex)
        {
            CorrectlyAnswered++;
            List<int> quest = Test.current.getQuestionList();
            Test.current.addCorrectQuestion(quest[CurrentQuestion]);
            //Check if the audiosource is playing a sound effect, stop it and play new effect. Otherwise just play the effect
            if ("Tutor" == Test.current.getTestType())
            {
                if(causeEffect)
                {
                    if (SoundFX.isPlaying)
                    {
                        SoundFX.Stop();
                        SoundFX.PlayOneShot(CorrectSound);
                    }
                    else
                        SoundFX.PlayOneShot(CorrectSound);
                }

                if (CorrectAnswer == 0)
                {
                    AnswerA.transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                    AnswerA.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = CorrectColor;
                    correctButton = AnswerA.transform.parent;
                }
                else if (CorrectAnswer == 1)
                {
                    AnswerB.transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                    AnswerB.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = CorrectColor;
                    correctButton = AnswerB.transform.parent;
                }
                else if (CorrectAnswer == 2)
                {
                    AnswerC.transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                    AnswerC.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = CorrectColor;
                    correctButton = AnswerC.transform.parent;
                }
                else if (CorrectAnswer == 3)
                {
                    AnswerD.transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                    AnswerD.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = CorrectColor;
                    correctButton = AnswerD.transform.parent;
                }
                else if (CorrectAnswer == 4)
                {
                    AnswerE.transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                    AnswerE.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = CorrectColor;
                    correctButton = AnswerE.transform.parent;
                }
                else if (CorrectAnswer == 5)
                {
                    AnswerF.transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                    AnswerF.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = CorrectColor;
                    correctButton = AnswerF.transform.parent;
                }
                else if (CorrectAnswer == 6)
                {
                    AnswerG.transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                    AnswerG.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = CorrectColor;
                    correctButton = AnswerG.transform.parent;
                }
                else if (CorrectAnswer == 7)
                {
                    AnswerH.transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                    AnswerH.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = CorrectColor;
                    correctButton = AnswerH.transform.parent;
                }
            }
        }
        else
        {
            List<int> quest = Test.current.getQuestionList();
            Test.current.addIncorrectAnswer(quest[CurrentQuestion]);
            //Check if the audiosource is playing a sound effect, stop it and play new effect. Otherwise just play the effect

            if ("Tutor" == Test.current.getTestType())
            {

                if(causeEffect)
                {
                    if (SoundFX.isPlaying)
                    {
                        SoundFX.Stop();
                        SoundFX.PlayOneShot(FailSound);
                    }
                    else
                        SoundFX.PlayOneShot(FailSound);
                

#if (SUPPORTED_PLATFORM || UNITY_EDITOR)
                if (CanVibrate)
                    Handheld.Vibrate();
#endif
                }
                if (ButtonIndex == 0)
                {
                    AnswerA.transform.parent.gameObject.GetComponent<Image>().color = FailColor;
                    AnswerA.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = FailColor;

                }
                else if (ButtonIndex == 1)
                {
                    AnswerB.transform.parent.gameObject.GetComponent<Image>().color = FailColor;
                    AnswerB.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = FailColor;
                }
                else if (ButtonIndex == 2)
                {
                    AnswerC.transform.parent.gameObject.GetComponent<Image>().color = FailColor;
                    AnswerC.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = FailColor;
                }
                else if (ButtonIndex == 3)
                {
                    AnswerD.transform.parent.gameObject.GetComponent<Image>().color = FailColor;
                    AnswerD.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = FailColor;
                }
                else if (ButtonIndex == 4)
                {
                    AnswerE.transform.parent.gameObject.GetComponent<Image>().color = FailColor;
                    AnswerE.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = FailColor;
                }
                else if (ButtonIndex == 5)
                {
                    AnswerF.transform.parent.gameObject.GetComponent<Image>().color = FailColor;
                    AnswerF.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = FailColor;
                }
                else if (ButtonIndex == 6)
                {
                    AnswerG.transform.parent.gameObject.GetComponent<Image>().color = FailColor;
                    AnswerG.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = FailColor;
                }
                else if (ButtonIndex == 7)
                {
                    AnswerH.transform.parent.gameObject.GetComponent<Image>().color = FailColor;
                    AnswerH.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = FailColor;
                }

                if (CorrectAnswer == 0)
                {
                    AnswerA.transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                    AnswerA.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = CorrectColor;
                    correctButton = AnswerA.transform.parent;
                }
                else if (CorrectAnswer == 1)
                {
                    AnswerB.transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                    AnswerB.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = CorrectColor;
                    correctButton = AnswerB.transform.parent;
                }
                else if (CorrectAnswer == 2)
                {
                    AnswerC.transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                    AnswerC.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = CorrectColor;
                    correctButton = AnswerC.transform.parent;
                }
                else if (CorrectAnswer == 3)
                {
                    AnswerD.transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                    AnswerD.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = CorrectColor;
                    correctButton = AnswerD.transform.parent;
                }
                else if (CorrectAnswer == 4)
                {
                    AnswerE.transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                    AnswerE.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = CorrectColor;
                    correctButton = AnswerE.transform.parent;
                }
                else if (CorrectAnswer == 5)
                {
                    AnswerF.transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                    AnswerF.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = CorrectColor;
                    correctButton = AnswerF.transform.parent;
                }
                else if (CorrectAnswer == 6)
                {
                    AnswerG.transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                    AnswerG.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = CorrectColor;
                    correctButton = AnswerG.transform.parent;
                }
                else if (CorrectAnswer == 7)
                {
                    AnswerH.transform.parent.gameObject.GetComponent<Image>().color = CorrectColor;
                    AnswerH.transform.parent.gameObject.GetComponent<RippleConfig>().normalColor = CorrectColor;
                    correctButton = AnswerH.transform.parent;
                }
            }
        }

        if(Test.current.getTestType() == "Tutor")
        {
            Destroy(QuestionController);

            QuestionController = Instantiate(questionControllerPrefab, transform.position, transform.rotation);
            QuestionController.transform.parent = AnswerList.transform;
            QuestionController.transform.localScale = new Vector3(0, 0, 0);
            QuestionController.transform.Find("Next").Find("Button Layer").gameObject.GetComponent<Button>().onClick.AddListener(() => NexQuestion());
            QuestionController.transform.Find("Previous").Find("Button Layer").gameObject.GetComponent<Button>().onClick.AddListener(() => PreviousQuestion());
            nextButton = QuestionController.transform.Find("Next").Find("Button Layer").gameObject;
            previousButton = QuestionController.transform.Find("Previous").Find("Button Layer").gameObject;
            QuestionController.transform.localScale = new Vector3(1, 1, 1);
            if (nextButton)
            {
                if((CurrentQuestion + 1) == QuestionCount)
                {
                    nextButton.transform.Find("Text").GetComponent<Text>().text = "Finish";
                }
                nextButton.GetComponent<Image>().color = HighlightedColor;
                nextButton.GetComponent<RippleConfig>().normalColor = HighlightedColor;
                nextButton.transform.Find("Text").GetComponent<Text>().color = ButtonColor;
            }

            if(previousButton)
            {
                if (CurrentQuestion == 0)
                {
                    previousButton.GetComponent<Image>().color = DisabledColor;
                }
            }
        }

    }

	private IEnumerator HideResultText()
	{
		yield return new WaitForSeconds(0.5f);
		if (ResultDisplay)
			ResultDisplay.gameObject.SetActive(false);
	}

	private void UpdateCounter()
	{

        Test.current.setCurrentQuestion(QuestionsAsked);
        
        SaveLoad.Save();
        GeneralData.Save();

        if ((CurrentQuestion) == QuestionCount)
		{
			AnswerList.GetComponent<CanvasGroup>().blocksRaycasts = false;
            StartCoroutine(GameOver(1));
		}
		else
        {
            general.answerQuestion(GameQuestions[TestQuestions[CurrentQuestion]].ID);
            if(CurrentQuestion == QuestionsAsked)
            {
                QuestionsAsked++;
            }
            StartCoroutine(NextQuestion());
        }
        
    }

    public void PreviousQuestion()
    {
        if(CurrentQuestion == 0)
        {
            return;
        }
        CurrentQuestion--;
        StartCoroutine(NextQuestion());
    }
    public void NexQuestion()
    {
        if(selectedAnswer < 0)
        {
            ShowNoAnswerPopup();
            return;
        }
        CurrentQuestion++;
        UpdateCounter();
    }

    public void starQuestions()
    {
        if(StarredQuestions.Contains(TestQuestions[CurrentQuestion]))
        {
            removeStarQuestion();
        }
        else
        {
            general.starQuestion(TestQuestions[CurrentQuestion]);
            StarButton.sprite = ActiveStar;
            GeneralData.Save();
        }
    }

    public void removeStarQuestion()
    {
        general.removeStarQuestion(TestQuestions[CurrentQuestion]);
        StarButton.sprite = InactiveStar;
        GeneralData.Save();
    }

    private IEnumerator GameOver(int a)
	{
		Playing = false;
		IsGameOver = true;
        Test.current.setCompleteStatus(true);
        SaveLoad.Save();
		GameOverCircle.fillAmount = 0;

		float fill = (float)CorrectlyAnswered / QuestionCount;

		yield return new WaitForSeconds(1.5f);

		#if USE_ADMOB
		if (AdCount <= 1)
		{
			AdmobManager.instance.ShowInterstitial();

			//Reset the ad counter
			AdCount = AdFrequency;
		}
		else
			AdCount--;
		#endif

		int GamePoints = PlayerPrefs.GetInt("Points", 0);

		GameOverResult.text = "FINISHED";
		GameOverTip.text = "Outstanding Job!";

		int Points = Mathf.RoundToInt(fill * 10 * PointsPerAnswer);

		GamePoints = GamePoints + Points;

		PointsText.text = CorrectlyAnswered.ToString() + " / " + QuestionCount + " Questions";

		if (GameCanvas)
			GameCanvas.SetActive(false);
		if (GameOverCanvas)
        {
            GameOverCanvas.SetActive(true);
        }
		if (PauseCanvas)
			PauseCanvas.SetActive(false);
		if (MenuCanvas)
			MenuCanvas.SetActive(false);

		GameOverPercentage.text = (fill * 100).ToString("0") + "%";

		float progress = 0;

		while (progress <= fill)
		{
			GameOverCircle.fillAmount = Mathf.Lerp(GameOverCircle.fillAmount, fill, progress);
			progress += Time.deltaTime * 0.1f;

			yield return new WaitForSeconds(Time.deltaTime * 0.1f);
		}

		GameOverCircle.fillAmount = fill;
	}

    //Diplays the next question is single player mode
    private IEnumerator NextQuestion(float delay = 0)
    {
        selectedAnswer = -1;
        hasAnswered = false;
        OriginalAnswersAvailable.Clear();

        yield return new WaitForSeconds(delay);

        ScrollToTop();

        if (StarredQuestions.Contains(TestQuestions[CurrentQuestion]))
        {
            StarButton.sprite = ActiveStar;
        }
        else
        {
            StarButton.sprite = InactiveStar;
        }

        if (QuizCounter)
        {

            QuizCounter.text = (CurrentQuestion + 1).ToString() + "/" + Test.current.getNumberOfQuestions().ToString();
        }
            if (ResultDisplay)
            {
                ResultDisplay.gameObject.SetActive(false);
            }

        ImageLoadingAnimation.gameObject.SetActive(false);
        ImageErrorPanel.SetActive(false);
        QuestionDisplay.gameObject.SetActive(false);
        AnswerList.gameObject.SetActive(false);
        ImageDisplay.gameObject.SetActive(false);
        Color ImageColor = ImageDisplay.color;
        ImageColor.a = 0;
        ImageDisplay.color = ImageColor;
        
        if (!OnlineMode && GameQuestions[TestQuestions[CurrentQuestion]].Image.Length > 1)
        {
            hasImage = true;
        }
        else
        {
            hasImage = false;
            //ImageDisplay.sprite = null;
            //ImageDisplaySixAnswers.sprite = null;
        }

        //Stop and hide the loading animation
        ImageLoadingAnimation.Stop();
        ImageLoadingAnimation.gameObject.SetActive(false);

        //Clear List<Answers> contents
        if (AnswersAvailable.Count > 0)
        {
            AnswersAvailable.Clear();
        }

        Questions currentQuestion = GameQuestions[TestQuestions[CurrentQuestion]];

        foreach (Transform child in AnswerList.transform)
        {
            Destroy(child.gameObject);
        }

        if (currentQuestion.Answer.Length > 0)
		{
			for (int q = 0; q < currentQuestion.Answer.Length; q++)
            {
                if (currentQuestion.Answer[q].Choices != null)
                {
                    AnswersAvailable.Add(currentQuestion.Answer[q]);
                }
            }

            AnswersCount = AnswersAvailable.Count;

            Transform[] AnswersArr = new Transform[] { A, B, C, D, E, F, G, H };
            for (int i = 0; i < AnswersCount; i++)
            {
                GameObject answerItem = Instantiate(answerPrefab, transform.position, transform.rotation);
                answerItem.transform.parent = AnswerList.transform;
                answerItem.transform.localScale = new Vector3(0, 0, 0);
                AnswersArr[i] = answerItem.transform;
                if (i == 0)
                    A = answerItem.transform.Find("Button Layer");
                if (i == 1)
                    B = answerItem.transform.Find("Button Layer");
                if (i == 2)
                    C = answerItem.transform.Find("Button Layer");
                if (i == 3)
                    D = answerItem.transform.Find("Button Layer");
                if (i == 4)
                    E = answerItem.transform.Find("Button Layer");
                if (i == 5)
                    F = answerItem.transform.Find("Button Layer");
                if (i == 6)
                    G = answerItem.transform.Find("Button Layer");
                if (i == 7)
                    H = answerItem.transform.Find("Button Layer");
            }

            UpdateLayout();
            
            if (quizMode == "Tutor")
            {
                QuestionController = Instantiate(questionControllerSubmitPrefab, transform.position, transform.rotation);
                QuestionController.transform.parent = AnswerList.transform;
                QuestionController.transform.localScale = new Vector3(0, 0, 0);
                QuestionController.transform.Find("Submit").Find("Button Layer").GetComponent<Button>().onClick.AddListener(() => SubmitAnswer(selectedAnswer, -1));
                previousButton = QuestionController.transform.Find("Previous").Find("Button Layer").gameObject;
                QuestionController.transform.Find("Previous").Find("Button Layer").gameObject.GetComponent<Button>().onClick.AddListener(() => PreviousQuestion());
                submitButton = QuestionController.transform.Find("Submit").Find("Button Layer").gameObject;
                
                if(CurrentQuestion == 0)
                {
                    previousButton.GetComponent<Image>().color = DisabledColor;
                }
            }
            else
            {
                QuestionController = Instantiate(questionControllerPrefab, transform.position, transform.rotation);
                QuestionController.transform.parent = AnswerList.transform;
                QuestionController.transform.localScale = new Vector3(0, 0, 0);
                QuestionController.transform.Find("Next").Find("Button Layer").gameObject.GetComponent<Button>().onClick.AddListener(() => NexQuestion());
                QuestionController.transform.Find("Previous").Find("Button Layer").gameObject.GetComponent<Button>().onClick.AddListener(() => PreviousQuestion());
                nextButton = QuestionController.transform.Find("Next").Find("Button Layer").gameObject;
                previousButton = QuestionController.transform.Find("Previous").Find("Button Layer").gameObject;

                if ((CurrentQuestion + 1) == QuestionCount)
                {
                    nextButton.transform.Find("Text").GetComponent<Text>().text = "Finish";
                }
            }

            selectedAnswer = -1;
            

            AnswerA = A.GetChild(0).GetComponent<Text>();
            A.gameObject.GetComponent<Button>().onClick.AddListener(() => MultipleChoiceAnswer(0));
            AnswerB = B.GetChild(0).GetComponent<Text>();
            B.gameObject.GetComponent<Button>().onClick.AddListener(() => MultipleChoiceAnswer(1));
            AnswerC = C.GetChild(0).GetComponent<Text>();
            C.gameObject.GetComponent<Button>().onClick.AddListener(() => MultipleChoiceAnswer(2));
            AnswerD = D.GetChild(0).GetComponent<Text>();
            D.gameObject.GetComponent<Button>().onClick.AddListener(() => MultipleChoiceAnswer(3));
            if (AnswersCount > 4)
            {
                AnswerE = E.GetChild(0).GetComponent<Text>();
                E.gameObject.GetComponent<Button>().onClick.AddListener(() => MultipleChoiceAnswer(4));
                if (AnswersCount > 5)
                {
                    AnswerF = F.GetChild(0).GetComponent<Text>();
                    F.gameObject.GetComponent<Button>().onClick.AddListener(() => MultipleChoiceAnswer(5));
                    if (AnswersCount > 6)
                    {
                        AnswerG = G.GetChild(0).GetComponent<Text>();
                        G.gameObject.GetComponent<Button>().onClick.AddListener(() => MultipleChoiceAnswer(6));
                        if (AnswersCount > 7)
                        {
                            AnswerH = H.GetChild(0).GetComponent<Text>();
                            H.gameObject.GetComponent<Button>().onClick.AddListener(() => MultipleChoiceAnswer(7));
                        }
                    }
                }
            }

            //Adapt the gameUI based on whether the current question is a text only question or text+picture question

            QuestionDisplay.gameObject.SetActive(true);
            QuestionDisplay.text = GameQuestions[TestQuestions[CurrentQuestion]].Question;
            AnswerList.SetActive(true);

            if (hasImage)
            {
                ImageDisplay.gameObject.SetActive(true);
                ImageDisplay.DOFade(1, 1f);
            }

            //Shuffle the answers
            if(GameQuestions[TestQuestions[CurrentQuestion]].Shuffle == 1)
            {
                OriginalAnswersAvailable.Insert(0, AnswersAvailable[0]);
                OriginalAnswersAvailable.Insert(1, AnswersAvailable[1]);
                OriginalAnswersAvailable.Insert(2, AnswersAvailable[2]);
                OriginalAnswersAvailable.Insert(3, AnswersAvailable[3]);
                if (AnswersCount > 4)
                {
                    OriginalAnswersAvailable.Insert(4, AnswersAvailable[4]);
                    if (AnswersCount > 5)
                    {
                        OriginalAnswersAvailable.Insert(5, AnswersAvailable[5]);
                        if (AnswersCount > 6)
                        {
                            OriginalAnswersAvailable.Insert(6, AnswersAvailable[6]);
                            if (AnswersCount > 7)
                            {
                                OriginalAnswersAvailable.Insert(7, AnswersAvailable[7]);
                            }
                        }
                    }
                }
                ShuffleAnswers();
            }
            

            AnswerA.text = AnswersAvailable[0].Choices;
			AnswerB.text = AnswersAvailable[1].Choices;
			AnswerC.text = AnswersAvailable[2].Choices;
			AnswerD.text = AnswersAvailable[3].Choices;

            if (AnswersCount >= 5)
            {
                AnswerE.text = AnswersAvailable[4].Choices;
                if (AnswersCount >= 6)
                {
                    AnswerF.text = AnswersAvailable[5].Choices;
                    if (AnswersCount >= 7)
                    {
                        AnswerG.text = AnswersAvailable[6].Choices;
                        if (AnswersCount == 8)
                        {
                            AnswerH.text = AnswersAvailable[7].Choices;
                        }
                    }
                }
            }
            

            for (int q = 0; q < AnswersAvailable.Count; q++)
			{
				if (AnswersAvailable[q].Correct)
				{
					CorrectAnswer = q;
					break;
				}
			}
            
            if (Test.current.checkQuestionAnswer(CurrentQuestion))
            {

                int selected = Test.current.getQuestionAnswer(CurrentQuestion);

                if (GameQuestions[TestQuestions[CurrentQuestion]].Shuffle == 1)
                {
                    for(int i=0; i < AnswersAvailable.Count; i++)
                    {
                        if(OriginalAnswersAvailable[selected].Choices == AnswersAvailable[i].Choices)
                        {
                            selected = i;
                            i = AnswersAvailable.Count;
                        }
                    }
                }
                
                if (selected >= 0)
                {
                    if(Test.current.getTestType() == "Test")
                    {
                        MultipleChoiceAnswer(selected);
                    }
                    else
                    {
                        selectedAnswer = selected;
                        SubmitAnswer(selected, -1, false);
                    }
                }
            }

            AnswersArr[0].DOScale(new Vector3(1, 1, 1), 0.25f);
            yield return new WaitForSeconds(0.1f);
            AnswersArr[1].DOScale(new Vector3(1, 1, 1), 0.25f);
            yield return new WaitForSeconds(0.1f);
            AnswersArr[2].DOScale(new Vector3(1, 1, 1), 0.25f);
            yield return new WaitForSeconds(0.1f);
            AnswersArr[3].DOScale(new Vector3(1, 1, 1), 0.25f);

            if (AnswersCount > 4)
            {
                yield return new WaitForSeconds(0.1f);
                AnswersArr[4].DOScale(new Vector3(1, 1, 1), 0.25f);
                if (AnswersCount > 5)
                {
                    yield return new WaitForSeconds(0.1f);
                    AnswersArr[5].DOScale(new Vector3(1, 1, 1), 0.25f);
                    if (AnswersCount > 6)
                    {
                        yield return new WaitForSeconds(0.1f);
                        AnswersArr[6].DOScale(new Vector3(1, 1, 1), 0.25f);
                        if (AnswersCount > 7)
                        {
                            yield return new WaitForSeconds(0.1f);
                            AnswersArr[7].DOScale(new Vector3(1, 1, 1), 0.25f);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(0.1f);
            QuestionController.transform.DOScale(new Vector3(1, 1, 1), 0.25f);
            AnswerList.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        //Stop all playing sounds
        if (SoundFX.isPlaying)
			SoundFX.Stop();
	}

	//Call this to redirect the user to a link
	public void URL(string link)
	{
		Application.OpenURL(link);
	}
#endif
}

static class Shuffle
{
    private static System.Random rng = new System.Random();

    public static void ShuffleArray<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

class ApplicationChrome
{

    /**
     * Manipulates the system application chrome to change the way the status bar and navigation bar work
     *
     * References:
     * . http://developer.android.com/reference/android/view/View.html#setSystemUiVisibility(int)
     * . http://forum.unity3d.com/threads/calling-setsystemuivisibility.139445/#post-952946
     * . http://developer.android.com/reference/android/view/WindowManager.LayoutParams.html#FLAG_LAYOUT_IN_SCREEN
     **/

    // Enums
    public enum States
    {
        Unknown,
        Visible,
        VisibleOverContent,
        TranslucentOverContent,
        Hidden
    }

    // Constants
    private const uint DEFAULT_BACKGROUND_COLOR = 0xff000000;

#if USE_ANDROID
        // Original Android flags
        private const int VIEW_SYSTEM_UI_FLAG_VISIBLE = 0;					// Added in API 14 (Android 4.0.x): Status bar visible (the default)
        private const int VIEW_SYSTEM_UI_FLAG_LOW_PROFILE = 1;				// Added in API 14 (Android 4.0.x): Low profile for games, book readers, and video players; the status bar and/or navigation icons are dimmed out (if visible)
        private const int VIEW_SYSTEM_UI_FLAG_HIDE_NAVIGATION = 2;			// Added in API 14 (Android 4.0.x): Hides all navigation. Cleared when theres any user interaction.
        private const int VIEW_SYSTEM_UI_FLAG_FULLSCREEN = 4;				// Added in API 16 (Android 4.1.x): Hides status bar. Does nothing in Unity (already hidden if "status bar hidden" is checked)
        private const int VIEW_SYSTEM_UI_FLAG_LAYOUT_STABLE = 256;			// Added in API 16 (Android 4.1.x): ?
        private const int VIEW_SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION = 512;	// Added in API 16 (Android 4.1.x): like HIDE_NAVIGATION, but for layouts? it causes the layout to be drawn like that, even if the whole view isn't (to avoid artifacts in animation)
        private const int VIEW_SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN = 1024;		// Added in API 16 (Android 4.1.x): like FULLSCREEN, but for layouts? it causes the layout to be drawn like that, even if the whole view isn't (to avoid artifacts in animation)
        private const int VIEW_SYSTEM_UI_FLAG_IMMERSIVE = 2048;				// Added in API 19 (Android 4.4): like HIDE_NAVIGATION, but interactive (it's a modifier for HIDE_NAVIGATION, needs to be used with it)
        private const int VIEW_SYSTEM_UI_FLAG_IMMERSIVE_STICKY = 4096;		// Added in API 19 (Android 4.4): tells that HIDE_NAVIGATION and FULSCREEN are interactive (also just a modifier)

        private static int WINDOW_FLAG_FULLSCREEN = 0x00000400;
        private static int WINDOW_FLAG_FORCE_NOT_FULLSCREEN = 0x00000800;
        private static int WINDOW_FLAG_LAYOUT_IN_SCREEN = 0x00000100;
        private static int WINDOW_FLAG_TRANSLUCENT_STATUS = 0x04000000;
        private static int WINDOW_FLAG_TRANSLUCENT_NAVIGATION = 0x08000000;
        private static int WINDOW_FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS = -2147483648; // 0x80000000; // Added in API 21 (Android 5.0): tells the Window is responsible for drawing the background for the system bars. If set, the system bars are drawn with a transparent background and the corresponding areas in this window are filled with the colors specified in getStatusBarColor() and getNavigationBarColor()

        // Current values
        private static int systemUiVisibilityValue;
        private static int flagsValue;
#endif

    // Properties
    private static States _statusBarState;
    private static States _navigationBarState;

    private static uint _statusBarColor = DEFAULT_BACKGROUND_COLOR;
    private static uint _navigationBarColor = DEFAULT_BACKGROUND_COLOR;

    private static bool _isStatusBarTranslucent; // Just so we know whether its translucent when hidden or not
    private static bool _isNavigationBarTranslucent;

    private static bool _dimmed;


    // ================================================================================================================
    // INTERNAL INTERFACE ---------------------------------------------------------------------------------------------

    static ApplicationChrome()
    {
        applyUIStates();
        applyUIColors();
    }

    private static void applyUIStates()
    {
#if USE_ANDROID
            applyUIStatesAndroid();
#endif
    }

    private static void applyUIColors()
    {
#if USE_ANDROID
            applyUIColorsAndroid();
#endif
    }

#if USE_ANDROID

        private static void applyUIStatesAndroid() {
            int newFlagsValue = 0;
            int newSystemUiVisibilityValue = 0;

            // Apply dim values
            if (_dimmed) newSystemUiVisibilityValue |= VIEW_SYSTEM_UI_FLAG_LOW_PROFILE;

            // Apply color values
            if (_navigationBarColor != DEFAULT_BACKGROUND_COLOR || _statusBarColor != DEFAULT_BACKGROUND_COLOR) newFlagsValue |= WINDOW_FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS;

            // Apply status bar values
            switch (_statusBarState) {
                case States.Visible:
                    _isStatusBarTranslucent = false;
                    newFlagsValue |= WINDOW_FLAG_FORCE_NOT_FULLSCREEN;
                    break;
                case States.VisibleOverContent:
                    _isStatusBarTranslucent = false;
                    newFlagsValue |= WINDOW_FLAG_FORCE_NOT_FULLSCREEN | WINDOW_FLAG_LAYOUT_IN_SCREEN;
                    newSystemUiVisibilityValue |= VIEW_SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN;
                    break;
                case States.TranslucentOverContent:
                    _isStatusBarTranslucent = true;
                    newFlagsValue |= WINDOW_FLAG_FORCE_NOT_FULLSCREEN | WINDOW_FLAG_LAYOUT_IN_SCREEN | WINDOW_FLAG_TRANSLUCENT_STATUS;
                    newSystemUiVisibilityValue |= VIEW_SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN;
                    break;
                case States.Hidden:
                    newFlagsValue |= WINDOW_FLAG_FULLSCREEN | WINDOW_FLAG_LAYOUT_IN_SCREEN;
                    if (_isStatusBarTranslucent) newFlagsValue |= WINDOW_FLAG_TRANSLUCENT_STATUS;
                    break;
            }

            // Applies navigation values
            switch (_navigationBarState) {
                case States.Visible:
                    _isNavigationBarTranslucent = false;
                    newSystemUiVisibilityValue |= VIEW_SYSTEM_UI_FLAG_LAYOUT_STABLE;
                    break;
                case States.VisibleOverContent:
                    // TODO: Side effect: forces status bar over content if set to VISIBLE
                    _isNavigationBarTranslucent = false;
                    newSystemUiVisibilityValue |= VIEW_SYSTEM_UI_FLAG_LAYOUT_STABLE | VIEW_SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION;
                    break;
                case States.TranslucentOverContent:
                    // TODO: Side effect: forces status bar over content if set to VISIBLE
                    _isNavigationBarTranslucent = true;
                    newFlagsValue |= WINDOW_FLAG_TRANSLUCENT_NAVIGATION;
                    newSystemUiVisibilityValue |= VIEW_SYSTEM_UI_FLAG_LAYOUT_STABLE | VIEW_SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION;
                    break;
                case States.Hidden:
                    newSystemUiVisibilityValue |= VIEW_SYSTEM_UI_FLAG_FULLSCREEN | VIEW_SYSTEM_UI_FLAG_HIDE_NAVIGATION | VIEW_SYSTEM_UI_FLAG_IMMERSIVE_STICKY;
                    if (_isNavigationBarTranslucent) newFlagsValue |= WINDOW_FLAG_TRANSLUCENT_NAVIGATION;
                    break;
            }

            if (Screen.fullScreen) Screen.fullScreen = false;

            // Applies everything natively
            setFlags(newFlagsValue);
            setSystemUiVisibility(newSystemUiVisibilityValue);
        }

        private static void runOnAndroidUiThread(Action target) {
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                using (var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
                    activity.Call("runOnUiThread", new AndroidJavaRunnable(target));
                }
            }
        }

        private static void setSystemUiVisibility(int value) {
            if (systemUiVisibilityValue != value) {
                systemUiVisibilityValue = value;
                runOnAndroidUiThread(setSystemUiVisibilityInThread);
            }
        }

        private static void setSystemUiVisibilityInThread() {
            //Debug.Log("SYSTEM FLAGS: " + systemUiVisibilityValue);
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                using (var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
                    using (var window = activity.Call<AndroidJavaObject>("getWindow")) {
                        using (var view = window.Call<AndroidJavaObject>("getDecorView")) {
                            view.Call("setSystemUiVisibility", systemUiVisibilityValue);
                        }
                    }
                }
            }
        }

        private static void setFlags(int value) {
            if (flagsValue != value) {
                flagsValue = value;
                runOnAndroidUiThread(setFlagsInThread);
            }
        }

        private static void setFlagsInThread() {
            //Debug.Log("FLAGS: " + flagsValue);
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                using (var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
                    using (var window = activity.Call<AndroidJavaObject>("getWindow")) {
                        window.Call("setFlags", flagsValue, -1); // (int)0x7FFFFFFF
                    }
                }
            }
        }

        private static void applyUIColorsAndroid() {
            runOnAndroidUiThread(applyUIColorsAndroidInThread);
        }

        private static void applyUIColorsAndroidInThread() {
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                using (var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
                    using (var window = activity.Call<AndroidJavaObject>("getWindow")) {
                        //Debug.Log("Colors SET: " + _statusBarColor);
                        window.Call("setStatusBarColor", unchecked((int)_statusBarColor));
                        window.Call("setNavigationBarColor", unchecked((int)_navigationBarColor));
                    }
                }
            }
        }

#endif

    // ================================================================================================================
    // ACCESSOR INTERFACE ---------------------------------------------------------------------------------------------

    public static States navigationBarState
    {
        get { return _navigationBarState; }
        set
        {
            if (_navigationBarState != value)
            {
                _navigationBarState = value;
                applyUIStates();
            }
        }
    }

    public static States statusBarState
    {
        get { return _statusBarState; }
        set
        {
            if (_statusBarState != value)
            {
                _statusBarState = value;
                applyUIStates();
            }
        }
    }

    public static bool dimmed
    {
        get { return _dimmed; }
        set
        {
            if (_dimmed != value)
            {
                _dimmed = value;
                applyUIStates();
            }
        }
    }

    public static uint statusBarColor
    {
        get { return _statusBarColor; }
        set
        {
            if (_statusBarColor != value)
            {
                _statusBarColor = value;
                applyUIColors();
                applyUIStates();
            }
        }
    }

    public static uint navigationBarColor
    {
        get { return _navigationBarColor; }
        set
        {
            if (_navigationBarColor != value)
            {
                _navigationBarColor = value;
                applyUIColors();
                applyUIStates();
            }
        }
    }
}