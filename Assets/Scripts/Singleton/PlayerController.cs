using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("PlayerController").AddComponent<PlayerController>();
            }
            return instance;
        }
    }
    [SerializeField] private int currencyAmount;

    private void Awake()
    {
        instance = this;

        currencyAmount = SaveSystem.LoadCurrencyAmount();
    }
    public int GetCurrencyAmount()
    {
        return currencyAmount;
    }
    public void DecreaseCurrencyAmount(int _price)
    {
        currencyAmount -= _price;
    }
}
