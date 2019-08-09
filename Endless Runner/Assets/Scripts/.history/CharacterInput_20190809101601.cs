using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;

public class CharacterInput : MonoBehaviour {
    public static Vector3 moveDirection = Vector3.zero;
    public float gravity = 9.8f;
    private CharacterController controller;
    private Animator anim;
    public static   bool autorun;
    public float duration;

    private bool isChangingLane = false;
    private bool isInSwipeArea = false;
    private Vector3 locationAfterChangingLane;
    //distance character will move sideways
    private Vector3 sidewaysMovementDistance = Vector3.right * 2f;
    static private float startTime;
    static private float curTime;
    public float SideWaysSpeed = 5.0f;
    public float ground = 0.25f;
    Vector3 castUp = new Vector3(0, 2, 0);

    public float JumpSpeed = 8.0f;
    public static float Speed;
    public float initSpeed = 15f;
    //Max gameobject
    public Transform CharacterGO;
   
    
    IInputDetector inputDetector = null;
    
    // Use this for initializationq
    void Start ()
    { 
        Speed=initSpeed;
        moveDirection = transform.forward;
       // moveDirection = transform.TransformDirection(moveDirection);
        transform.Translate(moveDirection,Space.Self);
        moveDirection *= Speed;
        //Setup
        UIManager.Instance.ResetScore();    
        UIManager.Instance.SetStatus(Constants.StatusTapToStart);
        GameManager.getManager().setState(State.Start);
    CameraController.rotation.y=0;
        anim = GetComponent<Animator>();
        inputDetector = GetComponent<IInputDetector>();
        controller =GetComponent<CharacterController>();
        //anim.gameObject.SetActive(true);
        anim.SetFloat("JumpSpeed", JumpSpeed);
        anim.SetFloat("Speed", initSpeed);
        anim.SetBool(Constants.ParamStarted, false);
        anim.SetFloat(Constants.ParamTurnDirection,0.0f);
        anim.SetBool(Constants.ParamJump, true);
        anim.SetBool(Constants.ParamDoubleJump, false);
        anim.SetBool(Constants.ParamGrounded,true);
        anim.SetBool(Constants.ParamTurning, false);
        anim.SetBool(Constants.ParamDead, false);
        
       
        Vector3 iPosition=Vector3.zero;
        CharacterGO.position.Set(0,0,0);
        
  
    }
    public Animator getAnim()
    {
        return anim;
    }

    // Update is called once per frame
    private void CheckHeight()
    {
        if (transform.position.y < -10)
        {
            GameManager.getManager().Die();
            anim.SetBool(Constants.ParamDead, true);
        
        }
    }
    
    private void Update()
    {
        switch(GameManager.getManager().getState())
        {
            case State.Start:
            {
                  
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    moveDirection = transform.forward;
                    moveDirection = transform.TransformDirection(moveDirection);
                    moveDirection *= Speed;
                    GameManager.getManager().setState(State.Playing);    
                    UIManager.Instance.SetStatus(string.Empty);
                    anim.SetBool(Constants.ParamStarted,true);
                   // anim.SetBool(Constants.ParamJumping, false);
                    anim.SetFloat(Constants.ParamTurnDirection, 0f);
                    anim.SetBool(Constants.ParamTurning, false);
                    anim.SetBool(Constants.ParamDoubleJump, false);
                    anim.SetBool(Constants.ParamJump, true);
                    anim.SetBool(Constants.ParamGrounded, true);
                    CharacterGO.transform.position.Set(0f, 0f, 4f);
                }
                break;
            }
            case State.Playing:
            {
                if (controller.isGrounded)
                {
                    anim.SetBool(Constants.ParamGrounded, true);
                }
                 
                UIManager.Instance.IncreaseScore(0 + Time.deltaTime);
                Speed += (Time.deltaTime*3 );

                CheckHeight();
                Detector();
                AutoRun();
                moveDirection.y -= gravity * Time.deltaTime;

                int offset = (int)CharacterGO.transform.rotation.eulerAngles.y - (int)transform.parent.transform.rotation.eulerAngles.y;
                Debug.DrawRay(CharacterGO.position + castUp, transform.TransformDirection(Vector3.down) * 2.05f, Color.black);
                Debug.DrawRay(transform.position + castUp, transform.TransformDirection(new Vector3(-1, -1, 0)) * 2.65f, Color.black);
                Debug.DrawRay(CharacterGO.position + castUp, transform.TransformDirection(new Vector3(1, -1, 0)) * 2.65f, Color.black);
                    //if(offset==0)
                controller.Move(moveDirection * Time.deltaTime);
                //transform.Translate((moveDirection*Time.deltaTime),Space.Self);
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

    void Detector()
    {
      
        var inputDirection = inputDetector.DetectInputDirection();
        //Jump
        //Debug.Log(inputDirection.ToString());
        if (anim.GetBool(Constants.ParamJump) && inputDirection == InputDirection.Top&& inputDirection.HasValue&&anim.GetBool(Constants.ParamGrounded))
            
        {
            anim.SetBool(Constants.ParamJump, false);
            anim.SetBool(Constants.ParamDoubleJump, true);
            //anim.SetBool(Constants.ParamJumping, true);
            anim.SetBool(Constants.ParamGrounded, false);
            //Debug.Break();
            anim.Play("jump", 0);
            moveDirection.y = JumpSpeed;
            //anim.SetBool(Constants.ParamDescending, true);
            //transform.position.Set(transform.position.x, transform.position.y + 20, transform.position.z);

        }
        //Double Jump
        else if (!anim.GetBool(Constants.ParamJump)&&anim.GetBool(Constants.ParamDoubleJump)&&
            inputDirection.HasValue && !anim.GetBool(Constants.ParamGrounded))
        {
            //Debug.Log("Double Jump");
            anim.SetBool(Constants.ParamJump, false);
            anim.SetBool(Constants.ParamDoubleJump, false);
            anim.Play("flip", 0);
            //anim.Play("right turn", 0);
            //transform.position.Set(transform.position.x, transform.position.y + 20, transform.position.z);

            moveDirection.y = JumpSpeed*4;
            
        }
        //Left Right Turn
        if (GameManager.getManager().getCanTurn())
        {
            //Right Turn 
            if(inputDirection.HasValue && InputDirection.Right == inputDirection)
            {
                //Debug.Log("Turning Right");
                if (anim.GetBool(Constants.ParamGrounded))
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
                transform.Rotate(0, -90, 0);
                CameraController.rotation.y-=90;
                moveDirection = Quaternion.AngleAxis(-90, Vector3.up) * moveDirection;

                GameManager.getManager().setTurn(false);
            }
                 
        }
        
    }
    //On hitting powerup

    void AutoRun()
    {
       
        //Get colliders from objects tagged GameController & obsticle
        startTime=Time.deltaTime;
       if(autorun)
       {
           autorun=true;
            Debug.Log("autorunning");
            UIManager.Instance.SetStatus("Autorunning");
            StartCoroutine(autorunTimer());
        IEnumerator autorunTimer()
        {
            float timePassed=0;
            while (timePassed<duration)
            {
                timePassed+=Time.deltaTime;
                yield return null;
            }
        }
            UIManager.Instance.SetStatus("Not Autorunning");        
           
        
    }
    }
}
    

