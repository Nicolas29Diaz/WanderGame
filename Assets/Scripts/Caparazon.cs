using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caparazon : MonoBehaviour
{
    private void Update()
    {
        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<VidaPlayer>().TomarDa�o(20);
            Destroy(gameObject);
        }
    }
}
