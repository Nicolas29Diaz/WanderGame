using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispararNuezEnemy : MonoBehaviour
{
    public float velocidad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            other.GetComponent<VidaPlayer>().TomarDaño(20);
        }
        //if (other.CompareTag("RocaDestroy"))
        //{
        //    other.GetComponent<RocaDestroy>().BajarVidaRoca();
        //    Debug.Log("ROCA");
        //    Destroy(gameObject);
        //}

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "RocaDestroy")
    //    {
    //        Debug.Log("ROCA");
    //    }
    //}
}
