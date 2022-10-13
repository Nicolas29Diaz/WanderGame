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
        if (other.CompareTag("player"))
        {
            Debug.Log("Daño por caparazon");
        }
    }
}
