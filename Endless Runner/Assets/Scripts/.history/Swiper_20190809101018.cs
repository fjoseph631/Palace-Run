using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swiper : MonoBehaviour {

    private void OnTriggerEnter(Collider swipe)
    {
       
        if(swipe.tag == "Player")
        {
            GameManager.getManager().setTurn(true);
            UIManager.Instance.SetStatus("Can Turn Now");
            //Currently Autorunning
            if(CharacterInput.autorun)
            {
                if(GameManager.getManager().getDirection().Count>0)
                {
                    if(GameManager.getManager().getDirection().Peek()
                    !=GameManager.turnDirection.Straight&&GameManager.getManager().getCanTurn())
                    {
                        Debug.Log(GameManager.getManager().getDirection().Peek());
                    }
                    //Check if can turn right or left
                    if(GameManager.getManager().getDirection().Peek()
                    ==GameManager.turnDirection.Right
                    &&GameManager.getManager().getCanTurn())
                    {
                        Debug.Log("Direction "+GameManager.getManager().getDirection().Peek());
                        transform.Rotate(0, 90, 0);
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
     void OnTriggerExit(Collider other)
    {
        //GameManager.turnDirection a;
        //Prevent multiple turns - may destroy trigger later.
        GameManager.getManager().setTurn(false);
        UIManager.Instance.SetStatus("Cannot Turn Now");

        
    }
}
