using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public int guildCoin;

    // 🔥 เก็บแค่นี้พอ
    public SerializableDictionary<string, int> upgradeLevels;

    public GameData()
    {
        guildCoin = 0;
        upgradeLevels = new SerializableDictionary<string, int>();
    }
}