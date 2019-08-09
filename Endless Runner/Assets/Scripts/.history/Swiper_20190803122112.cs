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
        //GameManager.turnDirection a;
        //Prevent multiple turns - may destroy trigger later.
        GameManager.getManager().setTurn(false);
        
    }
}
