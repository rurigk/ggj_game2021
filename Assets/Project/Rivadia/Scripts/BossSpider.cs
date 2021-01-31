using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum  EstadosAtaque 
{
    idle,
    fase1,
    shoot,
    shoot2,
    move1,
    move2,
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

    public Waypoints waypoints;

    public float velocity;
    public Transform waypointTarget;

    public Animator anim;

    public bool look;

    public Transform player;

    public AnimationClip[] animations;

    public Transform añara;
    // Start is called before the first frame update
    void Start()
    {
        velocity = 3;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        waypoints = GameObject.Find("Waypoints").GetComponent<Waypoints>();
        
        estadoActual = EstadosAtaque.idle;
        
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (look)
        {
            añara.localEulerAngles = new Vector3(0, 180, 0);
            Vector3 lookAtTarget = new Vector3(player.position.x, this.transform.position.y, player.position.z);

            transform.LookAt(lookAtTarget);
        }



        CambiarAtaque();
    }

    void CambiarAtaque()
    {
        switch (estadoActual)
        {
            case EstadosAtaque.idle:
                estadoActual = EstadosAtaque.move1;
                break;
            case EstadosAtaque.fase1:
                StartCoroutine(ChangeShoot(EstadosAtaque.shoot));
                
                StartCoroutine(ChangeMoveFase(EstadosAtaque.move2,8));
                //look = true;
                break;
            case EstadosAtaque.shoot:
                StartCoroutine(Disparo(EstadosAtaque.fase1));
                break;
            case EstadosAtaque.Fase2:
                StartCoroutine(ChangeShoot(EstadosAtaque.shoot2));
                break;
            case EstadosAtaque.shoot2:
                StartCoroutine(Disparo(EstadosAtaque.Fase2));
                break;
            case EstadosAtaque.fase3:
                Debug.Log("Aplasta");
                break;
            case EstadosAtaque.Muerte:
                Debug.Log("Muere");
                break;
            case EstadosAtaque.move1:
                StartCoroutine(AnimationCorr(3, "walk", 0,EstadosAtaque.fase1));
                break;
            case EstadosAtaque.move2:
                Debug.Log("estas en move dos");
                StartCoroutine(AnimationCorr(3,"walk2",1,EstadosAtaque.Fase2));
                break;



        }
    }

    public void SpawnProjectil()
    {
        GameObject projectilPos=Instantiate(projectil)as GameObject;
        ;
        projectilPos.transform.position = mouth.position;
        projectilPos.transform.rotation=Quaternion.identity;
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

    IEnumerator Disparo(EstadosAtaque e)
    {
        SpawnProjectil();
        yield return new WaitForSeconds(0.5f);
        estadoActual = e;
        yield break;

    }
    IEnumerator ChangeShoot(EstadosAtaque e)
    {
        yield return new WaitForSeconds(1);
        SpawnSpider();
        yield return new WaitForSeconds(5);
        estadoActual = e;
        yield break;

    }


    void Move()
    {
        Vector3 wayPos = waypointTarget.position - this.transform.position;

        Debug.Log(wayPos.magnitude);

        if (wayPos.magnitude > 0.05f)
        {
            this.transform.Translate(wayPos.normalized * velocity * Time.deltaTime, Space.World);
        }

        
    }


    void PlayAnimation(string ani)
    {
        anim.Play(ani);
    }

    IEnumerator AnimationCorr(float timeWait,string firtsAni,int contador, EstadosAtaque e)
    {
        yield return new WaitForSeconds(timeWait);
        PlayAnimation(firtsAni);
        yield return new WaitForSeconds(animations[contador].length);
        estadoActual = e;
        anim.enabled = false;
        look = true;
        yield return null;

    }

    IEnumerator ChangeMoveFase(EstadosAtaque e,float time)
    {
        yield return new WaitForSeconds(time);
        estadoActual = e;
        look = false;
        anim.enabled = true;
        yield break;
    }

}
