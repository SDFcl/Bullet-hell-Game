using System;
using UnityEngine;

public class MetaCurrency : Singleton<MetaCurrency>, IDataPersistence
{
    [SerializeField] private int GuildCoin = 0;

    public int MetaCurrencyAmount => GuildCoin;

    public Action<int> OnMetaCurrencyChanged;

    public void Start()
    {
        OnMetaCurrencyChanged?.Invoke(GuildCoin);
        //Debug.Log($"MetaCurrency initialized. Total: {GuildCoin}");
    }

    public void LoadData(GameData data)
    {
        GuildCoin = data.guildCoin;
        // Debug.Log($"MetaCurrency loaded. Total: {GuildCoin}");
    }

    public void SaveData(ref GameData data)
    {
        data.guildCoin = this.GuildCoin;
        // Debug.Log($"MetaCurrency saved. Total: {GuildCoin}");
    }

    private void FixedUpdate()
    {
        // OnMetaCurrencyChanged?.Invoke(GuildCoin);
    }

    public void AddMetaCurrency(int amount)
    {
        GuildCoin += amount;
        OnMetaCurrencyChanged?.Invoke(GuildCoin);
        //Debug.Log($"MetaCurrency increased by {amount}. Total: {GuildCoin}");
    }

    public bool SpendMetaCurrency(int amount)
    {
        if (GuildCoin < amount) return false;
        GuildCoin -= amount;
        //Debug.Log($"MetaCurrency decreased by {amount}. Total: {GuildCoin}");
        OnMetaCurrencyChanged?.Invoke(GuildCoin);
        return true;
    }

    public bool CanAfford(int amount)
    {
        return GuildCoin >= amount;
    }
}
