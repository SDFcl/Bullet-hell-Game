using System;
using UnityEngine;

public class MetaCurrency : Singleton<MetaCurrency>
{
    [SerializeField] private int GuildCoin = 0;

    public int MetaCurrencyAmount => GuildCoin;

    public Action<int> OnMetaCurrencyChanged;

    public void Start()
    {
        // เริ่มต้นด้วยค่าเริ่มต้นหรือโหลดจาก PlayerPrefs
        OnMetaCurrencyChanged?.Invoke(GuildCoin);
    }

    public void AddMetaCurrency(int amount)
    {
        GuildCoin += amount;
        OnMetaCurrencyChanged?.Invoke(GuildCoin);
        Debug.Log($"MetaCurrency increased by {amount}. Total: {GuildCoin}");
    }

    public bool SpendMetaCurrency(int amount)
    {
        if (GuildCoin < amount) return false;
        GuildCoin -= amount;
        Debug.Log($"MetaCurrency decreased by {amount}. Total: {GuildCoin}");
        OnMetaCurrencyChanged?.Invoke(GuildCoin);
        return true;
    }

    public bool CanAfford(int amount)
    {
        return GuildCoin >= amount;
    }
}
