using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UpdateAudioSlider : MonoBehaviour
{
    public Slider master,sfx,bmg;
    public AudioMixer audioMixer;

    void Start()
    {
        Invoke(nameof(DealySetup), 0.01f);
    }

    void DealySetup()
    {
        audioMixer.GetFloat("MasterVolume",out float _master);
        audioMixer.GetFloat("SFXVolume",out float _sfx);
        audioMixer.GetFloat("BGMVolume",out float _bgm);
        master.value = _master;
        sfx.value = _sfx;
        bmg.value = _bgm;
    }
}
