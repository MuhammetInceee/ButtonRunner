using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElephantSDK;


public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;

    [HideInInspector] public bool isStarted = false;
    [HideInInspector] public bool isFinished = false;
    [HideInInspector] public bool isWin = false;
    [HideInInspector] public bool isFail = false;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
    }

    public void StartGame()
    {
        Debug.Log($"<color=#5fe769><b>Game is started!</b> </color>");
        isStarted = true;
        Elephant.LevelStarted(ManagerLevel.Instance.currentLevel);
        Actions.OnGameStarted?.Invoke();
    }

    public void FinishTheGame()
    {
        isFinished = true;
    }

    public void LevelComplete()
    {
        isWin = true;
        FinishTheGame();
        ConfettiManager.instance.Play();
        CanvasManager.instance.OpenFinishRect(true);
        
        Actions.OnGameCompleted?.Invoke();
    }

    public void LevelFail()
    {
        isFail = true;
        
        FinishTheGame();

        CanvasManager.instance.OpenFinishRect(false);
        
        Actions.OnGameFailed?.Invoke();
        
    }
    
    public void DestroyObj(object obj)
    {
        Destroy(obj as GameObject);
    }
}