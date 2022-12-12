using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiControllerScript : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;
    public TextMeshProUGUI musicText;
    public TextMeshProUGUI sfxText;
    

   
    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }
    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
        musicText.text = Mathf.Ceil(musicSlider.value * 100).ToString();
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(sfxSlider.value);
        sfxText.text = Mathf.Ceil(sfxSlider.value * 100).ToString();
    }


}
