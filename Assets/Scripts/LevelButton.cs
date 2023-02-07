using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class LevelButton : MonoBehaviour
{

    private Vector3 newPosition;
    public float shiftPosition;
    public float shiftSpeed;
    public float nextAvailablePressDelay;
    private bool leftPressAvailable = false;
    private bool rightPressAvailable = true;
    public LevelSelect levelSelectControl;

    private void Awake()
    {
        newPosition = transform.position;

        
    }

    void Start()
    {
        Debug.LogWarning(newPosition + "Here");
    }

    
    void Update()
    {
        PositionChanging();
    }



    public void PositionChanging()
    {
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || CrossPlatformInputManager.GetButton("left")) && leftPressAvailable == true)
        {
            newPosition = new Vector3(transform.position.x + shiftPosition, transform.position.y);
            
            StartCoroutine("LeftLevelSelectPressed");
            
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow) || CrossPlatformInputManager.GetButton("right")) && rightPressAvailable == true)
        {
            newPosition = new Vector3(transform.position.x - shiftPosition, transform.position.y);
            
            StartCoroutine("RightLevelSelectPressed");
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, shiftSpeed * Time.deltaTime);
    }



    IEnumerator RightLevelSelectPressed()
    {
        rightPressAvailable = false;

        

        yield return new WaitForSeconds(nextAvailablePressDelay - 0.1f);

        

        yield return new WaitForSeconds(0.1f);

        if (levelSelectControl.currentLevelDisplayed == levelSelectControl.levelScreens.Length)
        {
            rightPressAvailable = false;
            leftPressAvailable = true;
        }

        else
        {
            rightPressAvailable = true;
        }
        


    }

    IEnumerator LeftLevelSelectPressed()
    {
        leftPressAvailable = false;

        yield return new WaitForSeconds(nextAvailablePressDelay - 0.1f);

        

        yield return new WaitForSeconds(0.1f);

        if (levelSelectControl.currentLevelDisplayed == 1)
        {
            rightPressAvailable = true;
            leftPressAvailable = false;
        }

        else
        {
            leftPressAvailable = true;
        }

    }
}
