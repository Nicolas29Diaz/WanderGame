using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCharacter : MonoBehaviour
{

    public CharacterController controller;
    public Rigidbody rb;
    private Vector3 direction;

    //Para saltar
    public float speed = 8f;
    public float jumpForce = 2f;
    public float gravity = -10;

    //Para saber si toca piso
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;

    //Doble salto
    private bool doubleJump;

    //Animaciones
    public Animator animator;

    public Transform mapache;

    Vector3 scale;

    float verX;
    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //float hInput = Input.GetAxis("Horizontal");
        //direction.x = hInput * speed;
        //controller.Move(direction * Time.deltaTime);
        ////animator.SetFloat("Speed", Mathf.Abs(hInput));
        //animator.SetFloat("Velx", hInput);


        ////direction.y += gravity * Time.deltaTime;
        //if (isGrounded)
        //{
        //    doubleJump = true;
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        //direction.y = jumpForce;

        //        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        //    }

        //}
        //else
        //{

        //    if (Input.GetButtonDown("Jump") && doubleJump)
        //    {
        //        doubleJump = false;
        //        //direction.y = jumpForce;
        //        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        //    }
        //}

        //if (hInput != 0)
        //{
        //    Quaternion newRotation = Quaternion.LookRotation(new Vector3(hInput, 0,0));
        //    mapache.rotation = newRotation;
        //}

        //Obtener coordenada en x
        verX = Input.GetAxisRaw("Horizontal");


        //Para saltar
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        if (isGrounded)
        {
            doubleJump = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && doubleJump)
            {
                doubleJump = false;
                rb.AddForce(new Vector3(0, jumpForce - 1, 0), ForceMode.Impulse);
            }
        }


    }

    private void FixedUpdate()
    {
        Vector3 movimiento = new Vector3(verX, 0, 0);
        movimiento.Normalize();

        transform.position += (transform.forward * movimiento.x * speed * Time.deltaTime);
        animator.SetFloat("Velx", verX);

        if (verX < 0)
        {
            transform.localScale = new Vector3(scale.x, scale.y, -scale.z);
        }
        else if (verX > 0)
        {
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
    }
}
