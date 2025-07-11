using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsData", menuName = "Data/LevelsData", order = 1)]
public class LevelsData : ScriptableObject
{
    public List<LevelData> Data;
}

[System.Serializable]
public class LevelData
{
    public int EnemiesInLevel;
    public int SceneryIndex;
}
