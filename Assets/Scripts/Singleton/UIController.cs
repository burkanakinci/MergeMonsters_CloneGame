using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    private static UIController instance;
    public static UIController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("UIController").AddComponent<UIController>();
            }
            return instance;
        }
    }
    [SerializeField] private GameObject failBackground;
    [SerializeField] private GameObject successBackground;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject startArea;
    [SerializeField] private TextMeshProUGUI currentAmountText;
    [SerializeField] private GameObject successArea;
    private void Awake()
    {
        instance=this;

        GameManager.Instance.levelStart+=ShowLevel;
        GameManager.Instance.levelStart+=ShowCurrentAmount;
        GameManager.Instance.levelStart+=ShowUIStart;
    }

    public void ShowUIStart()
    {
        failBackground.SetActive(false);
        successBackground.SetActive(false);
        levelText.gameObject.SetActive(true);
        restartButton.SetActive(false);
        startArea.SetActive(true);
        successArea.SetActive(false);
    }
    public void ShowUIInGame()
    {
        failBackground.SetActive(false);
        successBackground.SetActive(false);
        levelText.gameObject.SetActive(true);
        restartButton.SetActive(false);
        startArea.SetActive(false);
        successArea.SetActive(false);
    }
    public void ShowUISuccess()
    {
        failBackground.SetActive(false);
        successBackground.SetActive(true);
        levelText.gameObject.SetActive(false);
        restartButton.SetActive(false);
        startArea.SetActive(false);
        successArea.SetActive(true);
    }
    public void ShowUIFail()
    {
        failBackground.SetActive(true);
        successBackground.SetActive(false);
        levelText.gameObject.SetActive(false);
        restartButton.SetActive(true);
        startArea.SetActive(false);
        successArea.SetActive(false);
    }
    public void ShowLevel()
    {
        levelText.text = "Level." + GameManager.Instance.GetLevelNumber();
    }
    public void ShowCurrentAmount()
    {
        currentAmountText.text = "Remaining Amount : " + PlayerController.Instance.GetCurrencyAmount();
    }

    public void NextLevel(bool _isRestart)
    {
        if (!_isRestart)
        {
            GameManager.Instance.SetLevelData();
        }

        GameManager.Instance.NextLevelOnGameManager();
        
    }
    public void StartWarButton()
    {
        GameManager.Instance.SetGameState(GameState.Fight);

        ShowUIInGame();

        ObjectPool.Instance.OpponentStartWar();
    }
}
