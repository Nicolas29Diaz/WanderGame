using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArdillaMeleeEnemy : MonoBehaviour
{
    public float distanciaPerseguir;
    public float distanciaAtaque;
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
        animator.SetBool("run", false);
        infoSueloFrenado = Physics.Raycast(controladorSueloFrenado.position, Vector3.down, distanciaRayo);
        if (other.CompareTag("Player"))
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            if (infoSueloFrenado == false)
            {
                animator.SetBool("walk", false);
                animator.SetBool("run", false);
                transform.position = transform.position;
            }
            else if (Vector3.Distance(transform.position, player.transform.position) < distanciaPerseguir)
            {
                animator.SetBool("run", true);
                gameObject.GetComponent<MovPlataforma>().patrullando = false;
                transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
            }
            
            if (Vector3.Distance(transform.position, player.transform.position) < distanciaAtaque)
            {
                Debug.Log("estoy atacando");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.GetComponent<MovPlataforma>().patrullando = true;
    }

}
