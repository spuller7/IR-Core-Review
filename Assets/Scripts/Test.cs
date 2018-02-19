using System.Collections.Generic;

[System.Serializable]
public class Test {

    public static Test current;

    public int numOfQuestions;
    public bool complete;
    public int currentQuestion = 1;
    public string testMode;
    public string questionMode;
    public string timestamp;
    public int testID;
    public string testDate;

    public List<int> questionList;
    public List<int> correctQuestions;
    public List<int> incorrectQuestions;
    public List<int> questionAnswers;

    public Test()
    {
        numOfQuestions = 0;
        complete = false;
        currentQuestion = 1;
        testMode = "";
        questionMode = "";
        testID = 0;
        testDate = getCurrentDate();
        questionList = new List<int>();
        correctQuestions = new List<int>();
        incorrectQuestions = new List<int>();
        questionAnswers = new List<int>();
        timestamp = System.DateTime.Now.ToShortTimeString();

    }

    public string getTestDate()
    {
        return testDate;
    }
    public void setTestDate()
    {
        testDate = getCurrentDate();
    }
    public int getTestID()
    {
        return testID;
    }
    public void setTestID()
    {
        testID = GeneralData.general.getTotalTest() + 1;
        GeneralData.general.setTotalTests(testID);
    }
    public int getNumberOfQuestions()
    {
        return numOfQuestions;
    }
    public void setNumberOfQuestions(int num)
    {
        numOfQuestions = num;
    }
    public int getCurrentQuestion()
    {
        return currentQuestion;
    }
    public void setCurrentQuestion(int num)
    {
        currentQuestion = num;   
    }
    public bool isComplete()
    {
        return complete;
    }
    public void setCompleteStatus(bool status)
    {
        complete = status;
    }
    public string getQuestionType()
    {
        return questionMode;
    }
    public void setQuestionType(string type)
    {
        questionMode = type;
    }
    public string getTestType()
    {
        return testMode;
    }
    public void setTestType(string type)
    {
        testMode = type;
    }
    public List<int> getQuestionList()
    {
        return questionList;
    }
    public void setQuestionList(List<int> list)
    {
        questionList = list;
    }
    public List<int> getCorrectQuestions()
    {
        return correctQuestions;
    }
    public void addCorrectQuestion(int num)
    {
        correctQuestions.Add(num);
    }
    public List<int> getIncorrectQuestions()
    {
        return incorrectQuestions;
    }
    public void removeIncorrectQuestion(int question)
    {
        if (incorrectQuestions.Contains(question))
        {
            incorrectQuestions.Remove(question);
        }
    }
    public void setQuestionAnswer(int question, int answer)
    {
        if(checkQuestionAnswer(question))
        {
            questionAnswers[question] = answer;
        }
        else
        {
            questionAnswers.Add(answer);
        }
    }
    public bool checkQuestionAnswer(int question)
    {
        return (questionAnswers.Count > question);
    }
    public int getQuestionAnswer(int question)
    {
        return questionAnswers[question];
    }
    public void clearQuestionAnswer()
    {
        questionAnswers.Clear();
    }
    public void addIncorrectAnswer(int num)
    {
        incorrectQuestions.Add(num);
    }
    private string getCurrentDate()
    {
        string name = System.DateTime.Now.ToString("MMMM dd");
        return name;
    }
    public string getTime()
    {
        return timestamp;
    }
}
