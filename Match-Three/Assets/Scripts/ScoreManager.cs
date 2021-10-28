﻿using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    
    #region Singleton
    private static ScoreManager _instance = null;

    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScoreManager>();
                if (_instance == null)
                {
                    Debug.LogError("Fatal Error: ScoreManager is not Found");
                }
            }
            return _instance;
        }
    }
    #endregion

    private static int highScore;
    private int currentScore;

    public int tileRatio;
    public int comboRatio;

    public int HighScore { get { return highScore; } }
    public int CurrentScore { get { return currentScore; } }

    

    private void Start()
    {
        ResetCurrentScore();
    }

    public void ResetCurrentScore()
    {
        currentScore = 0;
    }

    public void IncrementCurrentScore(int tileCount, int comboCount)
    {
        currentScore += (tileCount * tileRatio) * (comboCount * comboRatio);
        SoundManager.Instance.PlayScore(comboCount > 1);
    }

    public void SetHighScore()
    {
        //highScore = currentScore;
        highScore = Mathf.Max(currentScore, highScore);
    }
}
