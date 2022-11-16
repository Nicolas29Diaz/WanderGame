using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMiniBoss : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<VidaPlayer>().TomarDaño(damage);
        }
    }
}
