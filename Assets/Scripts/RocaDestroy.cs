using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocaDestroy : MonoBehaviour
{
    public int vidaRoca = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BajarVidaRoca()
    {
        vidaRoca -= 1;
        if(vidaRoca <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Nuez")
        {
            Destroy(other.gameObject); 
            BajarVidaRoca();    
        }
        
    }

}
