using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaPlayer : MonoBehaviour
{
    public float vida;
    public float maximoVida;
    public BarraDeVida barraDeVida;

    // Start is called before the first frame update
    void Start()
    {
        vida = maximoVida;
        barraDeVida.InicializarVida(vida);
    }

    public void TomarDa�o(float da�o)
    {
        vida -= da�o;
        barraDeVida.CambiarVidaActual(vida);
        if(vida == 0)
        {
            Destroy(gameObject);
        }
    }
}
