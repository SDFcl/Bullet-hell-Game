using System.IO;
using UnityEditor;
using UnityEngine;

public class AutoCreateWeaponDataAssets : AssetPostprocessor
{
    private const string TargetRoot = "Assets/Prefab/Item/Weapon/ForUse";

    static void OnPostprocessAllAssets(
        string[] importedAssets,
        string[] deletedAssets,
        string[] movedAssets,
        string[] movedFromAssetPaths)
    {
        foreach (string path in importedAssets)
        {
            if (!AssetDatabase.IsValidFolder(path))
                continue;

            if (!IsTargetDataFolder(path))
                continue;

            CreateDataAssetsIfMissing(path);
        }
    }

    private static bool IsTargetDataFolder(string folderPath)
    {
        if (!folderPath.StartsWith(TargetRoot))
            return false;

        string folderName = Path.GetFileName(folderPath);
        return folderName == "Data";
    }

    private static void CreateDataAssetsIfMissing(string dataFolderPath)
    {
        string weaponFolderPath = Path.GetDirectoryName(dataFolderPath)?.Replace("\\", "/");
        if (string.IsNullOrEmpty(weaponFolderPath))
            return;

        string weaponFolderName = Path.GetFileName(weaponFolderPath);

        string weaponDataPath = $"{dataFolderPath}/{weaponFolderName}_Ability_Data.asset";
        string itemDataPath = $"{dataFolderPath}/{weaponFolderName}_Interection_Data.asset";

        bool createdAnything = false;

        if (AssetDatabase.LoadAssetAtPath<WeaponDataSO>(weaponDataPath) == null)
        {
            WeaponDataSO weaponData = ScriptableObject.CreateInstance<WeaponDataSO>();

            weaponData.weaponType = WeaponType.Melee;
            weaponData.meleeData = new MeleeWeaponData
            {
                baseData = new BaseWeaponData
                {
                    weaponName = weaponFolderName,
                    baseDamage = 0f,
                    fireRate = FireRateType.Normal,
                    weaponType = WeaponType.Melee
                }
            };

            weaponData.rangedData = new RangedWeaponData
            {
                baseData = new BaseWeaponData
                {
                    weaponName = weaponFolderName,
                    baseDamage = 0f,
                    fireRate = FireRateType.Normal,
                    weaponType = WeaponType.Ranged
                },
                projectileTag = string.Empty,
                manaCostType = ManaCostType.None,
                projectileSpeed = 0f
            };

            AssetDatabase.CreateAsset(weaponData, weaponDataPath);
            createdAnything = true;
        }

        if (AssetDatabase.LoadAssetAtPath<ItemData>(itemDataPath) == null)
        {
            ItemData itemData = ScriptableObject.CreateInstance<ItemData>();

            itemData.itemName = weaponFolderName;
            itemData.itemType = ItemType.Weapon;

            AssetDatabase.CreateAsset(itemData, itemDataPath);
            createdAnything = true;
        }

        if (createdAnything)
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"Auto-created data assets in: {dataFolderPath}");
        }
    }
}
