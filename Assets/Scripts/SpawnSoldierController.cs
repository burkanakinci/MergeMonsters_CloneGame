using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSoldierController : MonoBehaviour
{

    public void SpawnSoldier(int _soldierLevel)
    {
        if (ObjectPool.Instance.HasEmptyGridCell() &&
            (ObjectPool.Instance.GetSoldierSpawnPriceOnObjectPool(_soldierLevel) < PlayerController.Instance.GetCurrencyAmount()))
        {
            PlayerController.Instance.DecreaseCurrencyAmount(ObjectPool.Instance.GetSoldierSpawnPriceOnObjectPool(_soldierLevel));

            ObjectPool.Instance.ResetSoldierCounter();

            ObjectPool.Instance.SpawnSoldier(ObjectPool.Instance.GetEmptyGridPos(), _soldierLevel);
        }
    }
}
