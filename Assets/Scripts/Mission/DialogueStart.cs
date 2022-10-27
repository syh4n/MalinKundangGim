using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStart : MonoBehaviour
{
    public QuestGiver questGiver;
    public DialogueSystem dialogueSystem;
    public List<string> allDialogue;
    private void Awake()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }
    public void GiveDialogue()
    {
        dialogueSystem.nextDialogue.SetActive(true);
        int x = allDialogue.Count;
        for(int i = 0; i < x; i++)
        {
            dialogueSystem.AddDialogue(allDialogue[0]);
            allDialogue.RemoveAt(0);
        }
        dialogueSystem.RefreshDialogue();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GiveDialogue();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            questGiver.OnQuestStart();
            Destroy(gameObject);
        }
    }
}
