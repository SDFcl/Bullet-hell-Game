using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SavedInventory
{
    public List<InventoryItem> weapons;
    public List<InventoryItem> consumables;

    public int coins;
}

public static class GameSession
{
    public static LevelID currentLevel;
    public static int lastRandomIndex = -1;
    public static SavedInventory savedInventory;
    public static int savedHealth = 0;
    public static float timeCount;
    public static int enemyCount;
    public static bool isGamePlaying = false;
}
