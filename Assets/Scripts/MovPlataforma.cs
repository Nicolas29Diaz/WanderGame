using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPlataforma : MonoBehaviour
{

    public float velocidad;
    public Transform controladorSuelo;
    public float distancia;
    private bool moviendoDerecha;
    private bool infoSuelo;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        infoSuelo = Physics.Raycast(controladorSuelo.position, Vector3.down, distancia);

        if (infoSuelo == false && moviendoDerecha)
        {
            Girar();
            transform.Translate(Vector3.forward * -velocidad * Time.deltaTime);
        } else if(infoSuelo == false && !moviendoDerecha)
        {
            Girar();
            transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
        } else
        {
            transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
        }
    }

    public void Girar()
    {
        moviendoDerecha = !moviendoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorSuelo.transform.position, controladorSuelo.transform.position + Vector3.down * distancia);
    }
}
