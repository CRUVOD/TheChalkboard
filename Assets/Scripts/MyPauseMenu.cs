using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MyPauseMenu : MonoBehaviour {

    public string mainMenuLevel;
    public GameManager theGameManager;
    public GameObject thePauseMenu;
    public GameObject[] setInvisibleList;
    public Player thePlayer;

    public void Start()
    {
        Time.timeScale = 1f;
        
    }

    public void PauseGame()
    {
        thePauseMenu.gameObject.SetActive(true);
        

        Time.timeScale = 1f;
    }

    public void ResumeGame()
    {
        
        Time.timeScale = 1f;

        thePlayer.DisablePauseMenu();
    }


    public void RestartGame()
    {
        Time.timeScale = 1f;

        
        theGameManager.RestartGame();


        thePlayer.DisablePauseMenu();

    }

    public void QuitToMain()
    {

        Time.timeScale = 1f;
        thePlayer.DisablePauseMenu();

        Application.LoadLevel("MainMenu");
    }

   

    
}
