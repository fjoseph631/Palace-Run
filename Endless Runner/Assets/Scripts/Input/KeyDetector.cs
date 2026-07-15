using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class KeyDetector : MonoBehaviour,IInputDetector {
    public InputDirection? DetectInputDirection()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            return InputDirection.Top;
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            return InputDirection.Bottom;
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            return InputDirection.Right;
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            return InputDirection.Left;
        else
            return null;
    }

   
		
	}

