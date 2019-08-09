using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
//Adds bonus to score
public class AutoRun : MonoBehaviour {
    private float time=0f;
    private float start=0f;
    
    private GameObject player; 
    public void OnTriggerEnter(Collider col)
    {
        CharacterInput.autorun=true;
        Debug.Break();
    }
}