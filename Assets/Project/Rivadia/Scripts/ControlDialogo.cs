using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ControlDialogo : MonoBehaviour
{

    public GameObject[] cameras;
    public GameObject panel;

    public Text nombreText;
    public Text dialogoText;

  
    //Crea cola de dialogo
    private Queue<string> sentencias;

   
    //Recibe el scritableObject de los dialogos
    public Dialogo dialogo;
    

    public int contadorDialogo;

    public UnityEvent nuevoEvento;
    // Start is called before the first frame update
    void Start()
    {
       
       
        sentencias = new Queue<string>();
        IniciarDialogo(dialogo);
    }


    // Inicia la carga y limpieza de cola de los dialogos
    public void IniciarDialogo(Dialogo dialogo)
    {
        
        nombreText.text= dialogo.lineas[contadorDialogo].personaje.nombrePersonaje;
        contadorDialogo = 0;
        sentencias.Clear();
      
        

        foreach(Linea d in dialogo.lineas)
        {
            sentencias.Enqueue(d.linea);
        }


        string sentencia = sentencias.Dequeue();
        dialogoText.text = sentencia;
    }


    //Libera de la cola el siguiente dialogo 
    public void SiguienteDialogo()
    {
        if (contadorDialogo!=dialogo.lineas.Length)
        {
            contadorDialogo++;
        }
            
        
        if (sentencias.Count == 0)
        {
           cameras[0].SetActive(true);
           cameras[1].SetActive(false);
           nuevoEvento.Invoke();
           panel.SetActive(false);
            return;
        }
        //cambioPersonaje();
        string sentencia = sentencias.Dequeue();
        
        nombreText.text = dialogo.lineas[contadorDialogo].personaje.nombrePersonaje;
        dialogoText.text = sentencia;

    }


    // Update is called once per frame
    void Update()
    {
       
    }





}
