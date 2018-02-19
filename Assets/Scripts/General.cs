using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class General
{
    List<int> answeredQuestions = new List<int>();
    List<int> starredQuestion = new List<int>();
    int totalTests = 0;

    public List<int> getAnsweredQuestions()
    {
        return answeredQuestions;
    }
    public List<int> getStarredQuestions()
    {
        return starredQuestion;
    }
    public int getTotalTest()
    {
        return totalTests;
    }
    public void setAnsweredQuestions(List<int> list)
    {
        answeredQuestions = list;
    }
    public void setStarredQuestions(List<int> list)
    {
        starredQuestion = list;
    }
    public void setTotalTests(int total)
    {
        totalTests = total;
    }
    public void answerQuestion(int id)
    {
        if(!answeredQuestions.Contains(id))
        {
            if(answeredQuestions == null)
            {
                Debug.Log("here");
            }
            answeredQuestions.Add(id);
        }
    }
    public void starQuestion(int id)
    {
        if(!starredQuestion.Contains(id))
        {
            starredQuestion.Add(id);
        }
    }
    public void removeStarQuestion(int id)
    {
        if (starredQuestion.Contains(id))
        {
            starredQuestion.Remove(id);
        }
    }
}
