using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionReview : MonoBehaviour {

    public Text answerText;
    public Text questionNumberText;
    public Text questionText;
    public int id;
    public Test test;
    public Image starred;
    public Image StarButton;
    public Sprite ActiveStar;
    public Sprite InactiveStar;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onStarred()
    {
        MainController mc = GameObject.Find("Controller").GetComponent<MainController>();
        if (mc.general.getStarredQuestions().Contains(id))
        {
            mc.general.removeStarQuestion(id);
            StarButton.sprite = InactiveStar;
            GeneralData.Save();
        }
        else
        {
            mc.general.starQuestion(id);
            StarButton.sprite = ActiveStar;
            GeneralData.Save();
        }
    }

}
