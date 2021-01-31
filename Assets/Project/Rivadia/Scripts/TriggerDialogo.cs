using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDialogo : MonoBehaviour
{
    public UnityEvent nuevoevento;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            Debug.Log("entraste");
            nuevoevento.Invoke();
        }
       
    }




}
