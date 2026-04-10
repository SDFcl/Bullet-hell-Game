using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Weapon Data")]
public class WeaponDataSO : ScriptableObject
{
    public WeaponType weaponType;

    public MeleeWeaponData meleeData;
    public RangedWeaponData rangedData;

    [Header("Shared Tables")]
    [SerializeField] private WeaponStatTableSO statTable;

    public float GetFireRateValue()
    {
        BaseWeaponData baseData = GetBaseData();
        return statTable != null ? statTable.GetFireRate(baseData.fireRate) : 0f;
    }

    public float GetManaCostValue()
    {
        if (weaponType != WeaponType.Ranged)
            return 0f;

        return statTable != null ? statTable.GetManaCost(rangedData.manaCostType) : 0f;
    }

    public BaseWeaponData GetBaseData()
    {
        return weaponType switch
        {
            WeaponType.Melee => meleeData.baseData,
            WeaponType.Ranged => rangedData.baseData,
            _ => default
        };
    }
}
