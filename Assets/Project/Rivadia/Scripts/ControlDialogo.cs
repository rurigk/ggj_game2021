using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDialogo : MonoBehaviour
{

    private Queue<string> sentencias;
    public Dialogo dialogo;

    // Start is called before the first frame update
    void Start()
    {
        sentencias = new Queue<string>();
        IniciarDialogo(dialogo);
    }


    public void IniciarDialogo(Dialogo dialogo)
    {

        ManejadorInterfaz.nombrepersonaje = dialogo.personaje.nombrePersonaje;
        sentencias.Clear();

        foreach(Linea d in dialogo.lineas)
        {
            sentencias.Enqueue(d.linea);
        }

        SiguienteDialogo();
    }

    private void SiguienteDialogo()
    {
        if (sentencias.Count == 0)
        {
            return;
        }

        string sentencia = sentencias.Dequeue();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
