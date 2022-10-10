using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArdillaEnemy : MonoBehaviour
{
    public float rangoDeVision;

    public Transform jugador;
    public Transform controladorDisparo;
    public GameObject nuez;

    public float distanciaFrenado;
    public float distanciaRayo;

    public float velocidad;
    public Transform controladorSueloFrenado;
    public bool infoSueloFrenado;

    private float tiempoEsperado;
    public float tiempoEsperaAtaque;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        tiempoEsperado = 0;
    }

    public void Disparar()
    {
        Instantiate(nuez, controladorDisparo.position, controladorDisparo.rotation);
    }

    private void OnTriggerStay(Collider other)
    {
        infoSueloFrenado = Physics.Raycast(controladorSueloFrenado.position, Vector3.down, distanciaRayo);
        if (other.CompareTag("Player"))
        {
            if (infoSueloFrenado == false)
            {
                animator.SetBool("walk", false);
                transform.position = transform.position;
            }
            else if (Vector3.Distance(transform.position, other.transform.position) == distanciaFrenado)
            {
                animator.SetBool("walk", false);
                transform.position = transform.position;
            }
            else if (Vector3.Distance(transform.position, other.transform.position) < distanciaFrenado)
            {
                gameObject.GetComponent<MovPlataforma>().patrullando = false;
                transform.Translate(Vector3.forward * -velocidad * Time.deltaTime);
            }


            transform.LookAt(new Vector3(jugador.position.x, transform.position.y, jugador.position.z));
            if (tiempoEsperado <= 0)
            {
                transform.position = transform.position;
                animator.SetBool("throw", true);
                animator.SetBool("walk", false);
                tiempoEsperado = tiempoEsperaAtaque;
            }
            else
            {
                tiempoEsperado -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.GetComponent<MovPlataforma>().patrullando = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(controladorSueloFrenado.transform.position, controladorSueloFrenado.transform.position + Vector3.down * distanciaRayo);
    }

    public void Final_throw()
    {
        animator.SetBool("throw", false);
    }
}
