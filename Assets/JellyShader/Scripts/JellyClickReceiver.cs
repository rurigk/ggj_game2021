﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JellyClickReceiver : MonoBehaviour {

    RaycastHit hit;
    Ray clickRay;

    Renderer modelRenderer;
    float controlTime;

	// Use this for initialization
	void Start () {
        modelRenderer = GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        controlTime += Time.deltaTime;

        modelRenderer.material.SetFloat("_ControlTime", controlTime);
	}

    public void OnFire(InputValue input)
	{
        clickRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(clickRay, out hit))
        {
            controlTime = 0;

            modelRenderer.material.SetVector("_ModelOrigin", transform.position);
            modelRenderer.material.SetVector("_ImpactOrigin", hit.point);
        }
    }
}
