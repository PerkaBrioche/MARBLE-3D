using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManagerr : MonoBehaviour
{
    public Slider SFXSlider;
    public Slider MusicSlider;
    public Slider AmbiantSlider;
    
    [Space(20)]

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource SonAmbiant;

    [Space(20)]

    public List<AudioSource> ExtraSources;
    public List<AudioSource> ExtraSourcesMusic;
    public List<AudioSource> ExtrasourcesAmbiant;

    void Start()
    {
        if(SFXSlider == null || MusicSlider == null || AmbiantSlider == null)
        {
            Debug.LogError("You need to assign the sliders in the inspector");
            return;
        }
        
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        AmbiantSlider.value = PlayerPrefs.GetFloat("SonAmbiant", 1f);
        
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
        MusicSlider.onValueChanged.AddListener(SetMusicVolume);
        AmbiantSlider.onValueChanged.AddListener(SetAmbianSound);
        // Initialiser les volumes
        SetSFXVolume(SFXSlider.value);
        SetMusicVolume(MusicSlider.value);
        SetAmbianSound(AmbiantSlider.value);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        foreach (var source in ExtraSources)
        {
            if (source != null)
            {
                source.volume = volume;
            }
        }
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        foreach (var source in ExtraSourcesMusic)
        {
            if (source != null)
            {
                source.volume = volume;
            }
        }
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void SetAmbianSound(float volume)
    {
        SonAmbiant.volume = volume;
        foreach (var source in ExtrasourcesAmbiant)
        {
            if (source != null)
            {
                source.volume = volume;
            }
        }
        PlayerPrefs.SetFloat("SonAmbiant", volume);
    }
    
}