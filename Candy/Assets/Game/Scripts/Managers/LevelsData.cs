using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WrapLevels
{
    public LevelInfo[] levelInfos;
}

[System.Serializable]
public class LevelInfo
{
    public int numberLvl;
    public float timeOnLevel;
    public int targetScoreLevel;
    public StateLevel state;
    public float timeRecord;
    public int rewardLvl;
}

public class LevelsData : MonoBehaviour
{
    public static LevelsData Instance;

    [SerializeField] private WrapLevels wrapLevels;
    [SerializeField] private LevelsView levelsView;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        string stJs = PlayerPrefs.GetString("Levels");
        if (stJs != "")
        {
            wrapLevels = JsonUtility.FromJson<WrapLevels>(stJs);
        }

        levelsView.InitButtons(wrapLevels);
    }

    public void SaveData()
    {
        PlayerPrefs.SetString("Levels", JsonUtility.ToJson(wrapLevels));
    }

    public LevelInfo GetLevelData(int number)
    {
        LevelInfo levelInfo = wrapLevels.levelInfos[number];

        return levelInfo;
    }

    public void LevelSuccess(int number, float timeResult)
    {
        if (wrapLevels.levelInfos[number].timeRecord < timeResult)
        {
            wrapLevels.levelInfos[number].timeRecord = timeResult;
            SaveData();
        }
        if (number < wrapLevels.levelInfos.Length - 1)
        {
            wrapLevels.levelInfos[number + 1].state = StateLevel.Open;
            SaveData();
        }
    }
}
