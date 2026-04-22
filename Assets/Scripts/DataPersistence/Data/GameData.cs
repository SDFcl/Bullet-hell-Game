using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public int guildCoin;
    public SerializableDictionary<string, int> upgradeLevels;
    public SerializableDictionary<int, bool> UnLockLevel;

    public GameData()
    {
        guildCoin = 0;
        upgradeLevels = new SerializableDictionary<string, int>();
        UnLockLevel = new SerializableDictionary<int, bool>();
    }
}