using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionMinionAñara : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="bullet")
        {
            this.gameObject.SetActive(false);
            Debug.Log("Me mori");
        }
    }

}
