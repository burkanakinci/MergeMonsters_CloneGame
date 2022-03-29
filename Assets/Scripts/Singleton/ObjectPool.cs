using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool instance;
    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("ObjectPool").AddComponent<ObjectPool>();
            }
            return instance;
        }
    }
    [SerializeField] private GridController gridCellPrefab;
    [SerializeField] private Transform gridCellParent;
    private List<GridController> gridCellPool = new List<GridController>();
    private List<GridController> gridCellOnScene = new List<GridController>();
    [SerializeField] private OpponentController[] opponentPrefabs;
    [SerializeField] private Transform opponentParentActive, opponentParentInactive;
    private List<OpponentController> opponentPool = new List<OpponentController>();
    private List<OpponentController> opponentInScene = new List<OpponentController>();
    [SerializeField] private SoldierController[] soldierPrefabs;
    [SerializeField] private Transform soldierParentActive, soldierParentInactive;
    private List<SoldierController> soldierPool = new List<SoldierController>();
    private int opponentCounter, soldierCounter;
    private GridController tempGridOnSpawnSoldier;
    private void Awake()
    {
        instance = this;
    }

    public void SpawnGridCell(Vector3 _gridCellSpawnPos, Vector3 _gridCellScale)
    {
        if (gridCellPool.Count > 0)
        {
            gridCellPool[0].gameObject.SetActive(true);
            gridCellPool[0].transform.position = _gridCellSpawnPos;
            gridCellPool[0].transform.localScale = _gridCellScale;
            gridCellOnScene.Add(gridCellPool[0]);
            gridCellPool.Remove(gridCellPool[0]);
        }
        else
        {
            gridCellPrefab.transform.localScale = _gridCellScale;
            gridCellOnScene.Add(Instantiate(gridCellPrefab, _gridCellSpawnPos, Quaternion.identity, gridCellParent));
        }
    }
    public void SpawnOpponent(Vector3 _spawnPos, OpponentController _opponent)
    {
        if (opponentPool.Count > 0)
        {
            for (int i = opponentPool.Count - 1; i >= 0; i--)
            {
                if (_opponent.GetOpponentLevel() == opponentPool[i].GetOpponentLevel())
                {
                    opponentPool[i].gameObject.SetActive(true);
                    opponentPool[i].transform.position = _spawnPos - (Vector3.right * 0.6f) + (Vector3.right * 1.2f * opponentCounter);
                    opponentPool[i].transform.eulerAngles = new Vector3(0f, 180f, 0f);
                    opponentPool[i].transform.SetParent(opponentParentActive);
                    opponentInScene.Add(opponentPool[i]);
                    opponentPool.Remove(opponentPool[i]);

                    opponentCounter++;

                    if (opponentCounter < 2)
                    {
                        SpawnOpponent(_spawnPos, _opponent);
                    }
                    return;
                }
            }
        }

        for (int j = opponentPrefabs.Length - 1; j >= 0; j--)
        {
            if (_opponent.GetOpponentLevel() == opponentPrefabs[j].GetOpponentLevel())
            {
                opponentInScene.Add(Instantiate(opponentPrefabs[j],
                        (_spawnPos - (Vector3.right * 0.6f) + (Vector3.right * 1.2f * opponentCounter)),
                        Quaternion.Euler(0f, 180f, 0f),
                        opponentParentActive));

                opponentCounter++;

                if (opponentCounter < 2)
                {
                    SpawnOpponent(_spawnPos, _opponent);
                }

                return;
            }
        }
    }
    public void SpawnSoldier(Vector3 _spawnPos, int _soldierLevel)
    {
        if (soldierPool.Count > 0)
        {
            for (int i = soldierPool.Count - 1; i >= 0; i--)
            {
                if (_soldierLevel == soldierPool[i].GetSoldierLevel())
                {
                    soldierPool[i].gameObject.SetActive(true);
                    soldierPool[i].transform.position = _spawnPos - (Vector3.right * 0.6f) + (Vector3.right * 1.2f * soldierCounter);
                    soldierPool[i].transform.eulerAngles = new Vector3(0f, 0f, 0f);
                    soldierPool[i].transform.SetParent(soldierParentActive);
                    tempGridOnSpawnSoldier.soldiersOnGrid.Add(soldierPool[i]);
                    soldierPool.Remove(soldierPool[i]);

                    soldierCounter++;

                    if (soldierCounter < 2)
                    {
                        SpawnSoldier(_spawnPos, _soldierLevel);
                    }
                    return;
                }
            }
        }

        for (int j = soldierPrefabs.Length - 1; j >= 0; j--)
        {
            if (_soldierLevel == soldierPrefabs[j].GetSoldierLevel())
            {
                tempGridOnSpawnSoldier.soldiersOnGrid.Add(Instantiate(soldierPrefabs[j],
                        (_spawnPos - (Vector3.right * 0.6f) + (Vector3.right * 1.2f * soldierCounter)),
                        Quaternion.Euler(0f, 0f, 0f),
                        soldierParentActive));

                soldierCounter++;

                if (soldierCounter < 2)
                {
                    SpawnSoldier(_spawnPos, _soldierLevel);
                }

                return;
            }
        }
    }
    public void CleanGridCell()
    {

    }
    public void CleanOpponent()
    {

    }
    public void AddSoldierPool(SoldierController _soldier)
    {
        _soldier.transform.SetParent(soldierParentInactive);
        soldierPool.Add(_soldier);
        _soldier.gameObject.SetActive(false);
    }
    public void ResetOpponentCounter()
    {
        opponentCounter = 0;
    }
    public void ResetSoldierCounter()
    {
        soldierCounter = 0;
    }
    public int GetGridCellOnSceneCount()
    {
        return gridCellOnScene.Count;
    }
    public Vector3 GetGridPos(ref GridController _grid)
    {
        tempGridOnSpawnSoldier = _grid;

        return _grid.transform.position + (Vector3.right * _grid.transform.localScale.x + Vector3.forward * _grid.transform.localScale.z) / 2f;
    }
    public Vector3 GetEmptyGridPos()
    {
        for (int i = gridCellOnScene.Count - 1; i >= 0; i--)
        {
            if (gridCellOnScene[i].isEmpty)
            {
                gridCellOnScene[i].isEmpty = false;
                tempGridOnSpawnSoldier = gridCellOnScene[i];
                return gridCellOnScene[i].transform.position +
                    (Vector3.right * gridCellOnScene[i].transform.localScale.x / 2f) +
                    (Vector3.forward * gridCellOnScene[i].transform.localScale.z / 2f);
            }
        }
        return Vector3.zero;
    }
    public bool HasEmptyGridCell()
    {
        for (int i = gridCellOnScene.Count - 1; i >= 0; i--)
        {
            if (gridCellOnScene[i].isEmpty)
            {
                return true;
            }
        }
        return false;
    }
    public int GetSoldierSpawnPriceOnObjectPool(int _soldierLevel)
    {
        for (int i = soldierPrefabs.Length - 1; i >= 0; i--)
        {
            if (_soldierLevel == soldierPrefabs[i].GetSoldierLevel())
            {
                return soldierPrefabs[i].GetSoldierSpawnPrice();
            }
        }
        return -1;
    }

    public void FindTargetSoldier(ref Transform _target, Vector3 _pos)
    {
        _target = opponentParentActive.GetChild(0);

        for (int i = opponentParentActive.childCount - 1; i >= 0; i--)
        {
            if (Vector3.Distance(opponentParentActive.GetChild(i).position, _pos) <
                Vector3.Distance(_target.position, _pos))
            {
                _target = opponentParentActive.GetChild(i);
            }
        }
    }
    public void FindTargetOpponent(ref Transform _target, Vector3 _pos)
    {
        _target = soldierParentActive.GetChild(0);

        for (int i = soldierParentActive.childCount - 1; i >= 0; i--)
        {
            if (Vector3.Distance(soldierParentActive.GetChild(i).position, _pos) <
                Vector3.Distance(_target.position, _pos))
            {
                _target = soldierParentActive.GetChild(i);
            }
        }
    }
    public GridController GetClickedGrid(Transform _clickedGrid)
    {
        for (int i = gridCellOnScene.Count - 1; i >= 0; i--)
        {
            if (gridCellOnScene[i].transform == _clickedGrid)
            {
                return gridCellOnScene[i];
            }
        }
        return null;
    }
    public Transform GetSoldierParentActive()
    {
        return soldierParentActive;
    }
    public Transform GetSoldierParentInactive()
    {
        return soldierParentInactive;
    }
    public void OpponentStartWar()
    {
        for (int i = opponentInScene.Count - 1; i >= 0; i--)
        {
            opponentInScene[i].FindTarget();
        }
    }
}
