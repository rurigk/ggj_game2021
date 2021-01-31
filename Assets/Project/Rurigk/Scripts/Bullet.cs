using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float bulletSpeed = 1;
    System.Timers.ElapsedEventHandler timerEvent;
    System.Timers.Timer aTimer;

    Rigidbody rb;
    bool forceApplied = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timerEvent += OnTimedEventHandler;
        SetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if(!forceApplied)
        {
            rb.AddForce(transform.forward * bulletSpeed);
            forceApplied = true;
        }
        //transform.Translate((Vector3.forward * Time.deltaTime) * bulletSpeed);
    }

	void OnCollisionEnter(Collision collision)
	{
        if(collision.gameObject.tag != "Player")
		{
            aTimer.Stop();
            aTimer.Dispose();
            CreateExplosion();
            Destroy(gameObject);
        }
    }

    void CreateExplosion()
	{
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;
	}

    void SetTimer()
    {
        // Create a timer with a interval.
        aTimer = new System.Timers.Timer(10000);
        // Hook up the Elapsed event for the timer. 
        aTimer.Elapsed += timerEvent;
        aTimer.AutoReset = false;
        aTimer.Enabled = true;
    }

    void OnTimedEventHandler(object sender, System.Timers.ElapsedEventArgs e)
	{
        aTimer.Stop();
        aTimer.Dispose();
        Destroy(gameObject);
    }
}
