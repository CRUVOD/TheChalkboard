using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public Player thePlayer;
    public Vector3 playerStartPosition;
    [HideInInspector]
    public CameraFollow TheCamera;

    public AudioManager TheAudioManager;
   

    public string nextLevel;
    public int levelUnlock = 2;

    public int currentCheckpoint = 0;
    
    public Checkpoint[] CheckpointList;

    void Awake()
    {
        
    }

    void Start () {
        playerStartPosition = thePlayer.transform.position;

        TheAudioManager = FindObjectOfType<AudioManager>();
        

    }
	
	
	void Update ()
    {
		
	}

    public void RestartGame()
    {
        StartCoroutine("RestartGameCo");
    }

    public IEnumerator RestartGameCo()
    {
        TheAudioManager.Play("Death");
        thePlayer.gameObject.SetActive(false);
        float fadeTime = GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        thePlayer.gameObject.transform.position = playerStartPosition;
        
        TheCamera.ResetCamera();
        thePlayer.gameObject.SetActive(true);
        float fadeReverseTime = GetComponent<Fading>().BeginFade(-1);
        yield return new WaitForSeconds(fadeReverseTime);
        
    }

    public void EndGame()
    {
        PlayerPrefs.SetInt("lvlReached", levelUnlock);


        StartCoroutine("EndGameCo");
    }

    public IEnumerator EndGameCo()
    {

        thePlayer.gameObject.SetActive(false);


        float fadeTime = GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        Application.LoadLevel(nextLevel);
    }

    public void updateCheckpoint(int tag)
    {
        if (tag == 1)
        {
            currentCheckpoint = 1;
            
            CheckpointList[0].startRise();
            CheckpointList[0].current();
        }

        if (tag != 1 && tag > currentCheckpoint)
        {
            currentCheckpoint = tag;


            for (int i = 0; i < CheckpointList.Length; i++)
            {
                if (CheckpointList[i].checkpointTag == currentCheckpoint)
                {
                    playerStartPosition = CheckpointList[i].checkpointPosition;
                    TheAudioManager.Play("FlagReach");
                    CheckpointList[i].startRise();
                    CheckpointList[i].current();
                    CheckpointList[i - 1].previous();
                }
            }
        }
    }
}
