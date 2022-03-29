using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

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
        ObjectPool.Instance.CleanGridCell();
        ObjectPool.Instance.CleanOpponent();
    }
    private void SpawnSceneObject()
    {
        GetLevelData();

        SpawnOpponentsOnGameManager();
        SpawnGridCellOnGameManager();
    }
    private void SpawnOpponentsOnGameManager()
    {
        for (int i = currentLevelData.opponents.Count - 1; i >= 0; i--)
        {
            ObjectPool.Instance.ResetOpponentCounter();
            ObjectPool.Instance.SpawnOpponent(currentLevelData.opponentPoses[i], currentLevelData.opponents[i]);
        }
    }
    private void SpawnGridCellOnGameManager()
    {
        for (int j = currentLevelData.playerGridPoses.Count - 1; j >= 0; j--)
        {
            ObjectPool.Instance.SpawnGridCell(currentLevelData.playerGridPoses[j],
                 new Vector3(currentLevelData.gridCellXScale, 1f, currentLevelData.gridCellZScale));
        }
    }
    public void IncraceCurrencyAmountOnLevelStart(ref int _currencyAmount)
    {
        _currencyAmount += currentLevelData.levelCoinCount;
    }
    private void GetLevelData()
    {
        currentLevelData = null;

        tempLevelObject = (levelNumber % maxLevelObject) > 0 ? (levelNumber % maxLevelObject) : maxLevelObject;

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

