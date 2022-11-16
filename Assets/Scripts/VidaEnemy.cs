using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaEnemy : MonoBehaviour
{
    public float vida;
    public float maximoVida;

    public float dañoPuño = 4;
    public float dañoNuez = 5;

    CombatePersonaje personaje;

    public int contPuño = 0;
    public int contNuez = 0;



    // Start is called before the first frame update
    void Start()
    {
        personaje = GetComponent<CombatePersonaje>();
        vida = maximoVida;
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        if(vida <= 0)
        {
            Destroy(gameObject);
            personaje.AgarrarNueces(5);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Puño")
        {
            contPuño++;
            if (contPuño > 1)
            {
                TomarDaño(dañoPuño);
                contPuño = 0;
            }
        }
        if (other.gameObject.tag == "Nuez")
        {
            contNuez++;
            if(contNuez > 1)
            {
               TomarDaño(dañoNuez);
                contNuez = 0;
                Destroy(other.gameObject); 
            }
            
            
        }
    }


}
