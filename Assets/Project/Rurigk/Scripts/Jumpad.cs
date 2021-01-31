using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jumpad : MonoBehaviour {

    RaycastHit hit;
    Ray clickRay;

    public Renderer modelRenderer;
    float controlTime;

    public float jumpScale = 1;

    public AudioClip soundEffect;
    public AudioSource audioSource;

	// Use this for initialization
	void Start () {
        if(!modelRenderer)
		{
            modelRenderer = GetComponent<MeshRenderer>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        controlTime += Time.deltaTime;
        modelRenderer.material.SetFloat("_ControlTime", controlTime);
	}


	public void OnCollisionEnter(Collision collision)
	{
        if(collision.gameObject.tag == "Player")
		{
            audioSource.PlayOneShot(soundEffect);

            controlTime = 0;
            modelRenderer.material.SetVector("_ModelOrigin", transform.position);
            modelRenderer.material.SetVector("_ImpactOrigin", collision.contacts[0].point);

            PlayerController.Instance.Jump(jumpScale);
        }
    }
}
