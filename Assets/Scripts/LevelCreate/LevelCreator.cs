//#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private GameObject gridCellPrefab;
    private GameObject tempGridCell;
    [SerializeField] private OpponentController[] opponents;
    [HideInInspector] public int gridCellXCount = 1, gridCellZCount = 1;

    [SerializeField]
    private Transform gridCellBottomLeft,
        gridCellBottomRight,
        gridCellTopLeft,
        gridCellTopRight,
        gridCellOpponentSpawnPos,
        gridCellParentActive, gridCellParentInactive, gridCellParentOpponent;

    private float gridAreaWidth, gridAreaHeight;
    private Vector3 gridSpawnPos;
    private float gridCellXScale, gridCellZScale;

    [HideInInspector] public int createdLevelNumber;
    [HideInInspector] public int levelCoin;
    private LevelData tempLevelData;
    private string savePath;
    public int[] opponentLevels;
    public void SpawnGridCell()
    {
        opponentLevels = new int[gridCellXCount * gridCellZCount];

        for (int k = gridCellParentActive.childCount - 1; k >= 0; k--)
        {
            gridCellParentActive.GetChild(k).gameObject.SetActive(false);
            gridCellParentActive.GetChild(k).SetParent(gridCellParentInactive);
        }
        for (int l = gridCellParentOpponent.childCount - 1; l >= 0; l--)
        {
            gridCellParentOpponent.GetChild(l).gameObject.SetActive(false);
            gridCellParentOpponent.GetChild(l).SetParent(gridCellParentInactive);
        }

        gridAreaWidth = Mathf.Abs(gridCellBottomLeft.position.x - gridCellBottomRight.position.x);
        gridAreaHeight = Mathf.Abs(gridCellBottomLeft.position.z - gridCellTopLeft.position.z);

        gridCellXScale = gridAreaWidth / gridCellXCount;
        gridCellZScale = gridAreaHeight / gridCellZCount;

        for (int i = 0; i < gridCellZCount; i++)
        {
            for (int j = 0; j < gridCellXCount; j++)
            {
                gridSpawnPos = new Vector3((gridCellBottomLeft.position.x + j * gridCellXScale),
                                            gridCellBottomLeft.position.y,
                                            (gridCellBottomLeft.position.z + i * gridCellZScale));

                if (gridCellParentInactive.childCount > 0)
                {
                    tempGridCell = gridCellParentInactive.GetChild(0).gameObject;
                    tempGridCell.transform.position = gridSpawnPos;
                    tempGridCell.transform.SetParent(gridCellParentActive);
                    tempGridCell.SetActive(true);
                }
                else
                {
                    tempGridCell =
                            Instantiate(gridCellPrefab, gridSpawnPos, Quaternion.identity, gridCellParentActive);
                }

                tempGridCell.transform.localScale = new Vector3(gridCellXScale, 1f, gridCellZScale);

                gridSpawnPos = new Vector3((gridCellBottomLeft.position.x + j * gridCellXScale),
                            gridCellBottomLeft.position.y,
                            (gridCellOpponentSpawnPos.position.z + i * gridCellZScale));

                if (gridCellParentInactive.childCount > 0)
                {
                    tempGridCell = gridCellParentInactive.GetChild(0).gameObject;
                    tempGridCell.transform.position = gridSpawnPos;
                    tempGridCell.transform.SetParent(gridCellParentOpponent);
                    tempGridCell.SetActive(true);
                }
                else
                {
                    tempGridCell =
                            Instantiate(gridCellPrefab, gridSpawnPos, Quaternion.identity, gridCellParentOpponent);
                }

                tempGridCell.transform.localScale = new Vector3(gridCellXScale, 1f, gridCellZScale);

            }
        }
    }
    public void CreateLevel()
    {
        tempLevelData = ScriptableObject.CreateInstance<LevelData>();

        savePath = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/LevelScriptableObjects/Level" + createdLevelNumber + ".asset");

        tempLevelData.playerGridPoses = new List<Vector3>();
        for (int i = 0; i < gridCellParentActive.childCount; i++)
        {
            tempLevelData.playerGridPoses.Add(gridCellParentActive.GetChild(i).transform.position);
        }

        tempLevelData.opponents = new List<OpponentController>();
        tempLevelData.opponentPoses = new List<Vector3>();
        for (int j = 0; j < gridCellParentOpponent.childCount; j++)
        {
            tempLevelData.opponentPoses.Add(new Vector3(gridCellParentOpponent.GetChild(j).transform.position.x + gridCellXScale / 2f,
            gridCellParentOpponent.GetChild(j).transform.position.y,
                gridCellParentOpponent.GetChild(j).transform.position.z + gridCellZScale / 2f));

            tempLevelData.opponents.Add(opponents[(opponentLevels[j]) - 1]);
        }

        tempLevelData.levelCoinCount = levelCoin;

        tempLevelData.gridCellXScale = gridCellXScale;
        tempLevelData.gridCellZScale = gridCellZScale;

        AssetDatabase.CreateAsset(tempLevelData, savePath);
        AssetDatabase.SaveAssets();


    }
}
//#endif
