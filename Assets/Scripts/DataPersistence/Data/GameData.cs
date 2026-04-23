using System;

[Serializable]
public class GameData
{
    public int guildCoin;
    public SerializableDictionary<string, int> upgradeLevels;
    public SerializableDictionary<int, bool> UnLockLevel;

    // ✅ เพิ่มตรงนี้
    public bool OnGamePlaying; // ใช้สำหรับเช็คว่าเกมกำลังเล่นอยู่หรือไม่
    public int currentMap;
    public int currentStage;

    public GameData()
    {
        guildCoin = 0;
        upgradeLevels = new SerializableDictionary<string, int>();
        UnLockLevel = new SerializableDictionary<int, bool>();

        OnGamePlaying = false; // เริ่มต้นว่าเกมยังไม่เล่น
        currentMap = 1;
        currentStage = 0; // Stage1
    }

    public void SetCurrentLevel(int map, int stage)
    {
        currentMap = map;
        currentStage = stage;
    }
}