using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Vida : MonoBehaviour
{
    private float vida;
    private float maximoVida;
    
    public void Start()
    {
        vida = maximoVida;
    }

    public void tomarDaño (float daño)
    {
        vida -= daño;
        if (vida < 0)
        {
            barraDeVida.CambiarVidaActual(vida);
            Destroy(GameObject);
        }

    }
}
