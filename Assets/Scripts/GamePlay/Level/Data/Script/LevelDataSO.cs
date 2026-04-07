using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level/LevelData", fileName = "LevelData")]
public class LevelDataSO : ScriptableObject
{
    public LevelID levelID;
    public string mapName;
    public List<GameObject> levelPrefabs;
    
}
