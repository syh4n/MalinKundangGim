using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public GameObject panelPause;
    public GameObject panelWin;
    public int nextLevel;
    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    public void _OnButtonSceneMainMenu()
    {
        ChangeScene("MainMenu");
    }

    public void _OnButtonPause()
    {
        panelPause.SetActive(true);
        Time.timeScale = 0;
    }
    public void _OnButtonResume()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1;
    }
    public void OnWin(int scene)
    {
        panelWin.SetActive(true);
        nextLevel = scene;
        if (PlayerPrefs.GetInt("LevelProgress",1) <= scene)
        {
            PlayerPrefs.SetInt("LevelProgress", scene + 1);
        }
    }


    public void _OnButtonNextLevel()
    {
        ChangeScene("Gameplay "+ nextLevel);

    }

    public void _OnButtonRetryLevel()
    {
        ChangeScene("Gameplay " + (nextLevel-1));
    }
}
