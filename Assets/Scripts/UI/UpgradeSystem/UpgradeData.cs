using NUnit.Framework;
using UnityEngine;

[System.Serializable]
public struct upgradeData
{
    public float value;
    public int cost;
}

public enum UpgradeType
{
    Health,
    Damage,
    Speed,
}

[CreateAssetMenu(menuName = "UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;

    public UpgradeType upgradeType;

    public upgradeData[] upgradeValues;
}
