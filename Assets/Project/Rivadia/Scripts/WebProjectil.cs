using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebProjectil : MonoBehaviour
{

    public GameObject projectil;
    public Transform startPoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn",0,5);
    }

    // Update is called once per frame
    void Spawn()
    {
        GameObject proPos=Instantiate(projectil)as GameObject;

        proPos.transform.position = startPoint.position;
       
    }
}
