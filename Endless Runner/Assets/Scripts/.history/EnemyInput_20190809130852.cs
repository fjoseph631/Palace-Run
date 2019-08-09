using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : MonoBehaviour
{
    //Quarry for today
    public GameObject player;
    //Move Direction
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 9.8f;
    public float speed=14f;
    private CharacterController controller;
    private Animator anim;
   
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller =GetComponent<CharacterController>();
        moveDirection = transform.forward;
        anim.SetBool(Constants.ParamStart, false);
        //moveDirection = transform.TransformDirection(moveDirection);
        transform.position.Set(0f,0f,2f);
        transform.Translate(moveDirection,Space.Self);
        //CharacterGO.position.Set(0,0,0);
        moveDirection *= speed;
        
    }
    //Get animator component
    public Animator getAnim()
    {
        return anim;
    }

    // Update is called once per frame
    void Update()
    {
        //Game has started
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool(Constants.ParamStart, true);
        }
        if(GameManager.getManager().getState()==State.Playing)
        {
            //Look at its prey
            Quaternion.Slerp(this.transform.rotation,
            Quaternion.LookRotation(player.transform.position,this.transform.position),
            3* Time.deltaTime);
            detector();
            //Actual Movement of character
            controller.Move(moveDirection*Time.deltaTime);
            //Increase speed at lesser rate than player
            speed+=(Time.deltaTime*(float)2.75);
           

        }
      
    }
    //Turn tiger as needed
    void detector()
    {
        //if(GameManager.getManager().getCanSwipe())
            //Debug.Log("Tiger Swipe "+ GameManager.getManager().getCanSwipe());
        if(GameManager.getManager().getDirection().Count>0)
        {
            //Turn Tiger Left
            if( GameManager.getManager().getDirection().Peek()==GameManager.turnDirection.Left
                &&GameManager.getManager().getCanSwipe())
            {
                transform.Rotate(0, -90, 0);
                moveDirection = Quaternion.AngleAxis(-90, Vector3.up) * moveDirection;
                //Debug.Log("Tiger Turned Left");
                GameManager.getManager().setSwipe(false);                
            }
            //Turn Tiger Right
            if( GameManager.getManager().getDirection().Peek()==GameManager.turnDirection.Right
                &&GameManager.getManager().getCanSwipe())
            {
                transform.Rotate(0, 90, 0);
                moveDirection = Quaternion.AngleAxis(90, Vector3.up) * moveDirection;
                //Debug.Log("Tiger Turned Right");
                GameManager.getManager().setSwipe(false);
                
            }
        }
    }
   

    //Destroy obsticles that tiger collides with
    void OnControllerColliderHit(ControllerColliderHit collision)
    {     
        //Get colliders from objects tagged GameController & obsticle
       
        GameObject obsticle= GameObject.FindWithTag("Obsticle");
        GameObject tiger= GameObject.FindWithTag(Constants.Tiger);
        //Destroys obsticles as needed
        if(collision.gameObject.tag=="Obsticle")
        {
            //Debug.Log(collision.gameObject);
            Destroy(collision.gameObject);
           
        }
        //Tiger's reached player -- time to die
        if(collision.gameObject.tag==Constants.PlayerTag)
        {
            anim.Play("hit",0);
            GameManager.getManager().Die();
            anim.SetBool(Constants.ParamStart,false);
        }   
        
        
    }
    
}
