using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private SoundTable soundTable;

    [Header("SFX")]
    [SerializeField] private string audioPoolTag = "SFXsource";

    [Header("BGM")]
    [SerializeField] private AudioSource bgmSource;
    private SoundID currentBgmId;

    private Dictionary<SoundID, SoundData> soundsById;

    protected override bool UseDontDestroyOnLoad => false;

    protected override void Awake()
    {
        if (bgmSource == null)
        {
            Debug.LogWarning("BGM dont have audioSource");
        }
        base.Awake();
        BuildSoundLookup();
    }

    private void BuildSoundLookup()
    {
        soundsById = new Dictionary<SoundID, SoundData>();

        foreach (SoundLibrary library in soundTable.soundLibraries)
        {
            foreach (SoundData sound in library.sounds)
            {
                if (sound.id == null || sound.clip == null)
                    continue;

                soundsById[sound.id] = sound;
            }
        }
    }
    // SFX
    public float PlaySFX(SoundID id, Vector3 position)
    {
        if (!soundsById.TryGetValue(id, out SoundData data))
            return 0f;

        GameObject audioObject = ObjectPooler.Instance.SpawnFromPool(
            audioPoolTag,
            position,
            Quaternion.identity
        );

        if (audioObject.TryGetComponent(out SFXPlayer player))
            player.Play(data);

        return data.clip != null ? data.clip.length : 0f;
    }

    //BGM
    public void PlayBGM(SoundID id)
    {
        if (!soundsById.TryGetValue(id, out SoundData data))
            return;

        if (currentBgmId == id && bgmSource.isPlaying)
            return;

        currentBgmId = id;

        bgmSource.clip = data.clip;
        bgmSource.volume = data.volume;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        currentBgmId = null;
        bgmSource.Stop();
    }
}
