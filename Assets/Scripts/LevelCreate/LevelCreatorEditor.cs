//#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelCreator))]
public class LevelCreatorEditor : Editor
{


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelCreator levelCreator = (LevelCreator)target;

        GUILayout.Space(25f);

        EditorGUI.BeginChangeCheck();

        GUILayout.Label("Grid Cell Width Count");
        levelCreator.gridCellXCount = EditorGUILayout.IntSlider(levelCreator.gridCellXCount, 1, 10);
        GUILayout.Label("Grid Cell Height Count");
        levelCreator.gridCellZCount = EditorGUILayout.IntSlider(levelCreator.gridCellZCount, 1, 10);

        if (EditorGUI.EndChangeCheck())
        {
            levelCreator.SpawnGridCell();
        }

        GUILayout.Label("Number Of Level To Be Created");
        levelCreator.createdLevelNumber = EditorGUILayout.IntField("Number Of Level", levelCreator.createdLevelNumber);

        GUILayout.Space(25f);

        GUILayout.Label("Count Of Coins To Be Added At The Start Of The Level");
        levelCreator.levelCoin = EditorGUILayout.IntField("Count Of Coin", levelCreator.levelCoin);

        GUILayout.Space(25f);

        if (GUILayout.Button("CreateLevel"))
        {
            levelCreator.CreateLevel();
        }
    }

}

//#endif
