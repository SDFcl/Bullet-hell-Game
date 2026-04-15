using UnityEngine;

public class MetaCurrency : Singleton<MetaCurrency>
{
    [SerializeField] private int GuildCoin = 0;

    public int MetaCurrencyAmount => GuildCoin;

    public void AddMetaCurrency(int amount)
    {
        GuildCoin += amount;
        Debug.Log($"MetaCurrency increased by {amount}. Total: {GuildCoin}");
    }

    public bool SpendMetaCurrency(int amount)
    {
        if (GuildCoin < amount) return false;
        GuildCoin -= amount;
        Debug.Log($"MetaCurrency decreased by {amount}. Total: {GuildCoin}");
        return true;
    }
}
