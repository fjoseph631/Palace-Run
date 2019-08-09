using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    //static bool turned;

    Vector3 offset = new Vector3(0, 4, -4);
    public Vector3 rotation = new Vector3(22.5f, 0.0f, 0.0f);
	// Update is called once per frame
	void Update () {
        //player = GameObject.FindWithTag(Constants.PlayerTag);
      
        //player.transform.rotation.eulerAngles.y;
        transform.SetPositionAndRotation(player.transform.position + offset, Quaternion.Euler(rotation));
       
	}
}
