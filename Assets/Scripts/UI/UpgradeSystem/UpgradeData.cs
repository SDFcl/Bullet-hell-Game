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
    IncrementalDamage,
    Health,
    Mana,
    BonusCoin,
}

[CreateAssetMenu(menuName = "UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public UpgradeType upgradeType;

    public string upgradeName;
    public Sprite upgradeIcon;
    public string upgradeDescription;

    public upgradeData[] upgradeValues;
}
