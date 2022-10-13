using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArdillaMeleeEnemy : MonoBehaviour
{
    public float distanciaPerseguir;
    public float velocidad;
    public bool infoSueloFrenado;

    public GameObject player;
    public Transform controladorSueloFrenado;
    public float distanciaRayo;

    private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }


    private void OnTriggerStay(Collider other)
    {
        // animator.SetBool("run", false);
        infoSueloFrenado = Physics.Raycast(controladorSueloFrenado.position, Vector3.down, distanciaRayo);
        if (other.CompareTag("Player"))
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, transform.position.z));
            if (infoSueloFrenado == false)
            {
                transform.position = transform.position;
                animator.SetBool("walk", false);
                animator.SetBool("run", false);
            }
            else if (Vector3.Distance(transform.position, player.transform.position) < distanciaPerseguir)
            {
                gameObject.GetComponent<MovPlataforma>().patrullando = false;
                transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
                animator.SetBool("run", true);
                animator.SetBool("walk", false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.GetComponent<MovPlataforma>().patrullando = true;
        animator.SetBool("walk", true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<VidaPlayer>().TomarDa�o(20);
        }
    }

}
