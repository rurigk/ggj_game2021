using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaBoss : MonoBehaviour
{
    public static int vidaBoss;

    public int vidaActual;

    public GameObject mCamara;
    public GameObject cameraFinal;
    public Transform target;
    public GameObject panel;
    public GameObject cross;
    public GameObject barraVida;

    public bool onCamera;
    public int numeroEscena;
    private void Awake()
    {
        vidaBoss = 10;
        vidaActual = vidaBoss;
    }


    private void LateUpdate()
    {
        vidaActual = vidaBoss;
        if (onCamera)
        {
            cameraFinal.transform.LookAt(target.position);
           
        }
        
    }

    private void Update()
    {
        Derrota();
    }

    void Derrota()
    {
        if (vidaActual <= 0)
        {
            vidaBoss = 0;
            mCamara.SetActive(false);
            cameraFinal.SetActive(true);
            GetComponentInParent<BossSpider>().enabled = false;
            GetComponentInParent<Animator>().enabled = false;
            GameObject[] microAraña = GameObject.FindGameObjectsWithTag("spiderMicro");
            foreach (var m in microAraña)
            {
                m.SetActive(false);
            }
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerController>().LockPlayer();
            onCamera = true;
            cross.SetActive(false);
            barraVida.SetActive(false);
            panel.SetActive(true);
          
            StartCoroutine(CambiarEscena(numeroEscena));
        }
    }

    IEnumerator CambiarEscena(int escene)
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Cambiaste de escena");
        SceneManager.LoadScene(escene);
        yield break;
    }
}
