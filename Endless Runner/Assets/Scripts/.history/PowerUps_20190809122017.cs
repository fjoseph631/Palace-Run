using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {
	private void OnTriggerEnter(Collider col)
	{
		if(col.tag==Constants.PlayerTag)
		{
			CharacterInput.autorun=true;
			Debug.Log("autorunning");
            UIManager.Instance.SetStatus("Autorunning");
            StartCoroutine(autorunTimer());
           
		}
		UIManager.Instance.SetStatus("Not Longer Autorunning");        
		Destroy(this);
	}
    IEnumerator autorunTimer()
    {
        float timePassed=0;
        while (timePassed<CharacterInput.duration)
        {
            UIManager.Instance.SetStatus("Autorunning");
            timePassed+=Time.deltaTime;
            yield return new WaitForSeconds(CharacterInput.duration);
            Debug.Log(timePassed-CharacterInput.duration);
        }
        CharacterInput.autorun=false;

    }
}