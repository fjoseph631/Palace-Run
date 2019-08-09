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
                    Debug.Log("Tiger Can Turn ");
                }
            }
        }   
}