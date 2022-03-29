using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    [HideInInspector]public List<Vector3> opponentPoses;
    [HideInInspector]public List<OpponentController> opponents;
    [HideInInspector]public List<Vector3> playerGridPoses;
    public int levelCoinCount = 100;
    [HideInInspector]public float gridCellXScale, gridCellZScale;

}
