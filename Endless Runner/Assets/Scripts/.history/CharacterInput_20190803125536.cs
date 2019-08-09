using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;

public class CharacterInput : MonoBehaviour {
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 9.8f;
    private CharacterController controller;
    private Animator anim;
    public AudioSource source;
    //private bool isChangingLane = false;
    //private bool isInSwipeArea = false;
    //private Vector3 locationAfterChangingLane;
    //distance character will move sideways
    //private Vector3 sidewaysMovementDistance = Vector3.right * 2f;
    static private float startTime;
    static private float curTime;
    public float SideWaysSpeed = 5.0f;
    public float ground = 0.25f;
    Vector3 castUp = new Vector3(0, 2, 0);

    public float JumpSpeed = 8.0f;
    public float Speed = 6.0f;
    //Max gameobject
    public Transform CharacterGO;

    
    IInputDetector inputDetector = null;
    
    // Use this for initializationq
    void Start ()
    { 
        //Initialzation of variables
        moveDirection = transform.forward;
       // moveDirection = transform.TransformDirection(moveDirection);
        transform.Translate(moveDirection,Space.Self);
        moveDirection *= Speed;
        //Setting or Resetting Manageres
        UIManager.Instance.ResetScore();    
        UIManager.Instance.SetStatus(Constants.StatusTapToStart);
        GameManager.getManager().setState(State.Start);
        //Get audio and animator components
        source= GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        inputDetector = GetComponent<IInputDetector>();
        controller =GetComponent<CharacterController>();
        //Set Animator Values
        anim.SetFloat("JumpSpeed", JumpSpeed);
        anim.SetFloat("Speed", Speed);
        anim.SetBool(Constants.ParamStarted, false);
        anim.SetFloat(Constants.ParamTurnDirection,0.0f);
        anim.SetBool(Constants.ParamJump, true);
        anim.SetBool(Constants.ParamDoubleJump, false);
        anim.SetBool(Constants.ParamGrounded,true);
        anim.SetBool(Constants.ParamTurning, false);
        anim.SetBool(Constants.ParamDead, false);
        //Set player Position
        Vector3 iPosition=Vector3.zero;
        CharacterGO.position.Set(0,0,0);
        
  
    }
    //Get animator component
    public Animator getAnim()
    {
        return anim;
    }

    //Check height is above defined threshold
    private void CheckHeight()
    {
        //Commented out to test tiger
        if (transform.position.y < -10)
        {
            GameManager.getManager().Die();
            anim.SetBool(Constants.ParamDead, true);
        
        }
    }
     // Update is called once per frame    
    private void Update()
    {
        //Switch case based on game's current state
        switch(GameManager.getManager().getState())
        {
            //Start
            case State.Start:
            {
                //Actual start of game
                if (Input.GetKeyDown(KeyCode.Space))
                {

                    //Define player direction and change game state
                    moveDirection = transform.forward;
                    moveDirection = transform.TransformDirection(moveDirection);
                    moveDirection *= Speed;
                    GameManager.getManager().setState(State.Playing);    
                    UIManager.Instance.SetStatus(string.Empty);
                    anim.SetBool(Constants.ParamStarted, true);
                    Debug.Log(  GameManager.getManager().getDirection());
                    transform.position.Set(0f,0f,2f);
                    CharacterGO.transform.position.Set(0f, 0f, 2f);
                    source.Play();
                }
                break;
            }
            //Playing State
            case State.Playing:
            {
                
                anim.SetBool(Constants.ParamGrounded, controller.isGrounded);
                UIManager.Instance.IncreaseScore(0 + Time.deltaTime);
                Speed += (Time.deltaTime*3 );
                //Check Height and detect input direction
                CheckHeight();
                Detector();
                moveDirection.y -= gravity * Time.deltaTime;
                //Secondary Ground check
                int offset = (int)CharacterGO.transform.rotation.eulerAngles.y - (int)transform.parent.transform.rotation.eulerAngles.y;
                Debug.DrawRay(CharacterGO.position + castUp, transform.TransformDirection(Vector3.down) * 2.05f, Color.black);
                Debug.DrawRay(transform.position + castUp, transform.TransformDirection(new Vector3(-1, -1, 0)) * 2.65f, Color.black);
                Debug.DrawRay(CharacterGO.position + castUp, transform.TransformDirection(new Vector3(1, -1, 0)) * 2.65f, Color.black);
                //Actual Character movement
                controller.Move(moveDirection * Time.deltaTime);
                anim.SetFloat(Constants.ParamTurnDirection, 0.0f);
                anim.SetBool(Constants.ParamTurning, false);
                break;
            }
            case State.Stumbled:
            {
                    if (startTime == -1)
                        startTime = Time.time;
                    
                    curTime = Time.time;
                    //Change state
                    if(curTime-startTime>=5.0f)
                    {
                        Debug.Log("Back to playing state");
                        GameManager.getManager().setState(State.Playing);
                    }

                break;
            }
            case State.Dead:
            {
               
                anim.SetBool(Constants.ParamDead, true);
                anim.SetBool(Constants.ParamStarted, false);
                UIManager.Instance.SetStatus("Dead. Tap Start");
                source.Stop();
                if (Input.GetKeyDown(KeyCode.Space))
                {

                    //Restart Game
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
    //Detecting input direction
    void Detector()
    {
        var inputDirection = inputDetector.DetectInputDirection();
        //Jump
        if (inputDirection == InputDirection.Top&& inputDirection.HasValue&&anim.GetBool(Constants.ParamGrounded))
        {
            anim.SetBool(Constants.ParamJump, false);
            anim.SetBool(Constants.ParamDoubleJump, true);
            //anim.SetBool(Constants.ParamJumping, true);
            anim.SetBool(Constants.ParamGrounded, false);
            anim.Play("jump", 0);
            moveDirection.y = JumpSpeed;

        }
        //Double Jump
        else if (anim.GetBool(Constants.ParamDoubleJump)&&
            inputDirection.HasValue && inputDirection == InputDirection.Top&&!anim.GetBool(Constants.ParamGrounded))
        {
            //Debug.Log("Double Jump");
            anim.SetBool(Constants.ParamJump, false);
            anim.SetBool(Constants.ParamDoubleJump, false);
            anim.Play("flip", 0);
            moveDirection.y = JumpSpeed*4;
            
        }
        //Left Right Turn
        if (GameManager.getManager().getCanTurn())
        {
            //Right Turn 
            if(inputDirection.HasValue && InputDirection.Right == inputDirection)
            {
                if (anim.GetBool(Constants.ParamGrounded))
                {
                    anim.SetBool(Constants.ParamTurning, true);
                    anim.SetFloat(Constants.ParamTurnDirection, 1.0f);
                    
                }
                transform.Rotate(0, 90, 0);
                moveDirection = Quaternion.AngleAxis(90, Vector3.up) * moveDirection;
                //allow the user to swipe once per swipe location
                
                GameManager.getManager().setTurn(false);
            }
            //Left Turn
            else if (inputDirection.HasValue && inputDirection == InputDirection.Left)
            {
                transform.Rotate(0, -90, 0);
                moveDirection = Quaternion.AngleAxis(-90, Vector3.up) * moveDirection;
                GameManager.getManager().setTurn(false);
            }
                 
        }
        
    }

}
