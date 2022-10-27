using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public GameObject dialogBox;
    public GameObject skipDialogue;
    public GameObject nextDialogue;
    public List<string> dialogueQueQue;
    public GameObject buttonAnswer;
    public Text[] allAnswer;
    public TextMeshProUGUI textComp;
    public string toTell;
    public QuestGiver questTaker;
    public QuestQuestion currentQuestion;
    public QuestManager questManager;
    public float textSpeed;
    public bool isDialogueOpen;

    public float timer;
    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            buttonAnswer.SetActive(false);
            nextDialogue.SetActive(true);

            if (timer < 0)
            {
                timer = 0;
            }
        }

    }
    public void _OnButtonAnswerQuestion(int answer)
    {   
        isDialogueOpen = false;
        if(currentQuestion.theRealAnswer == answer)
        {          
            AddDialogue(currentQuestion.rightAnswer);
            buttonAnswer.gameObject.SetActive(false);
            questTaker.OnQuestDone();
            nextDialogue.SetActive(true);
        }
        else
        {
            AddDialogue(currentQuestion.wrongAnswer);
            RefreshDialogue();
            timer = 10;
        }
    }
    public void RefreshQuestion()
    {
        dialogueQueQue.Clear();
        dialogueQueQue.Add(currentQuestion.startDialogue);
        buttonAnswer.SetActive(true);
        nextDialogue.SetActive(false);
        if (currentQuestion != null)
        {
            for(int i=0; i<allAnswer.Length; i++)
            {
                allAnswer[i].text = currentQuestion.answer[i];
            }
        }
        if(timer > 0)
        {
            dialogueQueQue.Clear();
            dialogueQueQue.Add("Tolong pikirkan lagi jawabanmu, Kembalilah setelah " + timer.ToString("F0") + " detik");
            
        }
        RefreshDialogue();
    }
    public void OnDialogue()
    {
        questManager.GetComponent<PlayerController>().SetIdle();
        questManager.GetComponent<PlayerController>().enabled = false;
        dialogBox.SetActive(true);
        textComp.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    public void AddDialogue(string dialogueAdd)
    {
        dialogueQueQue.Add(dialogueAdd);
    }

    public void _OnButtonSkipDialogue()
    {
        StopAllCoroutines();
        textComp.text = toTell;
        skipDialogue.SetActive(false);


    }

    public void _OnButtonNextDialogue()
    {
        isDialogueOpen = false;
        if (dialogueQueQue.Count <= 0)
        {
            
            
                dialogBox.SetActive(false);
                questManager.CheckQuestDone();
                questManager.GetComponent<PlayerController>().enabled = true;
            
        }
        if (questManager.currentQuest == null  && questTaker != null)
        {          
            if(questTaker.allQuestList.Count > 0)
            questTaker.OnQuestStart();
        }
            
        
        //  questGiver = null;

        //if (questManager.currentQuest.questType == QuestType.Quiz)
        //{
        //    questTaker.OnAskQuestion();
        //}

        RefreshDialogue(); 
    }

    public void RefreshDialogue()
    {
        if (dialogueQueQue.Count > 0 && !isDialogueOpen)
        {
            isDialogueOpen = true;
            dialogBox.SetActive(true);
            toTell = dialogueQueQue[0];
            textComp.text = string.Empty;
            StartCoroutine(TypeLine());
            dialogueQueQue.RemoveAt(0);
        }
    }

    IEnumerator TypeLine()
    {
        questManager.GetComponent<PlayerController>().SetIdle();
        questManager.GetComponent<PlayerController>().enabled = false;

        int howManyChar = toTell.ToCharArray().Length;
        skipDialogue.SetActive(true);
        foreach (char c in toTell.ToCharArray())
        {
            textComp.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
        skipDialogue.SetActive(false);

    }
}
