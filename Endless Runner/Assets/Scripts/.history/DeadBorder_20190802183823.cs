using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class DeadBorder : MonoBehaviour {
    //Kills Player on Contact
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == Constants.PlayerTag)
        {
            //Call Death Function to end Game
            GameManager.getManager().Die();
        
        }
    }
    
}
