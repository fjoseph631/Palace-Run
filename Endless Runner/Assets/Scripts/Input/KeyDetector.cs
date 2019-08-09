using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class KeyDetector : MonoBehaviour,IInputDetector {
    public InputDirection? DetectInputDirection()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.W))
            return InputDirection.Top;
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            return InputDirection.Bottom;
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            return InputDirection.Right;
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            return InputDirection.Left;
        else
            return null;
    }

   
		
	}

