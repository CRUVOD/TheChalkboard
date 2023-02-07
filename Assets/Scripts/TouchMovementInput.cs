using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using WindowsInput;

public class TouchMovementInput : MonoBehaviour {

    InputSimulator s = new InputSimulator();
    public KeyboardSimulator KeyboardSimulator;
    public InputSimulator inputSim;

    

    void Start()
    {

 
    }

    void Update()
    {
        if (CrossPlatformInputManager.GetButton("jump"))
        {

            
        }

        
    }


   }

