using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticle : MonoBehaviour {

	// Use this for initialization

    void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.tag == Constants.PlayerTag)
        {

            //Character is killed unless autorunning
            if(CharacterInput.autorun)
            {
                Destroy(this);
            }
            GameManager.getManager().Die();
        }
        
        
    }
}
