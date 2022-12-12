using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sounds[] musicSounds, sfxsound;
    public AudioSource musicSource, sfxSource;
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        PlayMusic("Theme");
    }

    public void PlayMusic(string soundName)
    {
        Sounds sound = Array.Find(musicSounds, x => x.name == soundName);

        if (sound == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }


    public void PlaySFX(string sfxName)
    {
        Sounds sound = Array.Find(sfxsound, x => x.name == sfxName);

        if (sound == null)
        {
            Debug.Log("SFX not found");
        }
        else
        {
            sfxSource.PlayOneShot(sound.clip);
        }
    }


    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }


    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
        
       

    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
          
    }
}
