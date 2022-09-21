using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatePersonaje : MonoBehaviour
{
    MovementPlayer movPlayer;
    public int vidaPersonaje = 100;

    [Header("Disapro")]
    public Transform controladorDisparo;
    public Transform controladorDisparoArriba;
    public Transform controladorDisparoAbajo;
    public GameObject nuez;



    // Start is called before the first frame update
    void Start()
    {
        movPlayer = GetComponent<MovementPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        //PEGAR
        if (Input.GetKeyDown(KeyCode.C) && !movPlayer.saltando && !movPlayer.pegando && movPlayer.isGrounded)
        {
            PegarMelee();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Disparar();
        }


    }


    public void PegarMelee()
    {
        movPlayer.animator.SetBool("cambiarMano", movPlayer.cambiarMano);

        movPlayer.cambiarMano = !movPlayer.cambiarMano;

        movPlayer.animator.SetBool("Pegar", true);

        movPlayer.pegando = true;

    }
    public void dejarPegar()
    {
        movPlayer.pegando = false;
        movPlayer.animator.SetBool("Pegar", false);
    }


    public void Disparar(){

        if (movPlayer.escalando)
        {

            if (movPlayer.mirandoArriba && movPlayer.mirandoDerecha) 
            {
                Instantiate(nuez, controladorDisparoArriba.position, controladorDisparoArriba.rotation);
            }
            else if (movPlayer.mirandoArriba && !movPlayer.mirandoDerecha) 
            {
                Instantiate(nuez, controladorDisparoArriba.position, controladorDisparoArriba.rotation * Quaternion.Euler(180f, 0f, 0f));//Rotar la instancia(por culpa del scale)
            }
            //else if (!movPlayer.mirandoArriba && movPlayer.mirandoDerecha)
            //{
            //    Instantiate(nuez, controladorDisparoAbajo.position, controladorDisparoAbajo.rotation);//Rotar la instancia(por culpa del scale)
            //}
            //else if (!movPlayer.mirandoArriba && !movPlayer.mirandoDerecha)
            //{
            //    Instantiate(nuez, controladorDisparoAbajo.position, controladorDisparoAbajo.rotation * Quaternion.Euler(180f, 0f, 0f));//Rotar la instancia(por culpa del scale)
            //}

        }
        else
        {
            //Quaternion r = Quaternion.Inverse(controladorDisparo.rotation);

            //if (!movPlayer.mirandoDerecha && !movPlayer.mirandoArriba)
            //{
            //    Instantiate(nuez, controladorDisparo.position, controladorDisparo.rotation * Quaternion.Euler(0f, 180f, 0f));
            //    Debug.Log("MirandoIzqAbajo");
            //}
            //else if (movPlayer.mirandoDerecha && !movPlayer.mirandoArriba)
            //{
            //    Instantiate(nuez, controladorDisparo.position, controladorDisparo.rotation);
            //    Debug.Log("MirandoDerechaAbajo");

            //}else if(movPlayer.mirandoDerecha && movPlayer.mirandoArriba)
            //{
            //    Instantiate(nuez, controladorDisparoArriba.position, controladorDisparoArriba.rotation);
            //    Debug.Log("MirandoDerechaArriba");
            //}
            //else if(!movPlayer.mirandoDerecha && movPlayer.mirandoArriba)
            //{
            //     Instantiate(nuez, controladorDisparoArriba.position, controladorDisparoArriba.rotation * Quaternion.Euler(180f, 0f, 0f));
            //    Debug.Log("MirandoIzqA¡Arriba");
            //}
            if (movPlayer.mirandoArriba && movPlayer.mirandoDerecha)
            {
                Instantiate(nuez, controladorDisparoArriba.position, controladorDisparoArriba.rotation * Quaternion.Euler(0f, 180f, 0f));
                //Debug.Log("MirandoArribaDerecha");
                
            }
            else if (movPlayer.mirandoArriba && movPlayer.mirandoIzq)
            {
                Instantiate(nuez, controladorDisparoArriba.position, controladorDisparoArriba.rotation);
                //Debug.Log("MirandoArribaIzquierda");
            }






        }






    }
}
