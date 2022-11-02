using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip mainMenu;
    public AudioClip[] stage;
    public AudioClip cutscene;
    public AudioClip coin;
    // Start is called before the first frame update

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    public void PlayMusic(AudioClip ac)
    {
        musicSource.clip = ac;
        musicSource.Play();
    }

    public void PlayCoin()
    {
        sfxSource.PlayOneShot(coin);
    }
}
