using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Checkpoint : GameManager {

    [HideInInspector]
    

    public Vector3 checkpointPosition;
    public Checkpoint theCheckpoint;
    public int checkpointTag;
    public Animator animator;
    

    void Start ()
    {
        checkpointPosition = theCheckpoint.transform.position;
	}
	

	void Update ()
    {

	}

    public void startRise()
    {
        animator.SetBool("flagHit", true);
    }

    public void current()
    {
        animator.SetBool("flagCurrent", true);
    }

    public void previous()
    {
        animator.SetBool("flagCurrent", false);
    }

}
