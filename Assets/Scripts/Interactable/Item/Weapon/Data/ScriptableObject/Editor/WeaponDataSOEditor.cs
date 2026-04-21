using UnityEditor;

[CustomEditor(typeof(WeaponDataSO))]
public class WeaponDataSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty weaponTypeProp = serializedObject.FindProperty("weaponType");
        SerializedProperty meleeDataProp = serializedObject.FindProperty("meleeData");
        SerializedProperty rangedDataProp = serializedObject.FindProperty("rangedData");
        SerializedProperty statTableProp = serializedObject.FindProperty("statTable");

        SerializedProperty onAttackSoundIDProp = serializedObject.FindProperty("onAttackSoundID");
        SerializedProperty onHitSoundIDProp = serializedObject.FindProperty("onHitSoundID");

        EditorGUILayout.PropertyField(weaponTypeProp);
        EditorGUILayout.PropertyField(statTableProp);

        WeaponType weaponType = (WeaponType)weaponTypeProp.enumValueIndex;

        EditorGUILayout.Space();

        switch (weaponType)
        {
            case WeaponType.Melee:
                EditorGUILayout.LabelField("Melee Data", EditorStyles.boldLabel);
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.PropertyField(meleeDataProp, true);
                EditorGUILayout.EndVertical();
                break;

            case WeaponType.Ranged:
                EditorGUILayout.LabelField("Ranged Data", EditorStyles.boldLabel);
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.PropertyField(rangedDataProp, true);
                EditorGUILayout.EndVertical();
                break;
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Sound", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(onAttackSoundIDProp);
        EditorGUILayout.PropertyField(onHitSoundIDProp);

        serializedObject.ApplyModifiedProperties();
    }
}
