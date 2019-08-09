using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//All Constants
public class Constants : MonoBehaviour {

    public static readonly string PlayerTag = "Player";
    public static readonly string Tiger ="Tiger";
    //Animation
    public static readonly string AnimationRun = "run";
    public static readonly string AnimationDoubleJump = "flip";
    public static readonly string AnimationLeftTurn = "left turn";
    public static readonly string AnimationRightTurn = "right turn";
    public static readonly string AnimationHit = "hit";
    //Parameters
    public static readonly string ParamJump = "Jump";
    public static readonly string ParamDoubleJump = "DoubleJump";
    //public static readonly string AnimationRightTurn = "RightTurn";
    public static readonly string ParamTurning= "Turning";
    public static readonly string ParamTurnDirection = "TurnDirection";
    public static readonly string ParamDead = "Dead";
    public static readonly string ParamStarted = "Started";
    public static readonly string ParamStart = "Start";
    public static readonly string ParamGrounded = "OnGround";
    public static readonly string ParamJumping = "Jumping";
    public static readonly string ParamDescending = "Descending";
    
    //UI Statuses
    public static readonly string StatusTapToStart = "Tap to start";
    public static readonly string StatusDead = "Dead. Tap to start";
    //Other
    public static readonly string GameManager = "GameManager";
    public static readonly string WallTag = "Wall";
    public static readonly string GameController="GameController";
}
