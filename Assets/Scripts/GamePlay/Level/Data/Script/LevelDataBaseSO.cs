using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataBaseSO", menuName = "Level/LevelDataBaseSO")]
public class LevelDataBaseSO : ScriptableObject
{
    public List<LevelDataSO> levelDatas;
    public LevelDataSO GetLevelData(LevelID levelID)
    {
        return levelDatas.Find(l =>
            l.levelID.map == levelID.map &&
            l.levelID.stage == levelID.stage);
    }
}
