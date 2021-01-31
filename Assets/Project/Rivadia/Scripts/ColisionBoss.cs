using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionBoss : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="bullet")
        {
            VidaBoss.vidaBoss--;
            Debug.Log("Obligame perro!");
        }
    }

}
