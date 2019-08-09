using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;


public class SwipeDetector : MonoBehaviour,IInputDetector {

    public enum State
    {
        swipeStarted,swipeNotStarted
    }
    private State state = State.swipeNotStarted;
    private Vector2 startPt;
    private System.DateTime swipeStartTime;
    private System.TimeSpan swipeDuration;
    private System.TimeSpan maxSwipeDuration=System.TimeSpan.FromSeconds(1);
    private System.TimeSpan minSwipeDuration=System.TimeSpan.FromMilliseconds(100);

    public InputDirection? DetectInputDirection()
    {
        if(state==State.swipeNotStarted)
        {
            if(Input.GetMouseButtonDown(0))
            {
                state = State.swipeStarted;
                startPt = Input.mousePosition;
                swipeStartTime = System.DateTime.Now;
            }
        }
        else if(state==State.swipeStarted)
        {
            if(Input.GetMouseButtonUp(0))
            {
                swipeDuration = System.DateTime.Now - swipeStartTime;
                //Valid check
                if(swipeDuration<=maxSwipeDuration && (swipeDuration>=minSwipeDuration))
                {
                    Vector2 position = Input.mousePosition;
                    Vector2 dVector = position - startPt;
                    float angle = Vector2.Angle(dVector, Vector2.right);
                    Vector3 cross = Vector3.Cross(dVector, Vector2.right);
                    if(cross.z<0)
                    {
                        angle = 360 - angle;
                    }
                    //Rechange state
                    state = State.swipeNotStarted;
                    if (angle >= 315 && angle<=360 || angle >= 0 && angle <= 45)
                        return InputDirection.Right;
                    if (angle <= 225 && angle >= 135)
                        return InputDirection.Left;
                    if (angle <= 135 && angle >= 45)
                        return InputDirection.Left;
                    else
                        return InputDirection.Bottom;
                }
                else
                {
                    return null;
                }
            }
        }
        return null;
       
    }
}
