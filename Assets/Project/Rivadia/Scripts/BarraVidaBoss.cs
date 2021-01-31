using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVidaBoss : MonoBehaviour
{

    public Slider barraVida;
    // Start is called before the first frame update
    void Start()
    {
        barraVida.value = VidaBoss.vidaBoss;
    }

    // Update is called once per frame
    void Update()
    {
        barraVida.value = VidaBoss.vidaBoss;
    }
}
