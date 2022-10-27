using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField]
    private string requirementDoesntMet;
    [SerializeField]
    public string startDialogue;
    [SerializeField]
    public string questDesc;
    [SerializeField]
    private string askingIsItDone;
    [SerializeField]
    private string questDone;
    public QuestType questType;
    public QuestGiver questGiver;
    public QuestGiver questTaker;
    public List<Quest> questRequirement;
    public List<QuestItem> itemRequirement;
    public Quest previousQuest;
    public List<QuestItem> rewardItem;


    public string GetRequirementDoesntMet()
    {
        return requirementDoesntMet;
    }

    public string GetStartDialogue()
    {
        return startDialogue;
    }

    public string GetQuestDesc()
    {
        return questDesc;
    }

    public string GetAskingIsItDone()
    {
        return askingIsItDone;
    }

    public string GetQuestDone()
    {
        return questDone;
    }

}

public enum QuestType
{
    GetItem,Quiz
}
