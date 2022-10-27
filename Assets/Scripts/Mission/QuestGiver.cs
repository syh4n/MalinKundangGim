using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public List<Quest> allQuestList;
    public List<Quest> allQuestTaker;
    public QuestManager questManager;
    public DialogueSystem dialogueSystem;
    public string dialogueNoMore;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }
    public void OnQuestStart()
    {
        if (allQuestList.Count != 0)
        {
            if (questManager.questDone.Find(x => x == allQuestList[0].previousQuest) || allQuestList[0].previousQuest == null)
            {
                
                questManager.currentQuest = allQuestList[0];
                allQuestList.RemoveAt(0);
                dialogueSystem.AddDialogue(questManager.currentQuest.GetStartDialogue());
                if (questManager.currentQuest.questType == QuestType.Quiz)
                {
                    OnAskQuestion();
                    dialogueSystem.nextDialogue.SetActive(false);
                }
                else
                    dialogueSystem.nextDialogue.SetActive(true);

            }
            else
            {
                dialogueSystem.AddDialogue(allQuestList[0].GetRequirementDoesntMet());
            }
            dialogueSystem.RefreshDialogue();
        }
        
    }

    public void OnCheckQuest()
    {
       
            bool questPass = true;
            bool itemPass = true;
            if (allQuestTaker[0].questRequirement.Count != 0)
            {
                for (int i = 0; i < allQuestTaker[0].questRequirement.Count; i++)
                {
                    if (!questManager.questDone.Find(x => x == allQuestTaker[0].questRequirement[i]))
                    {
                        questPass = false;
                        break;
                    }
                }
            }
            if (allQuestTaker[0].itemRequirement.Count != 0)
            {
                for (int i = 0; i < allQuestTaker[0].itemRequirement.Count; i++)
                {
                    if (!questManager.itemInventory.Find(x => x == allQuestTaker[0].itemRequirement[i]))
                    {
                        itemPass = false;
                        break;
                    }
                }
            }

        if (questPass && itemPass)
        {
            OnQuestDone();
        }
        else
        {
            dialogueSystem.AddDialogue(questManager.currentQuest.GetAskingIsItDone());
            dialogueSystem.RefreshDialogue();
        }

        
    }

    public void OnQuestDone()
    {
        for(int i=0; i < questManager.currentQuest.itemRequirement.Count; i++)
        {
            questManager.itemInventory.Remove(questManager.currentQuest.itemRequirement[i]);
        }
        for(int i = 0; i < questManager.currentQuest.rewardItem.Count; i++)
        {
            questManager.itemInventory.Add(questManager.currentQuest.rewardItem[i]);
        }

        questManager.RefreshInventory();
        dialogueSystem.AddDialogue(questManager.currentQuest.GetQuestDone());
        dialogueSystem.nextDialogue.SetActive(true);

        questManager.questDone.Add(questManager.currentQuest);
        allQuestTaker.Remove(questManager.currentQuest);
        dialogueSystem.questTaker = this;
        questManager.currentQuest = null;
        

        if (allQuestList.Count != 0) { }
          //  OnQuestStart();
        else
            dialogueSystem.AddDialogue(dialogueNoMore);
            dialogueSystem.RefreshDialogue();
    }

    public void OnAskQuestion()
    {
        QuestQuestion myQuestion = questManager.currentQuest.GetComponent<QuestQuestion>();
        dialogueSystem.questTaker = this;
        dialogueSystem.currentQuestion = myQuestion;
        //dialogueSystem.nextDialogue.SetActive(false);
        //dialogueSystem.AddDialogue(myQuestion.GetQuestion());
        
        dialogueSystem.RefreshQuestion();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (allQuestList.Count == 0&& allQuestTaker.Count == 0 && questManager.currentQuest != null)
            {
                if (questManager.currentQuest.questGiver == this)
                    dialogueSystem.AddDialogue(questManager.currentQuest.questDesc);
                else
                    dialogueSystem.AddDialogue("Aku tidak ada tugas lagi untukmu");
                dialogueSystem.RefreshDialogue();
            }
            else if (questManager.currentQuest == null && allQuestList.Count > 0)
            {
                OnQuestStart();
            }
            else if (allQuestTaker.Count > 0) {
                if (questManager.currentQuest != null)
                {
                    if (questManager.currentQuest.questTaker == this && allQuestTaker[0] == questManager.currentQuest)
                    {
                        if (questManager.currentQuest.questType == QuestType.GetItem)
                            OnCheckQuest();
                        else if (questManager.currentQuest.questType == QuestType.Quiz)
                        {
                            OnAskQuestion();
                        }
                    }
                }
            }


        }
    }
}
