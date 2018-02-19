using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestIcon : MonoBehaviour {

    public Text date;
    public Text testName;
    public Test test;

    public void setDate(string date)
    {
        this.date.text = date;
    }
    public void setTestName(string testName)
    {
        this.testName.text = testName;
    }
    public void setTest(Test test)
    {
        this.test = test;
    }
    public Test getTest()
    {
        return test;
    }
    public void onTestSelect()
    {
        Test.current = test;
        GameObject.Find("Menu Canvas").GetComponent<MainMenuHandler>().onTestSelect();
    }
}
