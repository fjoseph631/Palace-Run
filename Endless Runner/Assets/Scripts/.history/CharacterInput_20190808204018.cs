using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;

public class CharacterInput : MonoBehaviour {
    public float duration = 10f;
    public float gravity = 9.8f;
    bool autorun;
    //Max Elements
    static private Vector3 moveDirection = Vector3.zero;
    static private CharacterController controller;
    static private Animator anim;
    public AudioSource source;
    static private float startTime;
    static private float curTime;
    public float JumpSpeed = 8.0f;
    static public float Speed = 15.0f;
    public float initSpeed;
    //Max gameobject
    public Transform CharacterGO;
    //Input Detector
    IInputDetector inputDetector = null;    
    // Use this for initializationq
      // Use this for initializationq
    void Start ()
    { 
        moveDirection = transform.forward;
        //moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= Speed;
        //Setup
        UIManager.Instance.ResetScore();    
        UIManager.Instance.SetStatus(Constants.StatusTapToStart);
        GameManager.getManager().setState(State.Start);

        anim = GetComponent<Animator>();
        inputDetector = GetComponent<IInputDetector>();
        controller =GetComponent<CharacterController>();
        //anim.gameObject.SetActive(true);
        anim.SetFloat("JumpSpeed", JumpSpeed);
        anim.SetFloat("Speed", Speed);
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
                   
                    GameManager.getManager().setState(State.Playing);    
                    UIManager.Instance.SetStatus(string.Empty);
                    anim.SetBool(Constants.ParamStarted,true);
                   // anim.SetBool(Constants.ParamJumping, false);
                    anim.SetFloat(Constants.ParamTurnDirection, 0f);
                    anim.SetBool(Constants.ParamTurning, false);
                    anim.SetBool(Constants.ParamDoubleJump, false);
                    anim.SetBool(Constants.ParamJump, true);
                    anim.SetBool(Constants.ParamGrounded, true);
                    transform.position.Set(0f,0f,4f);
                    CharacterGO.transform.position.Set(0f, 0f, 4f);
                }
                break;
            }
            case State.Playing:
            {
                 Debug.Log(moveDirection);
                    
                if (controller.isGrounded)
                {
                    anim.SetBool(Constants.ParamGrounded, true);
                }
                 
                UIManager.Instance.IncreaseScore(0 + Time.deltaTime);
                Speed += (Time.deltaTime*3 );

                CheckHeight();
                Detector();
                moveDirection.y -= gravity * Time.deltaTime;

                int offset = (int)CharacterGO.transform.rotation.eulerAngles.y - (int)transform.parent.transform.rotation.eulerAngles.y;
                controller.Move(moveDirection * Time.deltaTime);
                transform.Translate((moveDirection*Time.deltaTime),Space.Self);
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
        if (anim.GetBool(Constants.ParamJump) && inputDirection == InputDirection.Top&& inputDirection.HasValue
        &&controller.isGrounded)
            
        {
            anim.Play("jump",0);
            anim.SetBool(Constants.ParamJump, false);
            anim.SetBool(Constants.ParamDoubleJump, true);
            //anim.SetBool(Constants.ParamJumping, true);
            //Debug.Break();
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
            //anim.Play("right turn", 0);
            //transform.position.Set(transform.position.x, transform.position.y + 20, transform.position.z);

            moveDirection.y = JumpSpeed*4;
            
        }
        else
        {
            anim.SetBool(Constants.ParamJump, false);

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
    //On hitting powerup
    void OnTriggerEnter(Collider col)
    {
        startTime=Time.deltaTime;
        if(col.tag=="AutoRun")
        {
            autorun=true;
            //Debug.Log("autorunning");
            UIManager.Instance.SetStatus("Autorunning");
            float time=Time.deltaTime;
            while(time-startTime<=(duration))
            {
                 
                if(GameManager.getManager().getDirection().Count>0)
                {
                    if(GameManager.getManager().getDirection().Peek()
                    !=GameManager.turnDirection.Straight&&GameManager.getManager().getCanTurn())
                    {
                        Debug.Log(GameManager.getManager().getDirection().Peek());
                    }
                    //Check if can turn right or left
                    if(GameManager.getManager().getDirection().Peek()
                    ==GameManager.turnDirection.Right
                    &&GameManager.getManager().getCanTurn())
                    {
                        Debug.Log("Direction "+GameManager.getManager().getDirection().Peek());
                        transform.Rotate(0, 90, 0);
                        moveDirection= Quaternion.AngleAxis(90, Vector3.up) * moveDirection;
                        GameManager.getManager().setTurn(false);

                    }
                    if(GameManager.getManager().getDirection().Peek()==GameManager.turnDirection.Left
                    &&GameManager.getManager().getCanTurn())
                    {
                        transform.Rotate(0, -90, 0);
                        moveDirection= Quaternion.AngleAxis(-90, Vector3.up) * moveDirection;
                        GameManager.getManager().setTurn(false);
                    }
                    //Check if obsticle is hit
                    time+=1f;
                    Debug.Log("Autorunning Still");

                 }
                 UIManager.Instance.SetStatus("Autorunning Over");
             }
        }
        autorun=false;
        Destroy(this);
    }
    void OnControllerColliderHit(ControllerColliderHit collision)
    {
        //Get colliders from objects tagged GameController & obsticle
       if(autorun)
       {
            GameObject obsticle= GameObject.FindWithTag("Obsticle");
            if(collision.gameObject.tag=="Obsticle")
            {
                Debug.Log(collision.gameObject);
                Destroy(collision.gameObject);
            
            }
        }
    }

}
