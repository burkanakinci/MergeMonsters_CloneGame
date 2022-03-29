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
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject startArea;
    [SerializeField] private GameObject startFightButton;
    [SerializeField] private GameObject currentAmount;
    [SerializeField] private TextMeshPro currentAmountText;
    [SerializeField] private GameObject successButton;

    private void ShowSuccessPanel()
    {

    }
    public void ShowFailPanel()
    {

    }
    public void ShowUIStart()
    {

    }
    public void ShowUIInGame()
    {

    }
    public void ShowUISuccess()
    {

    }
    public void ShowLevel()
    {

    }
    public void ShowCurrentAmount()
    {

    }
    public void StartWarButton()
    {
        GameManager.Instance.SetGameState(GameState.Fight);

        ObjectPool.Instance.OpponentStartWar();
    }
}
