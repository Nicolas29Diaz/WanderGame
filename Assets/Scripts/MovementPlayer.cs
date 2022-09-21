using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [Header("RigidBody")]
    public Rigidbody rb;

    [Header("Saltar")]
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
    public float verX, verY;
    public bool puedoMover = true;
    public float speed = 8f;
    public float speedEscalando = 4f;
    private float speedInicial;

    [Range(0,0.3f)]public float velSuavi;
    public Vector3 vel = Vector3.zero;

    public bool mirandoDerecha = true;
    public bool mirandoArriba;

    [Header("'Mejorar' salto")]
    public bool betterJump = false;
    public float fallMultiplier = 0.5f;
    public float lowJumpMult = 1f;

    [Header("Gravedad")]
    public float globalGravity = -9.81f;
    private float gravedadInicial;

    [Header("Trepar")]
    public float velocidadEscalar;
    private BoxCollider boxCollider;
    public bool escalando;

    public bool tocandoLayerEscaleras;
    public float ultimoVerX;
    public bool escaleraFrente;

    [Header("Rebotar recibir da�o")]
    public Vector3 velocidadRebote;

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


        //TOCO PISO
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);

        //SALTAR
        if (Input.GetKeyDown(KeyCode.Space) && !pegando)
        {
            Saltar();
        }
        else
        {
            saltando = false;
            animator.SetBool("doubleJump", false);
            animator.SetBool("salto", false);
        }

        

    }

    private void FixedUpdate()
    {

        //GRAVEDAD 
        Vector3 gravity = globalGravity * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);

        //MOVIMIENTO
        if (puedoMover && !pegando)
        {
            MoverPersonaje();
            //MoverPersonaje2(verX * Time.fixedDeltaTime); 
        }

        //ESCALAR
        animator.SetFloat("VelY", verY);
        Escalar();

    }

    //FUNCIONES
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

            if(verY > 0)
            {
                mirandoArriba = true;
            }
            else if(verY < 0)
            {
                mirandoArriba = false;
            }

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

    public void Saltar()
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


        //Salto "Mejor"
        if (betterJump)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier) * Time.deltaTime;
            }
            if (rb.velocity.y < 0 && !Input.GetKey(KeyCode.Space))
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMult) * Time.deltaTime;
            }
        }
    }

    public void MoverPersonaje()
    {

        Vector3 movimiento = new Vector3(verX, 0, 0);

        movimiento.Normalize();

        transform.position += (transform.forward * movimiento.x * speed * Time.deltaTime);

        animator.SetFloat("Velx", verX);

        //Para rotar el mapache
        if (verX < 0)
        {
            mapache.localScale = new Vector3(scale.x, scale.y, -scale.z);
            mirandoDerecha = false;
            //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
        else if (verX > 0)
        {
            mapache.localScale = new Vector3(scale.x, scale.y, scale.z);
            mirandoDerecha = true;
        }

    }
    public void MoverPersonaje2(float mover)
    {
        animator.SetFloat("Velx", verX);
        Vector3 velocidadObj = new Vector3(mover, rb.velocity.y, 0);

        rb.velocity = Vector3.SmoothDamp(rb.velocity.normalized, velocidadObj, ref vel, velSuavi);

        if(mover > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if(mover < 0 && mirandoDerecha)
        {
            Girar();
        }
    }

    public void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);


        //Vector3 escala = transform.localScale;
        //escala.z *= -1;
        //transform.localScale = escala;
    }


    //public void rebote(Vector3 puntoGolpe)
    //{
    //    rb.velocity = new Vector3(-velocidadRebote.x * puntoGolpe, velocidadRebote.y, 0);
    //}

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 6 && other.gameObject.CompareTag("Escaleras"))
        {
            tocandoLayerEscaleras = true;
            ultimoVerX = verX;
            escaleraFrente = false;

        }
        else if (other.gameObject.layer == 6 && other.gameObject.CompareTag("Escaleras2"))
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
}
