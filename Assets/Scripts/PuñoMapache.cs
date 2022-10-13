using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuñoMapache : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<VidaEnemy>().TomarDaño(20);
        }
    }
}
