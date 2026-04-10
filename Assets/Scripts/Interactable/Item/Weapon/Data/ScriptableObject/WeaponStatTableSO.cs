using UnityEngine;
using System;

[Serializable]
public struct FireRateEntry
{
    public FireRateType type;
    public float value;
}

[Serializable]
public struct ManaCostEntry
{
    public ManaCostType type;
    public float value;
}

[CreateAssetMenu(menuName = "Weapon/Weapon Stat Table")]
public class WeaponStatTableSO : ScriptableObject
{
    public FireRateEntry[] fireRates;
    public ManaCostEntry[] manaCosts;

    public float GetFireRate(FireRateType type)
    {
        foreach (var entry in fireRates)
        {
            if (entry.type == type)
                return entry.value;
        }

        return 0f;
    }

    public float GetManaCost(ManaCostType type)
    {
        foreach (var entry in manaCosts)
        {
            if (entry.type == type)
                return entry.value;
        }

        return 0f;
    }
}