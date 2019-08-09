using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text scoreText, statusText;
	//Create Single Instance of UI
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            //Destroy if instance already exists
            DestroyImmediate(this);
        }
    }

    //Single Instance of UI manager
    private static UIManager instance;
    public static UIManager Instance
    {
        //UIManager Getter 
        get
        {
            if (instance == null)
                instance = new UIManager();

            return instance;
        }
    }
    //Player Score
    private float score = 0;
    //Reset Score
    public void ResetScore()
    {
        //Reset Score to 0
        score = 0;
        UpdateScoreText();
    }
    //Set Score
    public void SetScore(float value)
    {
        score = value;
        UpdateScoreText();
    }
    //Score Increment Function
    public void IncreaseScore(float value)
    {
        score += value;
        UpdateScoreText();
    }
    //Update Score Display
    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void SetStatus(string text)
    {
        statusText.text = text;
    }

}

