using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class GameManager : MonoBehaviour {
    //Initializations
    //Single game manager
    public static GameManager instance;
    //Current game state
    private State state;
    //Can Tiger Turn?
    public bool canSwipe=false;
    //Can player Turn?
    public bool canTurn=false;
    private GameObject player;
    private bool activePlayer;
    //Storing Direction to help tiger and autorunning features
    private Queue<turnDirection> direction;
    //Enum to define 
    public enum turnDirection
    {
        Left,Right,Straight
        
    };
    //public GameState state;
    // Use this for initialization
    public void Awake ()
    {
        
        if (instance == null)
        {
            setManager(this);
        }
        else
        {
            Debug.Log("Manager already exists");
            DestroyImmediate(this);
        }
        //Initialize manager to start state
        setState(State.Start);
        setSwipe(false);
        //Get player avatar
        player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(true);
        setDirection(new Queue<turnDirection>());
        direction.Enqueue(turnDirection.Straight);
        //Debug.Log("Maanger Created");

    }
	
	// Death 
	public void Die()
    {
        
        setState(State.Dead);
        setSwipe(false);
	}
    /* Game State*/
    //Getters
    public State getState()
    {
        return state;
    }

    //Setters
    public void setState(State s)
    {
        state = s;
    }
    /*Can Swipe*/
    //Get
    public bool getCanSwipe()
    {
        return canSwipe;
    }
    //Set
    public void setSwipe(bool swipe)
    {
        canSwipe = swipe;
    }
    public bool getCanTurn()
    {
        return canTurn;
    }
    //Set
    public void setTurn(bool turn)
    {
        canTurn = turn;
    }
    /*Manager*/
    //Get
    public static GameManager getManager()
    {
        if (Instance == null)
            Instance = new GameManager();

        return Instance;
    }
    //Set
    public void setManager(GameManager manager)
    {
        if (manager == null)
        {
            Instance = new GameManager();
           
        }
        else
            Instance = this;
        Instance.setSwipe(false);
        Instance.state = State.Start;
    }
    protected GameManager()
    {
        setState(State.Start);
        setSwipe(false);
    }
    public void setDirection(Queue<turnDirection> s)
    {
        direction = s;
    }
    public Queue<turnDirection> getDirection()
    {
        return direction;
    }
}
