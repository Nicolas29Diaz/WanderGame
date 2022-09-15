using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [Header("RigidBody")]
    public Rigidbody rb;

    [Header("Saltar")]
    //Para saltar
    public float jumpForce = 2f;
    public bool doubleJump;
    public bool saltando;

    [Header("Tocar Piso")]
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Animaciones")]
    public Animator animator;

    [Header("Transform")]
    public Transform mapache;

    //Escala original mapache
    private Vector3 scale;

    [Header("Movimiento")]
    //Movimiento mapache
    float verX,verY;
    public bool runing = true;
    public float speed = 8f;
    public float speedEscalando = 4f;
    private float speedInicial;


    [Header("'Mejorar' salto")]
    public bool betterJump = false;
    public float fallMultiplier = 0.5f;
    public float lowJumpMult = 1f;

    [Header("Trepar")]
    public float velocidadEscalar;
    private BoxCollider boxCollider;
    private float gravedadInicial;
    public bool escalando;
    public float globalGravity = -9.81f;
    public bool tocandoLayerEscaleras;
    public float ultimoVerX;
    public bool escaleraFrente;

    [Header("Pegar")]
    public bool pegando = false;
    public bool cambiarMano = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        scale = mapache.localScale;

        boxCollider = GetComponent<BoxCollider>();
        rb.useGravity = false;
        gravedadInicial = globalGravity;

        speedInicial = speed;
    }

    // Update is called once per frame
    void Update()
    {
        verX = Input.GetAxisRaw("Horizontal"); //Obtener eje horizontal

        verY = Input.GetAxisRaw("Vertical"); //Obtener eje vertical

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer); //Saber si toca el piso
    
        animator.SetBool("isGrounded", isGrounded);

        //PARA SALTAR
        if (Input.GetKeyDown(KeyCode.Space) && !pegando)
        {
            saltando = true;

            if (isGrounded)
            {
                doubleJump = true;
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                animator.SetBool("salto", true);
                
            }
            else
            {

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (doubleJump)
                    {
                        animator.SetBool("doubleJump", true);
                        rb.velocity = Vector3.zero;
                        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                        doubleJump = false;

                    }

                }
                

            }

        }
        else
        {
            saltando = false;
            animator.SetBool("doubleJump", false);
            animator.SetBool("salto", false);
        }

        //Salto "Mejor"
        if (betterJump)
        {
            if(rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier) * Time.deltaTime;
            }
            if(rb.velocity.y < 0 && !Input.GetKey(KeyCode.Space))
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMult) * Time.deltaTime;
            }
        }


        if (Input.GetKeyDown(KeyCode.C) && !saltando && !pegando && isGrounded)//!isGrounded)
        {
            

            animator.SetBool("cambiarMano", cambiarMano);

            cambiarMano = !cambiarMano;

            animator.SetBool("Pegar", true);

            pegando = true;

            


        }
        //else
        //{
        //    pegando = false;
        //}

        

        }

    private void FixedUpdate()
    {
        animator.SetFloat("VelY", verY);
        //Para manipular la gravedad    
        Vector3 gravity = globalGravity * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);

        //Movimiento
        if (runing && !pegando)
        {
            Vector3 movimiento = new Vector3(verX, 0, 0);

            movimiento.Normalize();
          
            transform.position += (transform.forward * movimiento.x * speed * Time.deltaTime);

            animator.SetFloat("Velx", verX);

            //Para rotar el mapache
            if (verX < 0)
            {
                mapache.localScale = new Vector3(scale.x, scale.y, -scale.z);
            }
            else if (verX > 0)
            {
                mapache.localScale = new Vector3(scale.x, scale.y, scale.z);
            }
        }
        
        Escalar();

    }

    private void Escalar()
    {

        if (escaleraFrente)
        {
            animator.SetBool("escalando2", escalando);
        }
        else
        {
            animator.SetBool("escalando", escalando);
        }
        if ((verY != 0 || escalando) && tocandoLayerEscaleras)
        {
            Vector3 velocidadSubida = new Vector3(rb.velocity.x, verY * velocidadEscalar);
            rb.velocity = velocidadSubida;
            globalGravity = 0f;
            escalando = true;
            speed = speedEscalando;
            
        }
        else
        {
            speed = speedInicial;
            globalGravity = gravedadInicial;
            escalando = false;
        }

        if (isGrounded)
        {
            escalando = false;
        }

       
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Escaleras"))
        //{
        //    Debug.Log("Touched a rail");
        //}
        if(other.gameObject.layer == 6 && other.gameObject.CompareTag("Escaleras"))
        {
            tocandoLayerEscaleras = true;
            ultimoVerX = verX;
            escaleraFrente = false;


        }
        else if(other.gameObject.layer == 6 && other.gameObject.CompareTag("Escaleras2"))
        {
            tocandoLayerEscaleras = true;
            escaleraFrente = true;
        }
        
    
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            tocandoLayerEscaleras = false;
        }
    }



    public void dejarPegar()
    {
        pegando = false;
        animator.SetBool("Pegar", false);
    }
}
