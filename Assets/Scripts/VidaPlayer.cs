using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaPlayer : MonoBehaviour
{
    public float vida;
    public float maximoVida;
    public BarraDeVida barraDeVida;
    public GameObject menuPsusa;
    private bool menuOn;
    // Start is called before the first frame update
    void Start()
    {
        vida = maximoVida;
        barraDeVida.InicializarVida(vida);
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        barraDeVida.CambiarVidaActual(vida);
        if(vida == 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Interfaz Final");
        }
    }
}
