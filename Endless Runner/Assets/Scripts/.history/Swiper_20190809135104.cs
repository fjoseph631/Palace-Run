using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Swiper : MonoBehaviour {
    //Turn trigger hit by player
    private void OnTriggerEnter(Collider swipe)
    {
       //Player is now allowed to turn
        if(swipe.tag == "Player")
        {
           
            GameManager.getManager().setTurn(true);
            UIManager.Instance.SetStatus("Can Turn Now");
        
            //Currently Autorunning
            
        }
    }
     void OnTriggerExit(Collider other)
    {
        //Player can no longer turn now - destroy trigger
        GameManager.getManager().setTurn(false);
        UIManager.Instance.SetStatus("Cannot Turn Now");
        Debug.Break();
        Destroy(this.transform.root,7);
    }
}
