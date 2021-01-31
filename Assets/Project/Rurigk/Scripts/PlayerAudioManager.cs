using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public static PlayerAudioManager Instance;

    public AudioClip ambientMusicSound;
    public AudioClip hoverSound;
    public AudioClip pauseMusicSound;

    public AudioClip shootSound;
    public AudioClip collectSound;

    // Audio sources
    AudioSource hoverSoundSource;
    AudioSource ambientSoundSource;
    AudioSource pauseSoundSource;

    AudioSource shootSoundSource;
    AudioSource collectSoundSource;

    // Start is called before the first frame update
    void Start()
    {
        if(!Instance)
		{
            Instance = this;
        }
        ConfigurePauseMusicAudioSource();
        ConfigureAmbientMusicAudioSource();
        ConfigureHoverAudioSource();
        ConfigureShootAudioSource();
        ConfigureCollectAudioSource();
    }

    void ConfigureShootAudioSource()
    {
        shootSoundSource = gameObject.AddComponent<AudioSource>();
        shootSoundSource.clip = shootSound;
        shootSoundSource.loop = false;
        shootSoundSource.playOnAwake = false;
        shootSoundSource.volume = 0.1f;
    }

    void ConfigureCollectAudioSource()
    {
        collectSoundSource = gameObject.AddComponent<AudioSource>();
        collectSoundSource.clip = collectSound;
        collectSoundSource.loop = false;
        collectSoundSource.playOnAwake = false;
        collectSoundSource.volume = 0.2f;
    }

    void ConfigurePauseMusicAudioSource()
    {
        pauseSoundSource = gameObject.AddComponent<AudioSource>();
        pauseSoundSource.clip = pauseMusicSound;
        pauseSoundSource.loop = false;
        pauseSoundSource.playOnAwake = false;
        pauseSoundSource.volume = 0.1f;
    }

    void ConfigureAmbientMusicAudioSource()
    {
        ambientSoundSource = gameObject.AddComponent<AudioSource>();
        ambientSoundSource.clip = ambientMusicSound;
        ambientSoundSource.loop = true;
        ambientSoundSource.playOnAwake = true;
        ambientSoundSource.volume = 0.08f;
        ambientSoundSource.Play();
    }

    void ConfigureHoverAudioSource()
	{
        hoverSoundSource = gameObject.AddComponent<AudioSource>();
        hoverSoundSource.clip = hoverSound;
        hoverSoundSource.loop = true;
        hoverSoundSource.playOnAwake = true;
        hoverSoundSource.volume = 0.15f;
        hoverSoundSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayShootSound()
	{
        shootSoundSource.PlayOneShot(shootSound);
	}

    public void PlayCollectSound()
    {
        collectSoundSource.PlayOneShot(collectSound);
    }

    public void SetHoverSoundVolume(float volumeScale)
	{
        float volumeCorrected = volumeScale > 1 ? 1 : volumeScale;
        volumeCorrected = 0.15f * volumeScale;
        hoverSoundSource.volume = volumeCorrected < 0.08f ? 0.08f : volumeCorrected;
    }
}
