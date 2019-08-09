using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroyer : MonoBehaviour {

    public float lifeTime = 5f;
    //int count = 0;
    static float time;
    // Use this for initialization
    void Start()
    {
        time = Time.deltaTime;
        Invoke("DestroyObject", lifeTime);

        
        
    }

    
    void DestroyObject()
    {
        //State is playing or stumbled
        if (GameManager.getManager().getState() != State.Dead&&GameManager.getManager().getState()!=State.Start)
        {
            //Debug.Log("Destroy");
            Destroy(gameObject,5);
        }
            
    }
}
