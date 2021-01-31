using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    public void cambioEscena()
    {
        SceneManager.LoadScene(2);
        Debug.Log("Cambiaste de escena");
    }
}
