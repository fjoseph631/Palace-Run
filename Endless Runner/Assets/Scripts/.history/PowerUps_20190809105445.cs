using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
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
                while (timePassed<duration)
                {
                    timePassed+=Time.deltaTime;
                    yield return null;
                }
                autorun=false;
                UIManager.Instance.SetStatus("Not Autorunning");        

            }
		}
		Destroy(this);
	}
}