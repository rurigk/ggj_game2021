using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDialogo : MonoBehaviour
{


    
    //Crea cola de dialogo
    private Queue<string> sentencias;

   
    //Recibe el scritableObject de los dialogos
    public Dialogo dialogo;

    public int contadorDialogo;
    // Start is called before the first frame update
    void Start()
    {
        sentencias = new Queue<string>();
        IniciarDialogo(dialogo);
    }


    // Inicia la carga y limpieza de cola de los dialogos
    public void IniciarDialogo(Dialogo dialogo)
    {
        ManejadorInterfaz.nombrepersonaje = dialogo.lineas[contadorDialogo].personaje.nombrePersonaje;
        contadorDialogo = 0;
        sentencias.Clear();
      
        

        foreach(Linea d in dialogo.lineas)
        {
            sentencias.Enqueue(d.linea);
        }


        string sentencia = sentencias.Dequeue();
        ManejadorInterfaz.dialogoPersonaje = sentencia;
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
            return;
        }
        //cambioPersonaje();
        string sentencia = sentencias.Dequeue();
        
        ManejadorInterfaz.nombrepersonaje = dialogo.lineas[contadorDialogo].personaje.nombrePersonaje;
        ManejadorInterfaz.dialogoPersonaje = sentencia;

    }


    // Update is called once per frame
    void Update()
    {
       
    }

    

}
