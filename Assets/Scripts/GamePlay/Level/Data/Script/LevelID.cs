using UnityEngine;
public enum Stage
{
    Stage1,
    Stage2,
    BossStage
}

[System.Serializable]
public struct LevelID
{
    public int map;
    public Stage stage;

    public LevelID(int map, Stage stage)
    {
        this.map = map;
        this.stage = stage;
    }
    public Stage? GetNextStage(Stage current)
    {
        switch (current)
        {
            case Stage.Stage1:
                return Stage.Stage2;
            case Stage.Stage2:
                return Stage.BossStage;
            case Stage.BossStage:
                return null;
            default:
                return current;
        }
    }
}
