using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArdillaMeleeEnemy : MonoBehaviour
{
    public float distanciaPerseguir;
    public float distanciaAtaque;
    public float velocidad;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Comportamiento();
        }
    }

    public void Comportamiento()
    {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        if (Vector3.Distance(transform.position, player.transform.position) < distanciaPerseguir)
        {
            gameObject.GetComponent<MovPlataforma>().patrullando = false;
            transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
        } 
        else if (Vector3.Distance(transform.position, player.transform.position) < distanciaAtaque)
        {
            Debug.Log("estoy atacando");
        }
    }

}
