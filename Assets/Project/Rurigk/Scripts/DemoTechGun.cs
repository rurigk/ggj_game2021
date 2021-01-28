using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoTechGun : MonoBehaviour
{
    MeshRenderer mRenderer;

    public AudioClip gunAudio;
    AudioSource gunPlayer;

    public GameObject bulletSpawnPivot;
    public GameObject bulletPrefab;
    public float bulletForce = 2;
    // Start is called before the first frame update
    void Start()
    {
        mRenderer = gameObject.GetComponent<MeshRenderer>();
        gunPlayer = gameObject.AddComponent<AudioSource>();
        gunPlayer.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowGun()
	{
        mRenderer.enabled = true;
    }

    public void HideGun()
	{
        mRenderer.enabled = false;
	}

    public void ShootGun()
    {
        gunPlayer.PlayOneShot(gunAudio);
        GameObject bulletInstance = Instantiate(bulletPrefab);
        bulletInstance.transform.position = bulletSpawnPivot.transform.position;

        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        rb.AddForce(bulletSpawnPivot.transform.forward * bulletForce);
    }
}
