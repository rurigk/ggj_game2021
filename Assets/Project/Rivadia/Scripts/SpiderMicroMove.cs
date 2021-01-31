using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class SpiderMicroMove : MonoBehaviour
{
    public Transform target;

    private Vector3 piso;

    private float velocity;
    // Start is called before the first frame update
    void Start()
    {
        piso = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //velocity = Random.Range(0.5f, 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y > 0)
        {
            velocity = Random.Range(0.5f, 0.8f);
            transform.Translate(Vector3.up*-velocity);
        }

        if (this.transform.position.y < 0)
        {
            this.transform.LookAt(-target.position);
            
        }

        Vector3 targetPos = target.position-this.transform.position;

        targetPos.Normalize();
        
        if (targetPos.magnitude > 0.8f)
        {
            this.transform.Translate(targetPos*0.5f*Time.deltaTime);
        }
    }
}
