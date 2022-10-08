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
        Debug.Log("Daño por caparazon");
    }
}
