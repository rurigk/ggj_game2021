using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoTechPlayer : MonoBehaviour
{
    public List<DemoTechGun> guns;
    int gunIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(guns.Count > 0)
		{
            for (int iGuns = 0; iGuns < guns.Count; iGuns++)
            {
                guns[iGuns].HideGun();
            }
            guns[0].ShowGun();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && guns.Count > 0)
        {
            gunIndex = gunIndex + 1 < guns.Count ? gunIndex + 1 : 0;
            for(int iGuns = 0; iGuns < guns.Count; iGuns++)
			{
                guns[iGuns].HideGun();
			}
            guns[gunIndex].ShowGun();
        }

        if(Input.GetMouseButtonDown(0) && guns.Count > 0)
		{
            guns[gunIndex].ShootGun();
        }
    }

	private void FixedUpdate()
	{
		
	}
}
