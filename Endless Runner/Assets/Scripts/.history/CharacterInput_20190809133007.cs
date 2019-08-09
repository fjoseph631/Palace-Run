using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;

public class CharacterInput : MonoBehaviour {
    public static Vector3 moveDirection = Vector3.zero;
    public float gravity = 9.8f;
    //Character components
    private CharacterController controller;
    private Animator anim;
    public static bool autorun=false;
    //Static duration
    public static float duration;
    //User defined duration
    public float autoDuration=10f;
    //Speed As Set by user
    public float jumpSpeed = 8.0f;
    //Static Speed used by other classes
    public static float speed;
    public float initSpeed = 15f;
    //Max gameobject
    public Transform CharacterGO;
   
    //Input Detection
    IInputDetector inputDetector = null;
    
    // Use this for initialization
    void Start ()
    { 
        speed=initSpeed;
        duration=autoDuration;
        //Establish player movement direction
        moveDirection = transform.forward;
        transform.Translate(moveDirection,Space.Self);
        moveDirection *= Speed;
        //Setup UI
        UIManager.Instance.ResetScore();    
        UIManager.Instance.SetStatus(Constants.StatusTapToStart);
        //Setup game maanager
        GameManager.getManager().setState(State.Start);
        //Reset Camera
        CameraController.rotation.y=0;
        //Get Player Components
        anim = GetComponent<Animator>();
        inputDetector = GetComponent<IInputDetector>();
        controller =GetComponent<CharacterController>();
        //Set anim components     
        anim.SetBool(Constants.ParamStarted, false);
        anim.SetBool(Constants.ParamJump, true);
        anim.SetBool(Constants.ParamDoubleJump, false);
        anim.SetBool(Constants.ParamDead, false);
        
        CharacterGO.position.Set(0,0,0);
        
  
    }
    //Getter of animator component
    public Animator getAnim()
    {
        return anim;
    }

    //Check Height of character
    private void CheckHeight()
    {
        if (transform.position.y < -10)
        {
            //Player's fallen too far- Time to Die!
            GameManager.getManager().Die();
            anim.SetBool(Constants.ParamDead, true);
        
        }
    }
    //Called once per Frame
    private void Update()
    {
        switch(GameManager.getManager().getState())
        {
            case State.Start:
            {
                //Player's started play
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    moveDirection = transform.forward;
                    moveDirection = transform.TransformDirection(moveDirection);
                    moveDirection *= Speed;
                    //Change Player State
                    GameManager.getManager().setState(State.Playing);
                    //Initialize UI Status    
                    UIManager.Instance.SetStatus(string.Empty);
                    //Update animator parameters
                    anim.SetBool(Constants.ParamStarted,true);
                    anim.SetBool(Constants.ParamDoubleJump, false);
                    anim.SetBool(Constants.ParamJump, true);
                    anim.SetBool(Constants.ParamGrounded, true);
                    //Reset Max
                    CharacterGO.transform.position.Set(0f, 0f, 4f);
                }
                break;
            }
            case State.Playing:
            {
                //Ground Check
                anim.SetBool(Constants.ParamGrounded, controller.isGrounded); 
                //Increase Score   
                UIManager.Instance.IncreaseScore(0 + Time.deltaTime);
                //Increase Speed
                Speed += (Time.deltaTime*3 );
                
                CheckHeight();
                Detector();
                //Apply Gravity
                moveDirection.y -= gravity * Time.deltaTime;
                //Actual move of character
                controller.Move(moveDirection * Time.deltaTime);
                break;
                    
            }
            //Will be implemented later
            case State.Stumbled:
            {
            
                break;
            }
            case State.Dead:
            {
               //Player's Dead - Update UI and animator
                anim.SetBool(Constants.ParamDead, true);
                anim.SetBool(Constants.ParamStarted, false);
                UIManager.Instance.SetStatus(Constants.StatusDeadTapToStart);
                //Player wants to restart
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //restart
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    GameManager.getManager().Awake();
                }
                    break;
            }
            default:
            {
                break;
            }
        }
        
    }
    //Input Detection
    void Detector()
    {
        //Get input Direction
        var inputDirection = inputDetector.DetectInputDirection();
        //Jump
        if (anim.GetBool(Constants.ParamJump) && inputDirection == InputDirection.Top
        && inputDirection.HasValue
        &&anim.GetBool(Constants.ParamGrounded))
        {
            anim.SetBool(Constants.ParamJump, false);
            anim.SetBool(Constants.ParamDoubleJump, true);
            //anim.SetBool(Constants.ParamJumping, true);
            anim.SetBool(Constants.ParamGrounded, false);
            //Debug.Break();
            anim.Play(Constants.AnimationJump, 0);
            moveDirection.y = JumpSpeed;
        }
        //Double Jump - Super Jump
        else if (!anim.GetBool(Constants.ParamJump)&&anim.GetBool(Constants.ParamDoubleJump)&&
            inputDirection.HasValue && !anim.GetBool(Constants.ParamGrounded))
        {
            anim.SetBool(Constants.ParamJump, false);
            anim.SetBool(Constants.ParamDoubleJump, false);
            anim.Play(Constants.AnimationDoubleJump, 0);
            moveDirection.y = JumpSpeed*4;
            
        }
        //Left or Right Turn
        if (GameManager.getManager().getCanTurn())
        {
            //Right Turn 
            if(inputDirection.HasValue && InputDirection.Right == inputDirection)
            {
                if (controller.isGrounded)
                {
                    anim.SetBool(Constants.ParamTurning, true);
                    anim.SetFloat(Constants.ParamTurnDirection, 1.0f);
                    
                }
                CameraController.rotation.y+=90;
                transform.Rotate(0, 90, 0);
                moveDirection = Quaternion.AngleAxis(90, Vector3.up) * moveDirection;
                //allow the user to swipe once per swipe location
                
                GameManager.getManager().setTurn(false);
            }
            //Left Turn
            else if (inputDirection.HasValue && inputDirection == InputDirection.Left)
            {
                //Turn Both Player and camera and update moveDirection
                transform.Rotate(0, -90, 0);
                CameraController.rotation.y-=90;
                moveDirection = Quaternion.AngleAxis(-90, Vector3.up) * moveDirection;
                //Allow only one swipe per trigger
                GameManager.getManager().setTurn(false);
            }
                 
        }
        
        
    }
    
}
    

