public enum WeaponType
{
    Melee,
    Ranged
}
public enum FireRateType
{
    None,
    VerySlow,
    Slow,
    Normal,
    Fast,
    VeryFast,
    MeleeFast

}
public enum ManaCostType
{
    None,
    VeryLow,
    Low,
    Medium,
    High,
    VeryHigh,
    QuiteLowSP
}

[System.Serializable]
public struct BaseWeaponData
{
    public string weaponName;
    public float baseDamage;
    public FireRateType fireRate;
    public WeaponType weaponType;
}

[System.Serializable]
public struct MeleeWeaponData
{
    public BaseWeaponData baseData;
}

[System.Serializable]
public struct RangedWeaponData
{
    public BaseWeaponData baseData;
    public string projectileTag;
    public ManaCostType manaCostType;
    public float projectileSpeed;
}




