using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    public AudioClip bulletExplosionSound;
    AudioSource bulletSoundSource;


    void ConfigureExplosionAudioSource()
    {
        bulletSoundSource = gameObject.AddComponent<AudioSource>();
        bulletSoundSource.clip = bulletExplosionSound;
        bulletSoundSource.loop = false;
        bulletSoundSource.playOnAwake = false;
        bulletSoundSource.volume = 1f;
        bulletSoundSource.spatialBlend = 1f;
    }
    // Start is called before the first frame update
    void Start()
    {
        ConfigureExplosionAudioSource();
        bulletSoundSource.Play();
        StartCoroutine(Die());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Die()
	{
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
	}
}
