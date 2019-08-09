using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swiper : MonoBehaviour {

    private void OnTriggerEnter(Collider swipe)
    {
       
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
            //Debug.Log("Last Direction "+a);
        }
    }
}
