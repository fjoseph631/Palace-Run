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
                if(swipe.tag==Constants.Tiger)
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
        //Prevent multiple turns
        if(other.tag==Constants.Tiger)
        {
            //Removing oldest queue element and storing whats left
            //GameManager.getManager().setDirection(GameManager.getManager().getDirection().Dequeue());
            if(GameManager.getManager().getDirection().Count>0)
            {
                //Tiger has turned - so set swipe to no and dequeue fro head
                GameManager.getManager().setSwipe(false);
                a=(GameManager.getManager().getDirection().Dequeue());
                //Object is no longer useful

            }
            Destroy(this.transform.root.gameObject);

        }
    }
}