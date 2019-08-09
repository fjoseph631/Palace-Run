using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    //static bool turned;

    Vector3 offset = new Vector3(0, 4, -4);
    Vector3 rotation = new Vector3(22.5f, 0.0f, 0.0f);
	// Update is called once per frame
	void Update () {
        //player = GameObject.FindWithTag(Constants.PlayerTag);
        rotation.y = player.transform.eulerAngles.y;
       // turned = false;
        //Debug.Log(rotation.y);
       /*  if(rotation.y>360)
        {
            rotation.y -= 360;
        }
        if(rotation.y<0)
        {
            rotation.y+= 360;
        }
        if(Mathf.Abs((int)rotation.y)==270|| Mathf.Abs((int)rotation.y) ==-90)
        {
            turned = true;
            offset.z = 0;
            offset.x = 4;
        }
        else if(Mathf.Abs((int)rotation.y) == 90)
        {
            turned = true;
            offset.z = 0;
            offset.x = -4;
        }
        else if(Mathf.Abs((int)rotation.y) == 180)
        {
            offset.x = 0;
            offset.z = -4;
        }*/
        rotation.y = player.transform.rotation.eulerAngles.y;
        transform.SetPositionAndRotation(player.transform.position + offset, Quaternion.Euler(rotation));
       
	}
}
