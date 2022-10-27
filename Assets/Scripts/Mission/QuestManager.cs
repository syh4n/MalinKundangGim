using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest currentQuest;
    public List<Quest> questDone;
    public List<QuestItem> itemInventory;
    public int levelIndex;

    public UIManager uiM;
    public InventoryManager inventoryManager;
    public int amountQuestRequired;

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        uiM = FindObjectOfType<UIManager>();
    }
    public void RefreshInventory()
    {
        inventoryManager.RefreshInventory();
    }

    public void CheckQuestDone()
    {
        if(amountQuestRequired  <= questDone.Count)
        {
            uiM.OnWin((levelIndex+1));
        }
    }
}
