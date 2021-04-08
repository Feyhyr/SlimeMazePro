using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    public new AudioSource audio;

    public new AudioSource audioTwo;

    private float sfxVolume = 1f;
    private float mainMenuAudio = 1f;
    //public AudioMixer audioMixer;

    public void Play(AudioClip sfxToPlay)
    {
        if (audio.isPlaying)
        {
            audio.Stop();
        }

        audio.clip = sfxToPlay;
        audio.Play();
    }

    public void Update()
    {
        audio.volume = sfxVolume;
        audioTwo.volume = mainMenuAudio;
    }

    public void updateSFXVolume(float sVolume)
    {
        sfxVolume = sVolume;
    }

    public void updateMenuAUDIO(float mVolume)
    {
        mainMenuAudio = mVolume;
    }



    /* public void SetVolume(float volume)
     {
         audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
     }*/

}
