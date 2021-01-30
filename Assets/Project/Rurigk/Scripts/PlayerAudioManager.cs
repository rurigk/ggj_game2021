using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public AudioClip hoverSound;

    // Audio sources
    AudioSource hoverSoundSource;

    // Start is called before the first frame update
    void Start()
    {
        hoverSoundSource = gameObject.AddComponent<AudioSource>();

        ConfigureHoverAudioSource();
    }

    void ConfigureHoverAudioSource()
	{
        hoverSoundSource.clip = hoverSound;
        hoverSoundSource.loop = true;
        hoverSoundSource.playOnAwake = true;
        hoverSoundSource.volume = 0.03f;
        hoverSoundSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHoverSoundVolume(float volumeScale)
	{
        float volumeCorrected = volumeScale > 1 ? 1 : volumeScale;
        volumeCorrected = 0.15f * volumeScale;
        hoverSoundSource.volume = volumeCorrected < 0.08f ? 0.08f : volumeCorrected;
    }
}
