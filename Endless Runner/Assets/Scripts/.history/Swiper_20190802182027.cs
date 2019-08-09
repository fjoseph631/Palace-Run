using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swiper : MonoBehaviour {

    private void OnTriggerEnter(Collider swipe)
    {
        if(GameManager.getManager().getDirection().Count>0)
        {
            if(GameManager.getManager().getDirection().Peek()!=GameManager.turnDirection.Straight)
            {
                if(swipe.tag =="GameController")
                {
                    GameManager.getManager().setSwipe(true);
                    Debug.Log("Tiger Can Turn ");
                }
            }
        }   
        if(swipe.tag == "Player")
            GameManager.getManager().setTurn(true);
    }
    private void OnTriggerExit(Collider other)
    {
        GameManager.turnDirection a;
        //Prevent multiple turns - may destroy trigger later.
        GameManager.getManager().setTurn(false);
        GameManager.getManager().setSwipe(false);
        //Removing oldest queue element and storing whats left
        //GameManager.getManager().setDirection(GameManager.getManager().getDirection().Dequeue());
        if(GameManager.getManager().getDirection().Count>0)
        {
            a=(GameManager.getManager().getDirection().Dequeue());
            Debug.Log("Last Direction "+a);
        }
    }
}
