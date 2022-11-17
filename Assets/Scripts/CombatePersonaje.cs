using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatePersonaje : MonoBehaviour
{
    MovementPlayer movPlayer;

    [Header("Disapro")]
    public Transform controladorDisparo;
    public Transform controladorDisparoArriba;
    public Transform controladorDisparoAbajo;
    public GameObject nuez;

    public bool cambioModoArma;
    public bool lanzaNuecesEncontrado;
    public bool activarlanzaNueces;
    public GameObject lanzaNueces;

    public BoxCollider puño1;
    public BoxCollider puño2;

    public int cantidadNueces = 10;

    // Start is called before the first frame update
    void Start()
    {
        movPlayer = GetComponent<MovementPlayer>();
        cambioModoArma = false;
        lanzaNuecesEncontrado = false;
        movPlayer.puedoEscalar = true;
        activarlanzaNueces = false;

        DesactivarCollidersArma();
    }

    // Update is called once per frame
    void Update()
    {
        //PEGAR
        if (Input.GetKeyDown(KeyCode.C) && !movPlayer.saltando && !movPlayer.pegando && movPlayer.isGrounded && !cambioModoArma)
        {
            PegarMelee();
        }   

        if (Input.GetKeyDown(KeyCode.Alpha1) && lanzaNuecesEncontrado && !movPlayer.escalando)
        {
            cambioModoArma = !cambioModoArma;
            movPlayer.puedoEscalar = !movPlayer.puedoEscalar;
            activarlanzaNueces = !activarlanzaNueces;
            lanzaNueces.SetActive(activarlanzaNueces);
        }

        if (cambioModoArma && lanzaNuecesEncontrado)
        {
            //Debug.Log("Disparo");
            movPlayer.animator.SetLayerWeight(0, 0);
            movPlayer.animator.SetLayerWeight(1, 1);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("Nueces: " + cantidadNueces);
                if(cantidadNueces > 0)
                {
                    Disparar();
                    cantidadNueces -= 1;

                }
                
            }


        }
        else
        {
            //Debug.Log("Arma");
            movPlayer.animator.SetLayerWeight(0, 1);
            movPlayer.animator.SetLayerWeight(1, 0);
        }

    }


    public void PegarMelee()
    {
       

        movPlayer.animator.SetBool("cambiarMano", movPlayer.cambiarMano);

        movPlayer.cambiarMano = !movPlayer.cambiarMano;

        movPlayer.animator.SetBool("Pegar", true);

        movPlayer.pegando = true;

        ActivarCollidersArma(movPlayer.cambiarMano);

    }
    public void dejarPegar()
    {
        movPlayer.pegando = false;
        movPlayer.animator.SetBool("Pegar", false);
        DesactivarCollidersArma();
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
            //DISPARAR HACIA ARRIBA
            if (movPlayer.mirandoArriba && movPlayer.mirandoDerecha && movPlayer.quieto)
            {

                Instantiate(nuez, controladorDisparoArriba.position, controladorDisparoArriba.rotation );
                
            }
            else if (movPlayer.mirandoArriba && movPlayer.mirandoIzq && movPlayer.quieto)
            {

                Instantiate(nuez, controladorDisparoArriba.position, controladorDisparoArriba.rotation * Quaternion.Euler(0f, 180f, 0f));
                
            }

            //DISPARAR HACIA LOS LADOS
            else if (movPlayer.mirandoIzq && !movPlayer.mirandoArriba && !movPlayer.mirandoAbajo)
            {

                Instantiate(nuez, controladorDisparo.position, controladorDisparo.rotation * Quaternion.Euler(180f, 0f, 0f));
         
            }
            else if (movPlayer.mirandoDerecha &&  !movPlayer.mirandoArriba && !movPlayer.mirandoAbajo)
            {

                Instantiate(nuez, controladorDisparo.position, controladorDisparo.rotation);

            }

            //DISPARAR DIAGONAL
            else if (movPlayer.mirandoIzq && !movPlayer.quieto && movPlayer.mirandoArriba && !movPlayer.mirandoAbajo)
            {

                Instantiate(nuez, controladorDisparo.position, controladorDisparo.rotation * Quaternion.Euler(-160f, 0f, 0f));

            }
            else if (movPlayer.mirandoDerecha && !movPlayer.quieto && movPlayer.mirandoArriba && !movPlayer.mirandoAbajo)
            {

                Instantiate(nuez, controladorDisparo.position, controladorDisparo.rotation * Quaternion.Euler(-20f, 0f, 0f));

            }



        }

    }
    public void ActivarCollidersArma(bool cambiarMano)
    {
        if (cambiarMano)
        {
            puño1.enabled = true;
            puño2.enabled = false;
        }
        else
        {
            puño1.enabled = false;
            puño2.enabled = true;
        }

    }
    public void DesactivarCollidersArma()
    {
        puño1.enabled = false;
        puño2.enabled = false;
    }

    public void AgarrarNueces(int cantidad)
    {
        cantidadNueces += cantidad;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("LanzaNueces") && Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("LANZA NUECES DESBLOQUEADO, PULSA '1' PARA CAMBIAR MODO DE ATAQUE");
            lanzaNuecesEncontrado = true;
            Destroy(other.gameObject);
        }
    }



}
