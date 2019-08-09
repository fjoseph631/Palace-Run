using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRun : MonoBehaviour {

    private void OnTriggerEnter(Collider auto)
    {
       //Hit autorun turn trigger
        if(auto.tag == Constants.PlayerTag)
        {
            //Currently Autorunning
            if(CharacterInput.autorun)
            {
                //Queue not-empty check
                if(GameManager.getManager().getDirection().Count>0)
                {
                    //Check if can turn right or left
                    if(GameManager.getManager().getDirection().Peek()
                    ==GameManager.turnDirection.Right
                    &&GameManager.getManager().getCanTurn())
                    {
                        //Turn player and camera 90 degrees
                        auto.transform.Rotate(0, 90, 0);
                        CameraController.rotation.y+=90;
                        //Update player moveDirection and prevent further turns
                        CharacterInput.moveDirection= Quaternion.AngleAxis(90, Vector3.up) * CharacterInput.moveDirection;
                        GameManager.getManager().setTurn(false);
                        //Destroy Trigger
                        Destroy(this);

                    }
                    if(GameManager.getManager().getDirection().Peek()==GameManager.turnDirection.Left
                    &&GameManager.getManager().getCanTurn())
                    {
                        //Turn player and camera 90 degrees
                        auto.transform.Rotate(0, -90, 0);
                        CameraController.rotation.y-=90;
                        //Update player moveDirection and prevent further turns
                        CharacterInput.moveDirection= Quaternion.AngleAxis(-90, Vector3.up) * CharacterInput.moveDirection;
                        GameManager.getManager().setTurn(false);
                        //Destroy Trigger
                        Destroy(this);
                    }
                }
            }

        }
    }
    private void OnTriggerExit(Collider swipe)
    {
        Destroy(this);
    }
}