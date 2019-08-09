using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : MonoBehaviour
{
    public GameObject player;
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 9.8f;
    public float speed;
    private CharacterController controller;
    private Animator anim;
    //Max gameobject
    //public Transform CharacterGO;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller =GetComponent<CharacterController>();
        moveDirection = transform.forward;
        anim.SetBool(Constants.ParamStart, false);
       // moveDirection = transform.TransformDirection(moveDirection);
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
         if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool(Constants.ParamStart, true);

        }
        if(GameManager.getManager().getState()==State.Playing)
        {
            transform.LookAt(player.transform);
            transform.x=player.transform.x;
            detector();
            controller.Move(moveDirection*Time.deltaTime);
            speed+=(Time.deltaTime*(float)2.75);
            //if(GameManager.getManager().getDirection().Count>0)
              //  Debug.Log(GameManager.getManager().getDirection().Peek());
        }
      
    }
    //Turn tiger as needed
    void detector()
    {
        if(GameManager.getManager().getCanSwipe())
            Debug.Log(GameManager.getManager().getCanSwipe());
        if(GameManager.getManager().getDirection().Count>0)
        {
            //Turn Tiger Left
            if( GameManager.getManager().getDirection().Peek()==GameManager.turnDirection.Left
                &&GameManager.getManager().getCanSwipe())
            {
                transform.Rotate(0, -90, 0);
                moveDirection = Quaternion.AngleAxis(-90, Vector3.up) * moveDirection;
                Debug.Log("Tiger Turned Left");
                GameManager.getManager().setSwipe(false);                
                
            }
            //Turn Tiger Right
            if( GameManager.getManager().getDirection().Peek()==GameManager.turnDirection.Right
                &&GameManager.getManager().getCanSwipe())
            {
                transform.Rotate(0, 90, 0);
                moveDirection = Quaternion.AngleAxis(90, Vector3.up) * moveDirection;
                Debug.Log("Tiger Turned Right");
                GameManager.getManager().setSwipe(false);
                
            }
        }
    }
    public void delayedRotateLeft()
    {
        
        
    }
    public void delayedRotateRight()
    {
       
    }
    
}
