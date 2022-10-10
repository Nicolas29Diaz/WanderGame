using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArdillaBoss : MonoBehaviour
{
    public Transform jugador;
    private Animator animator;
    public float velocidad;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.LookAt(new Vector3(jugador.position.x, transform.position.y, jugador.position.z));
    }

    public void Salto()
    {
        transform.Translate(Vector3.up* 2 * Time.deltaTime);
        animator.SetBool("jump", true);
    }

    public void SalirSalto()
    {
        animator.SetBool("jump", false);
    }
}
