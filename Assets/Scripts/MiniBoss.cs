using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public float time_rutinas;
    public Animator animator;
    public Quaternion angulo;
    public float grado;
    public GameObject target;
    public bool atacando;
    public RangoMiniBoss rango;
    public float velocidad;
    public GameObject[] hit;
    public int hit_Select;
    public int melee;

    public float HP_Min;
    public float HP_Max;
    public bool muerto;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    public void Comportamiento_MiniBoss()
    {
        if (Vector3.Distance(target.transform.position, transform.position) < 15)
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);

            if (Vector3.Distance(target.transform.position, transform.position) > 1 )
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                transform.Translate(Vector3.right * velocidad * Time.deltaTime);
                animator.SetBool("Walk", true);

                //if (transform.rotation == rotation)
                //{
                //    transform.Translate(Vector3.right * velocidad * Time.deltaTime);
                //}

                animator.SetBool("Attack", false);

                cronometro += 1 * Time.deltaTime;
                if (cronometro > time_rutinas)
                {
                    rutina += 1;
                    cronometro = 0;
                }
            }
            else if (Vector3.Distance(target.transform.position, transform.position) < 1 && rutina > 0)
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
                atacando = true;
                GetComponent<CapsuleCollider>().enabled = false;
            }
        }
    }

    public void Final_Animacion()
    {
        rutina = 0;
        animator.SetBool("Attack", false);
        atacando = false;
        rango.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void ColliderAttackTrue()
    {
        hit[hit_Select].GetComponent<SphereCollider>().enabled = true;
    }

    public void ColliderAttackFalse()
    {
        hit[hit_Select].GetComponent<SphereCollider>().enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (HP_Min > 0)
        {
            Comportamiento_MiniBoss();
        }
        else
        {
            if (!muerto)
            {
                animator.SetTrigger("Dead");
                muerto = true;
            }
        }
    }
}
