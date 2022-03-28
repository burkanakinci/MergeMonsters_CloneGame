using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState
{
    Start,
    Fight,
    Finish
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return instance;
        }
    }
    [SerializeField] private bool fpsLock = true;
    private LevelData currentLevelData;
    private int levelNumber;
    private GameState gameState;
    public event Action levelStart;
    [SerializeField] private int maxLevelObject;
    private int tempLevelObject;
    private void Awake()
    {

        levelStart += CleanSceneObject;
        levelStart += SpawnSceneObject;

        instance = this;

        if (fpsLock)
        {
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 60;
        }
    }
    private void Start()
    {

        NextLevelOnGameManager();
    }

    public void NextLevelOnGameManager()
    {
        gameState = GameState.Start;

        levelNumber = SaveSystem.LoadLastLevelNum();

        levelStart?.Invoke();
    }

    private void CleanSceneObject()
    {

    }
    private void SpawnSceneObject()
    {
        GetLevelData();

        PlayerController.Instance.SetNeededStack(currentLevelData.NeededStackCount);
        upgradePrice = currentLevelData.StackUpgradePrice;

        for (int i = (currentLevelData.GetDiamondPositionsCount) - 1; i >= 0; i--)
        {
            ObjectPool.Instance.SpawnDiamond(currentLevelData.DiamondPosition(i));
        }
        for (int j = (currentLevelData.GetGoldPositionsCount) - 1; j >= 0; j--)
        {
            ObjectPool.Instance.SpawnMoney(currentLevelData.GoldPosition(j));
        }
        for (int k = (currentLevelData.GetObstaclePositionsCount) - 1; k >= 0; k--)
        {
            ObjectPool.Instance.SpawnObstacle(currentLevelData.ObstaclePosition(k));
        }
        for (int l = (currentLevelData.PlatformCount) - 1; l >= 0; l--)
        {
            ObjectPool.Instance.SpawnPlatform();
        }

        finishTrigger.position = ((currentLevelData.PlatformCount) - 1) * (Vector3.forward * 10f);
        ObjectPool.Instance.SetFinishCollectionAreaPosition(finishTrigger.position);
    }
    private void GetLevelData()
    {
        currentLevelData = null;

        tempLevelObject = (levelNumber % maxLevelObject) > 0 ?
            (levelNumber % maxLevelObject) : maxLevelObject;

        currentLevelData = Resources.Load<LevelData>("LevelScriptableObjects/Level" + tempLevelObject);
    }
    public void SetLevelData()
    {
        SaveSystem.SaveLastLevelNum(levelNumber + 1);
    }
    public int GetLevelNumber()
    {
        return levelNumber;
    }
    public GameState GetGameState()
    {
        return gameState;
    }
    public void SetGameState(GameState _gameState)
    {
        gameState = _gameState;
    }

    private void OnDisable()
    {
        levelStart -= CleanSceneObject;
        levelStart -= SpawnSceneObject;
    }
}

