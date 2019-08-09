using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swiper : MonoBehaviour {

    private void OnTriggerEnter(Collider swipe)
    {
       
        if(swipe.tag == "Player")
        {
            if(GameManager.getManager().getDirection().Count>0)
            {
                //Debug.Log(GameManager.getManager().getDirection().Peek());
                    GameManager.getManager().setTurn(true);
                    UIManager.Instance.SetStatus("Can Turn Now");
            }
            //Currently Autorunning
            
        }
    }
     void OnTriggerExit(Collider other)
    {
        GameManager.getManager().setTurn(false);
        UIManager.Instance.SetStatus("Cannot Turn Now");

        
    }
}
