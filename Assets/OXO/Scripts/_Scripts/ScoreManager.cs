using System;
using System.Collections;
using System.Collections.Generic;
using MuhammetInce.DesignPattern.Singleton;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : LazySingleton<ScoreManager>
{
    public int currentScore;

    private void Start()
    {
        currentScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void SetNewHighScore(int newHighScore)
    {
        currentScore += (newHighScore - currentScore);
    }

    private void OnDisable()
    {
        SetScore();
    }

    private void OnApplicationQuit()
    {
        SetScore();
    }

    //Oyunu Diger Levele Gecirirken de Save at unutma
    private void SetScore()
    {
        PlayerPrefs.SetInt("HighScore", currentScore);
    }
}


