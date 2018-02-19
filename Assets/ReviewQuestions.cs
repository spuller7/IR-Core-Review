using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if USE_DOTWEEN
using DG.Tweening;
#endif

public class ReviewQuestions : MonoBehaviour {

    public GameObject questionReviewList;

    public GameObject correctAnswerContainer;
    public GameObject incorrectAnswerContainer;
    public GameObject answerContainer;
    public Sprite starredImage;
    public Sprite notStarred;
    Dictionary<int, Questions> questions = new Dictionary<int, Questions>();
    private List<GameObject> questionContainers = new List<GameObject>();


    public void loadQuestions()
    {

        List<int> starred = GameObject.Find("Controller").GetComponent<MainController>().StarredQuestions;
        questions.Clear();
        questions = GameObject.Find("Controller").GetComponent<MainController>().GameQuestions;
        int num = 1;
        foreach (KeyValuePair<int, Questions> question in questions)
        {
           
            GameObject go = Instantiate(answerContainer);
            QuestionReview qr = go.GetComponent<QuestionReview>();
            for (int q = 0; q < question.Value.Answer.Length; q++)
            {
                if (question.Value.Answer[q].Choices != null)
                {
                    if(question.Value.Answer[q].Correct)
                    {
                        qr.answerText.text = question.Value.Answer[q].Choices;
                    }
                }
            }
            qr.questionText.text = question.Value.Question;
            qr.questionNumberText.text = num.ToString();
            if (starred.Contains(question.Value.ID))
            {
                qr.starred.sprite = starredImage;
            }
            else
            {
                qr.starred.sprite = notStarred;
            }
            qr.id = question.Value.ID;
            go.transform.parent = questionReviewList.transform;
            go.transform.localScale = new Vector3(0, 0, 0);
            num++;

            questionContainers.Add(go);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void displayTheQuestions()
    {
        StartCoroutine(displayQuestion());
    }

    IEnumerator displayQuestion()
    {
        foreach (GameObject go in questionContainers)
        {
            go.transform.DOScale(new Vector3(1, 1, 1), 0.25f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
