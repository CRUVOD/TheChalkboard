using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class LevelSelect : MonoBehaviour {

    public GameObject[] levelScreens;

    public float LSshiftPosition;

    public float LSnextAvailablePressDelay;

    public float LSshiftSpeed;

    public float smoothTime;

    public Button[] levelButtons;

    public int currentLevelDisplayed = 1;

    private bool levelSelectValueChangeReady = true;



    private void Awake()
    {

        for (int i = 0; i < levelScreens.Length; i++)
        {
            LevelButton theLevelButton = levelScreens[i].GetComponent<LevelButton>();




            theLevelButton.shiftPosition = LSshiftPosition;
            theLevelButton.nextAvailablePressDelay = LSnextAvailablePressDelay;
            theLevelButton.shiftSpeed = LSshiftSpeed;
        }
    }

    void Start ()
    {
        //StartCoroutine("DelayedShiftSpeedUpdate");


       
    


        int lvlReached = PlayerPrefs.GetInt("lvlReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > lvlReached)
            {
                levelButtons[i].interactable = false;
            }
            
        }
	}

    

    public void PlayGameLevel (string levelName)
    {
        GetComponent<Fading>().BeginFade(1);

        Application.LoadLevel(levelName);
    }

    void Update ()
    {
        LevelDisplayCehck();

    }

    void LevelDisplayCehck()
    {
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || CrossPlatformInputManager.GetButton("left")) && currentLevelDisplayed > 1 && levelSelectValueChangeReady == true)
        {
            currentLevelDisplayed -= 1;

            StartCoroutine("PotentialValueChangeCheck");
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow) || CrossPlatformInputManager.GetButton("right")) && currentLevelDisplayed < levelScreens.Length && levelSelectValueChangeReady == true)
        {
            currentLevelDisplayed += 1;

            StartCoroutine("PotentialValueChangeCheck");
        }
    }

    IEnumerator PotentialValueChangeCheck()
    {
        levelSelectValueChangeReady = false;

        yield return new WaitForSeconds(LSnextAvailablePressDelay);

        levelSelectValueChangeReady = true;
    }

   /** IEnumerator DelayedShiftSpeedUpdate()
    {
        yield return new WaitForSeconds(5f);

        Debug.LogWarning("shift");

        for (int i = 0; i < levelScreens.Length; i++)
        {
            LevelButton theLevelButton = levelScreens[i].GetComponent<LevelButton>();





            theLevelButton.shiftSpeed = LSshiftSpeed;
        }
    }
    **/
}
