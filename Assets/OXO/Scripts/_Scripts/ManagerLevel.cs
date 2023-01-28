using System.Collections.Generic;
using MuhammetInce.DesignPattern.Singleton;
using MuhammetInce.HelperUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerLevel : MonoBehaviour
{
    public int currentLevel;
    public List<GameObject> levelPrefabList;
    public static ManagerLevel Instance;

    private void Awake()
    {
        Instance = this;
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);

        if (currentLevel >= levelPrefabList.Count - 1)
        {
            currentLevel = 0;
            levelPrefabList.ShuffleList();
        }
        GameObject level = Instantiate(levelPrefabList[currentLevel]);
        if (!level.activeInHierarchy)
        {
            level.SetActive(true);
        }


    }


    public void UpgradeLevel()
    {
        currentLevel++;
        if (currentLevel >= levelPrefabList.Count - 1)
        {
            currentLevel = 0;
            levelPrefabList.ShuffleList();
        }
        LevelPrefsSetter(currentLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LevelPrefsSetter(int level)
    {
        PlayerPrefs.SetInt("CurrentLevel", level);

    }
}
