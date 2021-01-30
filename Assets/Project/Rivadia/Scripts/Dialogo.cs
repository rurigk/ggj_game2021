using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "nuevoDialogo", menuName = "nuevo Dialogo")]
public class Dialogo : ScriptableObject

{
    
    public Linea[] lineas;

}

[System.Serializable]
public class Linea
{
    public Personaje personaje;

    [TextArea(3, 10)]
    public string linea;
}
