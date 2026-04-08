using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemData))]
public class ItemDataEditor : Editor
{
    SerializedProperty itemName;
    SerializedProperty itemIcon;
    SerializedProperty worldPrefab;
    SerializedProperty price;
    SerializedProperty itemType;
    SerializedProperty holdingPrefab;
    SerializedProperty consumableType;
    SerializedProperty effects;

    void OnEnable()
    {
        itemName = serializedObject.FindProperty("itemName");
        itemIcon = serializedObject.FindProperty("itemIcon");
        worldPrefab = serializedObject.FindProperty("WorldPrefab");
        price = serializedObject.FindProperty("Price");
        itemType = serializedObject.FindProperty("itemType");
        holdingPrefab = serializedObject.FindProperty("HoldingPrefab");
        consumableType = serializedObject.FindProperty("consumableType");
        effects = serializedObject.FindProperty("effects");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Field ปกติ
        EditorGUILayout.PropertyField(itemName);
        EditorGUILayout.PropertyField(itemIcon);
        EditorGUILayout.PropertyField(worldPrefab);
        EditorGUILayout.PropertyField(price);

        EditorGUILayout.Space();

        // 🔥 Item Type (ตัวแม่)
        EditorGUILayout.PropertyField(itemType);

        // 🔥 ทำให้ลูกอยู่ใต้ itemType
        EditorGUI.indentLevel++;

        ItemType type = (ItemType)itemType.enumValueIndex;

        switch (type)
        {
            case ItemType.Weapon:
                EditorGUILayout.PropertyField(holdingPrefab);
                break;

            case ItemType.Consumable:
                EditorGUILayout.PropertyField(consumableType);
                EditorGUILayout.PropertyField(effects, true);
                break;
        }

        EditorGUI.indentLevel--;

        serializedObject.ApplyModifiedProperties();
    }
}