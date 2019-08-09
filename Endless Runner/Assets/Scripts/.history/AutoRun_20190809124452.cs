using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRun : MonoBehaviour {

    private void OnTriggerEnter(Collider swipe)
    {
       //Hit autorun turn trigger
        if(swipe.tag == "Player")
        {
            //Currently Autorunning
            if(CharacterInput.autorun)
            {
                //Queue not-empty check
                if(GameManager.getManager().getDirection().Count>0)
                {
                    if(GameManager.getManager().getDirection().Peek()
                    !=GameManager.turnDirection.Straight&&GameManager.getManager().getCanTurn())
                    {
                       // Debug.Log(GameManager.getManager().getDirection().Peek());
                    }
                    //Check if can turn right or left
                    if(GameManager.getManager().getDirection().Peek()
                    ==GameManager.turnDirection.Right
                    &&GameManager.getManager().getCanTurn())
                    {
                        swipe.transform.Rotate(0, 90, 0);
                        CameraController.rotation.y+=90;
                        CharacterInput.moveDirection= Quaternion.AngleAxis(90, Vector3.up) * CharacterInput.moveDirection;
                        GameManager.getManager().setTurn(false);

                    }
                    if(GameManager.getManager().getDirection().Peek()==GameManager.turnDirection.Left
                    &&GameManager.getManager().getCanTurn())
                    {
                        CameraController.rotation.y-=90;
                        swipe.transform.Rotate(0, -90, 0);
                        CharacterInput.moveDirection= Quaternion.AngleAxis(-90, Vector3.up) * CharacterInput.moveDirection;
                        GameManager.getManager().setTurn(false);
                        }
                }
            }//Debug.Log("Player Can Turn");

        }
    }
    private void OnTriggerExit(Collider swipe)
    {
        Destroy(this);
    }
}