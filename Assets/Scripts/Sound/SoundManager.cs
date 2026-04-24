using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private SoundTable soundTable;

    [Header("SFX")]
    [SerializeField] private string audioPoolTag = "SFXsource";

    [Header("BGM")]
    [SerializeField] private AudioSource bgmSource;

    [Header("Mixer")]
    [SerializeField] private AudioMixer audioMixer;
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
    
    public float PlayLoopSFX(SoundID id, Vector3 position, float duration)
    {
        if (!soundsById.TryGetValue(id, out SoundData data))
            return 0f;

        GameObject audioObject = ObjectPooler.Instance.SpawnFromPool(
            audioPoolTag,
            position,
            Quaternion.identity
        );

        if (audioObject.TryGetComponent(out SFXPlayer player))
            player.PlayLoop(data, duration);

        return duration;
    }
    // UnityEvent
    public void PlayEventSFX(SoundID id)
    {
        PlaySFX(id, Vector3.zero);
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

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume",level);
    }
    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("SFXVolume",level);
    }
    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("BGMVolume",level);
    }
}
