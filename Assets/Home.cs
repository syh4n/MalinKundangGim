using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Home : MonoBehaviour
{
    // Start is called before the first frame update
    public Button[] allLevel;

    void Start()
    {
        for(int i=0; i < allLevel.Length; i++)
        {
            if (i < PlayerPrefs.GetInt("LevelProgress", 1))
            {
                allLevel[i].interactable = true;
            }
            else
            {
                allLevel[i].interactable = false;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _OnButtonSelectLevel(int idx)
    {
        SceneManager.LoadScene("Gameplay "+idx);
    }

    public void _OnButtonMainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }

    public void _OnButtonExitApp()
    {
        Application.Quit();
    }
}
