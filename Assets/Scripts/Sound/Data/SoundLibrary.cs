using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SoundData
{

#if UNITY_EDITOR
    [SerializeField, HideInInspector]
    private string idName;

    public void UpdateName()
    {
        idName = id != null ? id.name : "None";
    }
#endif

    public SoundID id;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
}

[CreateAssetMenu(menuName = "Sound/SoundLibrary")]
public class SoundLibrary : ScriptableObject
{
    public List<SoundData> sounds;

#if UNITY_EDITOR
    private void OnValidate()
    {
        foreach (var s in sounds)
        {
            if (s != null)
                s.UpdateName();
        }
    }
#endif
}
