using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<Image> allImage;
    public QuestManager qm;

    private void Start()
    {
        qm = FindObjectOfType<QuestManager>();
    }

    public void RefreshInventory()
    {
        for(int i=0; i < allImage.Count; i++)
        {
            if(i < qm.itemInventory.Count)
            {
                allImage[i].sprite = qm.itemInventory[i].itemImage;
                allImage[i].gameObject.SetActive(true);

            }
            else
            {
                allImage[i].gameObject.SetActive(false);
            }
        }
    }
}
