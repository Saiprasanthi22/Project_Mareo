using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer audio;
    public void SetVolume(float volume)
    {
        audio.SetFloat("volume",volume);
    }
}
