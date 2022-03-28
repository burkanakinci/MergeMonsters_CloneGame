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
    [SerializeField] private GameObject gridCellPrefab;
    [SerializeField] private Transform gridCellParent;
    private List<GameObject> gridCellPool=new List<GameObject>();


    private void Awake()
    {
        instance = this;
    }

    public void SpawnGridCell(ref Vector3 _gridCellSpawnPos)
    {
        if (gridCellPool.Count > 0)
        {
            gridCellPool[0].SetActive(true);
            gridCellPool[0].transform.position = _gridCellSpawnPos;
            gridCellPool.Remove(gridCellPool[0]);
        }
        else
        {
            Instantiate(gridCellPrefab, _gridCellSpawnPos, Quaternion.identity, gridCellParent);
        }
    }
}
