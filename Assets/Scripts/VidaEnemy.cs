using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaEnemy : MonoBehaviour
{
    public float vida;
    public float maximoVida;

    public float da�oPu�o = 4;
    public float da�oNuez = 5;

    CombatePersonaje personaje;

    public int contPu�o = 0;
    public int contNuez = 0;



    // Start is called before the first frame update
    void Start()
    {
        personaje = GetComponent<CombatePersonaje>();
        vida = maximoVida;
    }

    public void TomarDa�o(float da�o)
    {
        vida -= da�o;
        if(vida <= 0)
        {
            Destroy(gameObject);
            personaje.AgarrarNueces(5);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pu�o")
        {
            contPu�o++;
            if (contPu�o > 1)
            {
                TomarDa�o(da�oPu�o);
                contPu�o = 0;
            }
        }
        if (other.gameObject.tag == "Nuez")
        {
            contNuez++;
            if(contNuez > 1)
            {
               TomarDa�o(da�oNuez);
                contNuez = 0;
            }
            
            
        }
    }


}
