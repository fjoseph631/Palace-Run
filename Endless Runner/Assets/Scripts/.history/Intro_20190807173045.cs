using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

    public void LevelClick()
    {
        //Loading In Scene
        Debug.Log("Load");
        SceneManager.LoadScene("S1");
    }
}
