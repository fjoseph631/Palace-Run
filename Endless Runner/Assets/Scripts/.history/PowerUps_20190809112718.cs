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
            IEnumerator autorunTimer()
            {
                float timePassed=0;
                while (timePassed<CharacterInput.duration)
                {
                    timePassed+=Time.deltaTime;
					Debug.Log(timePassed);
                    yield return null;
                }
                CharacterInput.autorun=false;

            }
		}
		UIManager.Instance.SetStatus("Not Autorunning");        
		Destroy(this);
	}
}