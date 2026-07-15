using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public void LevelClick(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void RotatedLevelClick()
    {
        LevelClick("S1");
    }
}
