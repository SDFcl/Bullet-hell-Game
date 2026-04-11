using UnityEditor;
using UnityEngine;

public static class WeaponSortingLayerEditor
{
    private const string WeaponPrefabRoot = "Assets/Prefab/Item/Weapon";
    private const string TargetSortingLayer = "Weapon";
    private const int TargetOrderInLayer = 0;

    [MenuItem("Tools/Weapon/Set Sorting Layer For All Weapon Prefabs")]
    public static void SetWeaponSortingLayer()
    {
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { WeaponPrefabRoot });

        int changedPrefabCount = 0;
        int changedRendererCount = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefabRoot = PrefabUtility.LoadPrefabContents(path);

            bool prefabChanged = false;

            try
            {
                SpriteRenderer[] renderers = prefabRoot.GetComponentsInChildren<SpriteRenderer>(true);

                foreach (SpriteRenderer sr in renderers)
                {
                    if (sr.sortingLayerName != TargetSortingLayer || sr.sortingOrder != TargetOrderInLayer)
                    {
                        sr.sortingLayerName = TargetSortingLayer;
                        sr.sortingOrder = TargetOrderInLayer;
                        prefabChanged = true;
                        changedRendererCount++;
                    }
                }

                if (prefabChanged)
                {
                    PrefabUtility.SaveAsPrefabAsset(prefabRoot, path);
                    changedPrefabCount++;
                }
            }
            finally
            {
                PrefabUtility.UnloadPrefabContents(prefabRoot);
            }
        }

        AssetDatabase.Refresh();
        Debug.Log($"Updated sorting layer for {changedRendererCount} SpriteRenderer(s) in {changedPrefabCount} weapon prefab(s).");
    }
}
