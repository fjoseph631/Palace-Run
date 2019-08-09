using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TigerSwipe : MonoBehaviour {
    private void OnTriggerEnter(Collider swipe)
    { 
        if(GameManager.getManager().getDirection().Count>0)
        {
            if(GameManager.getManager().getDirection().Peek()!=GameManager.turnDirection.Straight)
            {
                if(swipe.tag =="GameController")
                {
                    GameManager.getManager().setSwipe(true);
                    //Debug.Log("Tiger Can Turn "+ GameManager.getManager().getDirection().Peek());
                    
                }
            }
        }   
    }
    private void OnTriggerExit(Collider other)
    {
        GameManager.turnDirection a;
        //Prevent multiple turns - may destroy trigger later.
        if(other.tag=="GameController")
        {
            //Removing oldest queue element and storing whats left
            //GameManager.getManager().setDirection(GameManager.getManager().getDirection().Dequeue());
            if(GameManager.getManager().getDirection().Count>0)
            {
                // Debug.Log("Old Count "+GameManager.getManager().getDirection().Count);
                // Debug.Log("Tiger Cannot Turn "+ GameManager.getManager().getDirection().Peek());
                GameManager.getManager().setSwipe(false);
                a=(GameManager.getManager().getDirection().Dequeue());
                Debug.Log("Last Direction "+a);
                // Debug.Log("New Count "+GameManager.getManager().getDirection().Count);
                // if(GameManager.getManager().getDirection().Count>0)
                //     Debug.Log("Next Turn "+ GameManager.getManager().getDirection().Peek());
                // Debug.Break();
                Destroy(this);

            }
        }
    }
}