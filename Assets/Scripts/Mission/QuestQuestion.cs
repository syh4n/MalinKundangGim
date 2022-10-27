using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestQuestion : Quest
{
    public string theQuestion;
    public List<string> answer;
    public int theRealAnswer;
    public string wrongAnswer;
    public string rightAnswer;

    public string GetQuestion()
    {
        return theQuestion;
    }
}
