using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text ScoreText, StatusText;
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
    private float score = 0;
    //Restart Score
    public void ResetScore()
    {
        score = 0;
        //Update Score
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

    private void UpdateScoreText()
    {
        ScoreText.text = score.ToString();
    }

    public void SetStatus(string text)
    {
        StatusText.text = text;
    }

}

