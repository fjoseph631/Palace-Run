using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts


{
   
    public interface IInputDetector
{
    InputDirection? DetectInputDirection();
}

    public enum InputDirection
    {
        Left, Right, Top, Bottom
    }
}
