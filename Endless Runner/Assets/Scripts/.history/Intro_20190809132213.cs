﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

    public void LevelClick(string level)
    {
        //Loading In Scene on Click
        SceneManger.LoadScene(level);
    }
}
