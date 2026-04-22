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
    [SerializeField] private string id;
    public string Id => id;

    public UpgradeType upgradeType;

    public string upgradeName;
    public Sprite upgradeIcon;
    public string upgradeDescription;

    public UpgradeValue[] upgradeValues;

    [System.Serializable]
    public struct UpgradeValue
    {
        public int cost;
        public float value;
    }

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
}
