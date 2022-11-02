using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    public string itemName;
    public string desc;
    public Sprite itemImage;
    public MusicManager mm;
    private void Start()
    {
        mm = FindObjectOfType<MusicManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<QuestManager>().itemInventory.Add(this);
            collision.GetComponent<QuestManager>().RefreshInventory();
            gameObject.SetActive(false);
            mm.PlayCoin();
        }
    }

}
