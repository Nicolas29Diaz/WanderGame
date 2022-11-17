using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class MiniBossArdilla : MonoBehaviour
{
    public float velocidad;
    public Animator animator;
    public Transform jugador;

    public GameObject ataque;
    public GameObject hit;
    public int melee;

    public float vida;
    public float cronometro;
    public int rutina;
    public bool atacando;

    public float maximoVida;

    public float dañoPuño = 4;
    public float dañoNuez = 5;

    CombatePersonaje personaje;

    public int contPuño = 0;
    public int contNuez = 0;

    public GameObject lanzanueces;

    public int instanciarLanzaNueces = 0;

    public void Comportamiento()
    {
        cronometro += 1 * Time.deltaTime;
        if (cronometro > 2)
        {
            cronometro = 0;
            rutina += 1;
        }
        
        if (Vector3.Distance(jugador.position, transform.position) <= 1 && rutina >= 1)
        {
            transform.position = transform.position;
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", true);
            ataque.GetComponent<BoxCollider>().enabled = false;
            atacando = true;
        }
        else if (Vector3.Distance(jugador.position, transform.position) <= 1 && rutina < 1)
        {
            transform.position = transform.position;
        }
        else if (Vector3.Distance(jugador.position, transform.position) < 5 && !atacando)
        {
            transform.LookAt(new Vector3(jugador.position.x, transform.position.y, jugador.position.z));
            transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
            animator.SetBool("Walk", true);
            animator.SetBool("Attack", false);
        }
    }

    public void Final_Animacion()
    {
        ataque.GetComponent<BoxCollider>().enabled = true;
        animator.SetBool("Attack", false);
        rutina = 0;
        atacando = false;
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
        vida = maximoVida;
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
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
            instanciarLanzaNueces++;
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", false);
            animator.SetBool("Dead", true);
            if(instanciarLanzaNueces == 1)
            {
                Instantiate(lanzanueces, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                
            }
            Destroy(gameObject, 6);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Puño")
        {
            contPuño++;
            if (contPuño > 1)
            {
                TomarDaño(dañoPuño);
                contPuño = 0;
            }
        }
        if (other.gameObject.tag == "Nuez")
        {
            contNuez++;
            if (contNuez > 1)
            {
                TomarDaño(dañoNuez);
                contNuez = 0;
                Destroy(other.gameObject);
            }


        }
    }
}
