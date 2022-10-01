using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArdillaMeleeEnemy : MonoBehaviour
{
    public float distanciaPerseguir;
    public float distanciaAtaque;
    public float velocidad;
    public bool infoSueloFrenado;
    public bool patrullaje;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        infoSueloFrenado = gameObject.GetComponent<MovPlataforma>().infoSuelo;
        patrullaje = gameObject.GetComponent<MovPlataforma>().patrullando;
        if (other.CompareTag("Player"))
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            if (infoSueloFrenado == false)
            {
                transform.position = transform.position;
            }
            else if (Vector3.Distance(transform.position, player.transform.position) < distanciaPerseguir)
            {
                patrullaje = false;
                transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
            }

            if (Vector3.Distance(transform.position, player.transform.position) < distanciaAtaque)
            {
                Debug.Log("estoy atacando");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.GetComponent<MovPlataforma>().patrullando = true;
    }

}
