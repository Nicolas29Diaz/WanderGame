using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCaparazon : MonoBehaviour
{
    public GameObject caparazon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(caparazon, transform.position, Quaternion.identity);
        }
    }

}
