using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaEnemy : MonoBehaviour
{
    public float vida;
    public float maximoVida;

    // Start is called before the first frame update
    void Start()
    {
        vida = maximoVida;
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        if(vida == 0)
        {
            Destroy(gameObject);
        }
    }
}
