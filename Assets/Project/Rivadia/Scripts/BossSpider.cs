using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum  EstadosAtaque 
{
    fase1,
    shoot,
    Fase2,
    fase3,
    Muerte

}
;


public class BossSpider : MonoBehaviour
{

    public EstadosAtaque estadoActual;

    public GameObject projectil;
    public GameObject spiderBody;

    public Transform mouth;

    public Transform[] tail;
    // Start is called before the first frame update
    void Start()
    {
        estadoActual = EstadosAtaque.fase1;
    }

    // Update is called once per frame
    void Update()
    {
        CambiarAtaque();
    }

    void CambiarAtaque()
    {
        switch (estadoActual)
        {
            case EstadosAtaque.fase1:
                Debug.Log("dispara");
                StartCoroutine(ChangeShoot());
                break;
            case EstadosAtaque.shoot:
                StartCoroutine(Disparo());
                break;
            case EstadosAtaque.Fase2:
                Debug.Log("Dispara mas");
                break;
            case EstadosAtaque.fase3:
                Debug.Log("Aplasta");
                break;
            case EstadosAtaque.Muerte:
                Debug.Log("Muere");
                break;
            


        }
    }

    public void SpawnProjectil()
    {
        GameObject projectilPos=Instantiate(projectil)as GameObject;
        ;
        projectilPos.transform.position = mouth.position;
        GameObject[] proyectiles = GameObject.FindGameObjectsWithTag("pBoss");
        if (proyectiles.Length > 1)
        {
            Destroy(projectilPos);
        }
    }

    public void SpawnSpider()
    {
        foreach (Transform t in tail)
        {
            GameObject spiderPos = Instantiate(spiderBody) as GameObject;

            spiderPos.transform.position = t.position;

            GameObject[] spiders = GameObject.FindGameObjectsWithTag("spiderMicro");
            if (spiders.Length > 3)
            {
                Destroy(spiderPos);
            }
        }
        

        

    }

    IEnumerator Disparo()
    {
        SpawnProjectil();
        yield return new WaitForSeconds(0.5f);
        estadoActual = EstadosAtaque.fase1;
        yield break;

    }
    IEnumerator ChangeShoot()
    {
        yield return new WaitForSeconds(1);
        SpawnSpider();
        yield return new WaitForSeconds(5);
        estadoActual = EstadosAtaque.shoot;
        yield break;

    }
}
