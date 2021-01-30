using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilTrans : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right*0.5f*Time.deltaTime);
        Destroy(this.gameObject, 2);
    }
}
