using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    public Vector3[] playerGridPoses;
    public Dictionary<Vector3, OpponentController> opponents = new Dictionary<Vector3, OpponentController>();
    public int levelCoinCount = 100;
}
