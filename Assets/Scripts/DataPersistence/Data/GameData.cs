using System;

[Serializable]
public class GameData
{
    public int guildCoin;
    public SerializableDictionary<string, int> upgradeLevels;
    public SerializableDictionary<int, bool> UnLockLevel;

    // ✅ เพิ่มตรงนี้
    public bool OnGamePlaying; // ใช้สำหรับเช็คว่าเกมกำลังเล่นอยู่หรือไม่
    public float SavedTimeCount; // ใช้สำหรับเก็บเวลาที่เล่นไปแล้ว
    public int SavedenemyCount; // ใช้สำหรับเก็บจำนวนศัตรูที่เล่นไปแล้ว
    public int CurrentReward;

    public GameData()
    {
        guildCoin = 0;
        upgradeLevels = new SerializableDictionary<string, int>();
        UnLockLevel = new SerializableDictionary<int, bool>();

        OnGamePlaying = false; // เริ่มต้นว่าเกมยังไม่เล่น
        SavedTimeCount = 0f; // เริ่มต้นเวลาที่เล่นไปแล้วเป็น 0
        SavedenemyCount = 0; // เริ่มต้นจำนวนศัตรูที่เล่นไปแล้วเป็น 0
        CurrentReward = 0;
    }
}