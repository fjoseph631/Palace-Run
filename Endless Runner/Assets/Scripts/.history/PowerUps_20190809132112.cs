﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {
	//Autorun PowerUp Hit
    private void OnTriggerEnter(Collider col)
	{
		if(col.tag==Constants.PlayerTag)
		{
            //Update Status
			CharacterInput.autorun=true;
			Debug.Log("autorunning");
            UIManager.Instance.SetStatus("Autorunning");
            //Call Time
            StartCoroutine(autorunTimer());
           
		}
		UIManager.Instance.SetStatus("No Longer Autorunning");        
		Destroy(this);
	}
    IEnumerator autorunTimer()
    {
        float timePassed=0;
        //Remained autorunning for given duration using coroutine
        while (timePassed<CharacterInput.duration)
        {
            UIManager.Instance.SetStatus("Still Autorunning");
            timePassed+=Time.deltaTime;
            yield return new WaitForSeconds(CharacterInput.duration);
            Debug.Log(timePassed-CharacterInput.duration);
        }
        CharacterInput.autorun=false;

    }
}