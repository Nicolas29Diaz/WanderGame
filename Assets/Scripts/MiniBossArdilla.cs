using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossArdilla : MonoBehaviour
{
    public float velocidad;
    public Animator animator;
    public Transform jugador;

    public GameObject hit;
    public int melee;

    public float vida;

    public void Comportamiento()
    {
        transform.LookAt(new Vector3(jugador.position.x, transform.position.y, jugador.position.z));
        if (Vector3.Distance(jugador.position, transform.position) < 15)
        {
            transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
            animator.SetBool("Walk", true);
            animator.SetBool("Attack", false);
           
        }
        else if (Vector3.Distance(jugador.position, transform.position) <= 5)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", true);
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            transform.position = transform.position;
            melee = Random.Range(0, 4);
            switch (melee)
            {
                case 0:
                    animator.SetFloat("Skills", 0);
                    break;
                case 1:
                    animator.SetFloat("Skills", 0.5f);
                    break;
                case 2:
                    animator.SetFloat("Skills", 1);
                    break;
            }
        }

    }

    public void Final_Animacion()
    {
        animator.SetBool("Attack", false);
    }

    public void ColliderAttackTrue()
    {
        hit.GetComponent<SphereCollider>().enabled = true;
    }

    public void ColliderAttackFalse()
    {
        hit.GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vida > 0)
        {
            Comportamiento();
        }
        else 
        { 
            animator.SetTrigger("Dead");
            Destroy(gameObject);
        }
    }
}
