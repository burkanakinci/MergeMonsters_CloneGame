using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void ShowSuccessPanel()
    {

    }
    public void StartWarButton()
    {
        GameManager.Instance.SetGameState(GameState.Fight);

        ObjectPool.Instance.OpponentStartWar();
    }
}
