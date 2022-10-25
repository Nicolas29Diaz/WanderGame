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
        if(Vector3.Distance(target.transform.position, transform.position) < 15)
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);

            if(Vector3.Distance(target.transform.position, transform.position) > 1 && !atacando)
            {
                switch (rutina)
                {
                    case 0:
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        animator.SetBool("Walk", true);

                        if (transform.rotation == rotation)
                        {
                            transform.Translate(Vector3.right * velocidad * Time.deltaTime);
                        }

                        animator.SetBool("Attack", false);

                        cronometro += 1 * Time.deltaTime;
                        if(cronometro > time_rutinas)
                        {
                            rutina = Random.Range(0, 2);
                            cronometro = 0;
                        }
                        break;
                    case 1:
                        animator.SetBool("Walk", false);
                        animator.SetBool("Attack", true);
                        animator.SetFloat("Skills", 0);
                        rango.GetComponent<CapsuleCollider>().enabled = false;
                        break;
                }
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
        if(HP_Min > 0)
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
