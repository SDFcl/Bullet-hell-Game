using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections;

public class SoundSaveData : Singleton<SoundSaveData>,IDataPersistence
{
    public AudioMixer audioMixer;
    float master,sfx,bmg;
    public void LoadData(GameData data)
    {
        master = data.MasterVolume;
        sfx = data.SFXVolume;
        bmg = data.BMGVolume;
        

        // audioMixer.GetFloat("MasterVolume", out float currentMasterVolume);
        // audioMixer.GetFloat("SFXVolume", out float currentSfxVolume);
        // audioMixer.GetFloat("BGMVolume", out float currentBgmVolume);

        // Debug.Log(
        //     $"[SoundSaveData.LoadData] " +
        //     $"scene={SceneManager.GetActiveScene().name}, " +
        //     $"object={gameObject.name}, " +
        //     $"instance={GetInstanceID()}, " +
        //     $"Set Master={data.MasterVolume} success={masterSet} current={currentMasterVolume}, " +
        //     $"SFX={data.SFXVolume} success={sfxSet} current={currentSfxVolume}, " +
        //     $"BGM={data.BMGVolume} success={bgmSet} current={currentBgmVolume}"
        // );
    }
    public void SaveData(ref GameData data)
    {
        bool masterGet = audioMixer.GetFloat("MasterVolume",out float _masterVolume);
        bool sfxGet = audioMixer.GetFloat("SFXVolume",out float _sfxVolume);
        bool bgmGet = audioMixer.GetFloat("BGMVolume",out float _bmgVolume);

        data.MasterVolume = _masterVolume;
        data.SFXVolume = _sfxVolume;
        data.BMGVolume = _bmgVolume;

        // Debug.Log(
        //     $"[SoundSaveData.SaveData] " +
        //     $"scene={SceneManager.GetActiveScene().name}, " +
        //     $"object={gameObject.name}, " +
        //     $"instance={GetInstanceID()}, " +
        //     $"Master success={masterGet} value={_masterVolume}, " +
        //     $"SFX success={sfxGet} value={_sfxVolume}, " +
        //     $"BGM success={bgmGet} value={_bmgVolume}"
        // );
    }

    private void Start()
    {
        audioMixer.SetFloat("MasterVolume", master);
        audioMixer.SetFloat("SFXVolume", sfx);
        audioMixer.SetFloat("BGMVolume", bmg);
    }

}
