using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossArdilla : MonoBehaviour
{
    public float cronometro;
    public float time_rutinas;
    public Animator animator;
    public Transform jugador;

    public float velocidad;
    public GameObject[] hit;
    public int hit_Select;
    public int melee;

    public float vida;

    public void Comportamiento()
    {
        transform.LookAt(new Vector3(jugador.position.x, transform.position.y, transform.position.z));
        if (Vector3.Distance(jugador.position, transform.position) < 15)
        {
            transform.Translate(jugador.position);
            animator.SetBool("Walk", true);
            animator.SetBool("Attack", false);
            if (Vector3.Distance(jugador.position, transform.position) < 1)
            {
                melee = Random.Range(0, 4);
                switch (melee)
                {
                    case 0:
                        animator.SetFloat("Skills", 0);
                        hit_Select = 0;
                        break;
                    case 1:
                        animator.SetFloat("Skills", 0.5f);
                        hit_Select = 1;
                        break;
                    case 2:
                        animator.SetFloat("Skills", 1);
                        hit_Select = 2;
                        break;
                }

                animator.SetBool("Walk", false);
                animator.SetBool("Attack", true);
                GetComponent<CapsuleCollider>().enabled = false;
            }
        }

    }

    public void Final_Animacion()
    {
        animator.SetBool("Attack", false);
    }

    public void ColliderAttackTrue()
    {
        hit[hit_Select].GetComponent<SphereCollider>().enabled = true;
    }

    public void ColliderAttackFalse()
    {
        hit[hit_Select].GetComponent<SphereCollider>().enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
