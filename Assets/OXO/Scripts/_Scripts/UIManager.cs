using System;
using ElephantSDK;
using MuhammetInce.DesignPattern.Singleton;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class UIManager : LazySingleton<UIManager>
{
    private GameObject level;
    
    [SerializeField] private GameObject inGameCanvas;
    [SerializeField] private GameObject levelEndCanvas;
    [SerializeField] private GameObject textParent;
    [SerializeField] private ParticleSystem confettiParticle;
    [SerializeField] private ManagerLevel managerLevel;
    [SerializeField] private TextMeshProUGUI levelText;
    
    private void Start()
    {
        managerLevel = GameObject.Find("LevelManager").GetComponent<ManagerLevel>();
        level = GameObject.FindWithTag("Level");
    }

    private void Update()
    {
        levelText.text = $"Level {level.name[7]}";
    }

    public void RestartButton()
    {
        managerLevel.UpgradeLevel();
        Elephant.LevelCompleted(LevelManager.instance.level);

    }

    [Button]
    public void LevelEndVisualiser(bool isWon ,string str)
    {
        inGameCanvas.SetActive(false);
        levelEndCanvas.SetActive(true);
        if (isWon)
        {
            confettiParticle.gameObject.SetActive(true);
            confettiParticle.Play();
        }
        ChildFounder(textParent, str);
    }

    private void ChildFounder(GameObject gO, string str)
    {
        for (int i = 0; i < gO.transform.childCount; i++)
        {
            if (gO.transform.GetChild(i).name == str)
            {
                gO.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
    
    
}
