using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NarrationManager : MonoBehaviour
{
    // Start is called before the first frame update

    int idx;
    public Image panelImage;
    public TextMeshProUGUI narration;
    public Sprite[] allNarration;
    public string[] narrationText;
    public GameObject level;
    public GameObject narrationObject;
    void Start()
    {
        Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _OnButtonNext()
    {
        if (idx < allNarration.Length - 1)
        {
            idx++;
            Refresh();
        }
        else
        {
            level.SetActive(true);
            narrationObject.SetActive(false);
        }
    }

    public void _OnButtonePrevious()
    {
        if (idx > 0)
        {
            idx--;
            Refresh();
        }
    }

    public void Refresh()
    {
        panelImage.sprite = allNarration[idx];
        narration.text = narrationText[idx];
    }
}
