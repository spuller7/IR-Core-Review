using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if USE_DOTWEEN
using DG.Tweening;
#endif

public class ReviewTestQuestions : MonoBehaviour
{

    public GameObject questionReviewList;

    public GameObject correctAnswerContainer;
    public GameObject incorrectAnswerContainer;
    public GameObject answerContainer;
    public List<int> testQuestions;
    public List<int> correctAnswers;
    public List<int> incorrectAnswers;
    public Sprite starredImage;
    public Sprite notStarred;
    private List<GameObject> questionContainers = new List<GameObject>();
    Dictionary<int, Questions> questions = new Dictionary<int, Questions>();

    void Start()
    {
        
    }

    public void LoadQuestions(bool star = false)
    {
            foreach (Transform child in questionReviewList.transform)
            {
                Destroy(child.gameObject);
            }

        questionContainers.Clear();
        questions = GameObject.Find("Controller").GetComponent<MainController>().GameQuestions;
        testQuestions.Clear();
        correctAnswers.Clear();
        incorrectAnswers.Clear();
        List<int> starred = GameObject.Find("Controller").GetComponent<MainController>().StarredQuestions;
        if(star)
        {
            testQuestions = starred;
            this.gameObject.transform.Find("TopBar").Find("Text").GetComponent<Text>().text = "Starred Questions";
        }
        else
        {
            correctAnswers = Test.current.getCorrectQuestions();
            incorrectAnswers = Test.current.getIncorrectQuestions();
            testQuestions = Test.current.getQuestionList();
            this.gameObject.transform.Find("TopBar").Find("Text").GetComponent<Text>().text = "Test " + Test.current.testID;
        }
        
        int num = 1;
        foreach (int questioon in testQuestions)
        {
            Questions question = questions[questioon];
            GameObject go;
            if (Test.current.getCorrectQuestions().Contains(question.ID))
            {
                go = Instantiate(correctAnswerContainer);
                
            }
            else if(incorrectAnswers.Contains(question.ID))
            {
                go = Instantiate(incorrectAnswerContainer);
            }
            else
            {
                go = Instantiate(answerContainer);
            }

            QuestionReview qr = go.GetComponent<QuestionReview>();

            if (starred.Contains(question.ID))
            {
                qr.starred.sprite = starredImage;
            }
            else
            {
                qr.starred.sprite = notStarred;
            }

            for (int q = 0; q < question.Answer.Length; q++)
            {
                if (question.Answer[q].Choices != null)
                {
                    if (question.Answer[q].Correct)
                    {
                        qr.answerText.text = question.Answer[q].Choices;
                    }
                }
            }
            qr.questionText.text = question.Question;
            qr.questionNumberText.text = num.ToString();
            qr.id = question.ID;
            go.transform.parent = questionReviewList.transform;
            go.transform.localScale = new Vector3(0, 0, 0);
            num++;

            questionContainers.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void displayTheQuestions()
    {
        StartCoroutine(displayQuestion());
    }

    IEnumerator displayQuestion()
    {
        foreach(GameObject go in questionContainers)
        {
            go.transform.DOScale(new Vector3(1, 1, 1), 0.25f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
