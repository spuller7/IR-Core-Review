using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour {

    public int numOfQuestions;
    public MainController mc;
    public Text numberOfQuestionsPlaceHolder;
    public Text numberOfQuestionsInput;
    public InputField textNumOfQues;
    public MaterialUI.RadioButtons questionTypeRadio;
    public MaterialUI.RadioButtons quizModeRadio;

    public GameObject completeTest;
    public GameObject incompleteTest;

    public GameObject testList;

    public List<Test> savedTests;

    public GameObject resumeTestPanel;
    public GameObject reviewTestPanel;

    public void displayPreviousTests()
    {
        foreach (Transform child in testList.transform)
        {
            if(child.gameObject != testList.transform.GetChild(0).gameObject)
            {
                Destroy(child.gameObject);
            }
            
        }
        int num = 1;
        foreach (Test test in SaveLoad.savedTests)
        {
            if(num > 8)
            {
                return;
            }

            Test.current = test;
            if(test.isComplete())
            {
                GameObject testObject = Instantiate(completeTest);
                TestIcon icon = testObject.GetComponent<TestIcon>();
                icon.setDate(test.getTestDate());
                icon.setTestName(test.getTime());
                icon.setTest(test);
                testObject.transform.parent = testList.transform;
                testObject.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                GameObject testObject = Instantiate(incompleteTest);
                TestIcon icon = testObject.GetComponent<TestIcon>();
                icon.setDate(test.getTestDate());
                icon.setTestName(test.getTime());
                icon.setTest(test);
                testObject.transform.parent = testList.transform;
                testObject.transform.localScale = new Vector3(1, 1, 1);
            }
            num++;
        }
        if(savedTests.Count < 2)
        {
            GameObject go1 = new GameObject("nullFiller", typeof(RectTransform));
            GameObject go2 = new GameObject("nullFiller", typeof(RectTransform));
            go1.transform.parent = testList.transform;
            go2.transform.parent = testList.transform;
        }
    }

    public void changeLimit(string type)
    {
        numOfQuestions = mc.getTotal(type);
        textNumOfQues.text = string.Empty;
        numberOfQuestionsPlaceHolder.text = numOfQuestions.ToString();
    }
    public int getNumberOfQuestions()
    {
        if(numberOfQuestionsInput.text == "")
        {
            return int.Parse(numberOfQuestionsPlaceHolder.text);
        }
        return int.Parse(numberOfQuestionsInput.text);
    }

    public string getQuizMode()
    {
        return quizModeRadio.getSelected("Quiz Mode");
    }

    public string getQuestionType()
    {
        return questionTypeRadio.getSelected("Question Types");
    }

    public void onTestSelect()
    {

        if (Test.current.isComplete())
        {
            reviewTestPanel.SetActive(true);
        }
        else
        {
            resumeTestPanel.SetActive(true);
        }
    }

    public void cancelReview()
    {
        reviewTestPanel.SetActive(false);
    }

    public void cancelResume()
    {
        resumeTestPanel.SetActive(false);
    }
}
