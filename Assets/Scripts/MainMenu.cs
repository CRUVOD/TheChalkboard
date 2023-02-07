using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public string levelSelect;

    public AudioManager TheAudioManager;

    public void Start()
    {

        TheAudioManager = FindObjectOfType<AudioManager>();
        TheAudioManager.Play("Theme");
    }

    public void PlayGame()
    {
        GetComponent<Fading>().BeginFade(1);

        Application.LoadLevel(levelSelect);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
