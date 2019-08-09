using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
	private void OnTriggerEnter(Collider col)
	{
		if(col.tag==Constants.PlayerTag)
		{
			CharacterInput.autorun=true;
		}
		Destroy(this);
	}
}