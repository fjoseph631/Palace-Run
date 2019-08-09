using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticle : MonoBehaviour {

	// Use this for initialization

    void OnTriggerEnter(Collider col)
    {
        Debug.Break();
        if (col.gameObject.tag == Constants.PlayerTag)
        {

            //   GameManager.Die();
            GameManager.getManager().Die();
        }
        
        
    }
}
