using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManejadorInterfaz : MonoBehaviour
{
    public static string nombrepersonaje;
    public static string dialogoPersonaje;

    public Text nPersonajeText;
    public Text dialogoPersoanjeText;

    private void Start()
    {
        nPersonajeText.text = nombrepersonaje;
        dialogoPersoanjeText.text = dialogoPersonaje;
    }

    private void Update()
    {
        nPersonajeText.text = nombrepersonaje;
        dialogoPersoanjeText.text = dialogoPersonaje;
    }



}
