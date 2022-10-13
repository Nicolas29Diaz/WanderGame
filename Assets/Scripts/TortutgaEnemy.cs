using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TortutgaEnemy : MonoBehaviour
{
    public float distanciaBloqueo;    

    public GameObject player;

    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            if (Vector3.Distance(transform.position, player.transform.position) < distanciaBloqueo)
            {
                gameObject.GetComponent<MovPlataforma>().patrullando = false;
                transform.position = transform.position;
                animator.SetBool("walk", false);
            }
            else
            {
                animator.SetBool("walk", true);
                gameObject.GetComponent<MovPlataforma>().patrullando = true;
            }

        }
    }
}
