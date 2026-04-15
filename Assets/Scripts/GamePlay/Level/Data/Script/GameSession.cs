using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SavedInventory
{
    public List<Item> weapons;
    public List<Item> consumables;
    public int coins;
}

public static class GameSession
{
    public static LevelID currentLevel;
    public static int lastRandomIndex = -1;
    public static SavedInventory savedInventory;
}
