using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerSwipe : MonoBehaviour
{
    private void OnTriggerEnter(Collider swipe)
    {
        if (GameManager.getManager().getDirection().Count > 0)
        {
            if (
                GameManager.getManager().getDirection().Peek() != GameManager.turnDirection.Straight
            )
            {
                if (swipe.tag == Constants.Tiger)
                {
                    GameManager.getManager().setSwipe(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Constants.Tiger && GameManager.getManager().getDirection().Count > 0)
        {
            var peek = GameManager.getManager().getDirection().Peek();
            if (peek != GameManager.turnDirection.Straight && !GameManager.getManager().getCanSwipe())
                GameManager.getManager().getDirection().Dequeue();
            else
                GameManager.getManager().setSwipe(false);
            Destroy(transform.root.gameObject, 5);
        }
    }
}
