using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArdillaEnemy : MonoBehaviour
{

    public float rangoDeVision;
    public LayerMask capaJugador;
    public Transform jugador;
    public Transform controladorDisparo;
    public GameObject nuez;

    private bool alerta;

    private float tiempoEsperado;
    public float tiempoEsperaAtaque = 3;


    // Start is called before the first frame update
    void Start()
    {
        tiempoEsperado = 0;
    }

    // Update is called once per frame
    void Update()
    {
        alerta = Physics.CheckSphere(transform.position, rangoDeVision, capaJugador);

        if (alerta)
        {
            transform.LookAt(new Vector3 (jugador.position.x, transform.position.y, jugador.position.z));
            // transform.LookAt(jugador);
            if (tiempoEsperado <= 0)
            {
                Disparar();
                tiempoEsperado = tiempoEsperaAtaque;
            } else
            {
                tiempoEsperado -= Time.deltaTime;
            }
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoDeVision);
    }

    public void Disparar()
    {
        Instantiate(nuez, controladorDisparo.position, controladorDisparo.rotation);
    }
}
