using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float maxJumpHeight = 4;
    public float minJumpHeight = 2.5f;
    public float jumpTimeToApex = 0.4f;
    float moveSpeed = 6.5f;


    public Animator animator;
    public AudioManager TheAudioManager;
    
    bool wallSliding;
    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    float accelerationTimeAirbourne = 0.1f;
    float accelerationTimeGrounded = 0.05f;
    float velocityXSmoothing;

    public float wallSlideSpeedMax = 3;
    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;
    public float wallStickTime = 0.6f;
    float timeToWallUnstick;

    Vector3 velocity;

    
    public Image jumpButtonImage;

    Controller2D controller;

    public MyPauseMenu thePauseMenu;

    void Start()
    {
        TheAudioManager = FindObjectOfType<AudioManager>();
        controller = GetComponent<Controller2D>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(jumpTimeToApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * jumpTimeToApex;
        minJumpVelocity = Mathf.Sqrt((2 * Mathf.Abs(gravity) * minJumpHeight));

    }

    void Update()
    {
        Vector2 input;
        input = new Vector2(CrossPlatformInputManager.GetAxisRaw("Horizontal"), CrossPlatformInputManager.GetAxisRaw("Vertical"));

        
         if (CrossPlatformInputManager.GetButtonDown("left"))
            {
                input = new Vector2(-1, 0);
            }
            if (CrossPlatformInputManager.GetButtonDown("right"))
            {
                input = new Vector2(1, 0);
            }

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (CrossPlatformInputManager.GetButton("left"))
            {
                input = new Vector2(-1, 0);
            }
            if (CrossPlatformInputManager.GetButton("right"))
            {
                input = new Vector2(1, 0);
            }
        } 
            


        int wallDirX = (controller.collisions.left) ? -1 : 1;




        float targetVelocityX = input.x * moveSpeed;
        

        if (Mathf.Abs(input.x) != 0)
        {
            
            if (input.y == 0f)
            {
                
                animator.SetBool("Running", true);
                
            }
        }

        if (Mathf.Abs(input.x) == 0)
        {
            if (input.y == 0f)
            {
                animator.SetBool("Running", false);
                
            }
        }

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirbourne);


        if (velocity.x > 0)
        {
            
            transform.localScale = new Vector2(0.4f, 0.4f);
        }

        if (velocity.x < 0)
        {
            transform.localScale = new Vector2(-0.4f, 0.4f);
        }

        wallSliding = false;
        if((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y <0)
        {
            wallSliding = true;
            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (input.x != wallDirX && input.x != 0)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }
        }

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetKeyDown(KeyCode.Z) )
            {



                if (wallSliding)
                {
                    if (wallDirX == input.x)
                    {
                        velocity.x = -wallDirX * wallJumpClimb.x;
                        velocity.y = wallJumpClimb.y;
                        TheAudioManager.Play("Jump");
                    }
                    else if (input.x == 0)
                    {
                        velocity.x = -wallDirX * wallJumpOff.x;
                        velocity.y = wallJumpOff.y;
                        TheAudioManager.Play("Jump");
                    }
                    else
                    {
                        velocity.x = -wallDirX * wallLeap.x;
                        velocity.y = wallLeap.y;
                        TheAudioManager.Play("Jump");
                    }
                }

                if (controller.collisions.below)
                {
                    velocity.y = maxJumpVelocity;
                    TheAudioManager.Play("Jump");
                }
            }
            if (Input.GetKeyUp(KeyCode.Z))
            {


                if (velocity.y > minJumpHeight)
                {
                    velocity.y = minJumpHeight;
                }
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                PauseGame();
            }
        }

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (CrossPlatformInputManager.GetButtonDown("jump"))
            {

                if (wallSliding)
                {
                    if (wallDirX == input.x)
                    {
                        velocity.x = -wallDirX * wallJumpClimb.x;
                        velocity.y = wallJumpClimb.y;
                        TheAudioManager.Play("Jump");
                    }
                    else if (input.x == 0)
                    {
                        velocity.x = -wallDirX * wallJumpOff.x;
                        velocity.y = wallJumpOff.y;
                        TheAudioManager.Play("Jump");
                    }
                    else
                    {
                        velocity.x = -wallDirX * wallLeap.x;
                        velocity.y = wallLeap.y;
                        TheAudioManager.Play("Jump");
                    }
                }

                if (controller.collisions.below)
                {
                    velocity.y = maxJumpVelocity;
                    TheAudioManager.Play("Jump");
                }
            }
            if (CrossPlatformInputManager.GetButtonUp("jump"))
            {


                if (velocity.y > minJumpHeight)
                {
                    velocity.y = minJumpHeight;

                }
            }

            if (CrossPlatformInputManager.GetButton("pause"))
            {
                PauseGame();
            }
        }
       

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime, input);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
            animator.SetBool("Jumping", false);
        }
        else
        {
            animator.SetBool("Jumping", true);
        }


    }
    public void PauseGame()
    {
        
        jumpButtonImage.enabled = false;

        thePauseMenu.PauseGame();
    }

    public void DisablePauseMenu()
    {
        Time.timeScale = 1f;
        
        jumpButtonImage.enabled = true;

        thePauseMenu.gameObject.SetActive(false);
    }



    public void VelocityReset()
    {
        velocity = Vector3.zero;
    }
}
