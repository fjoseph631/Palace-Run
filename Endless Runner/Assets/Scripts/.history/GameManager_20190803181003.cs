using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class GameManager : MonoBehaviour {
    //Initializations
    public static GameManager Instance;
    private State state;
    public bool canSwipe=false;
    public bool canTurn=false;
    private GameObject player;
    private bool activePlayer;
    private Queue<turnDirection> direction;
    public enum turnDirection
    {
        Left,Right,Straight
        
    };
    //public GameState state;
    // Use this for initialization
    public void Awake ()
    {
        
        if (Instance == null)
        {
            setManager(this);
        }
        else
            DestroyImmediate(this);
        setState(State.Start);
        setSwipe(false);
        if (Instance == null)
        {
            Debug.Log("Manager not set properly");
            setManager(this);
        }
        player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(true);
        setDirection(new Queue<turnDirection>());
        GameManager.getManager().getDirection().Enqueue(GameManager.turnDirection.Straight);
        activePlayer = true;
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
    public GameObject getPlayer()
    {
        return player;
    }
    public bool getActivePlayer()
    {
        return activePlayer;
    }
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
