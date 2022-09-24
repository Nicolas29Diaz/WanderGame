using UnityEngine;

public class MovPlataforma : MonoBehaviour
{
    public bool patrullando;
    public float velocidad;
    public Transform controladorSuelo;
    public float distancia;

    private bool moviendoDerecha;
    public bool infoSuelo;
    // Start is called before the first frame update
    void Start()
    {
        patrullando = true;
    }

    private void FixedUpdate()
    {
        if (patrullando)
        {
            Patrullar();
        }
    }

    public void Girar()
    {
        moviendoDerecha = !moviendoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    public void Patrullar()
    {
        infoSuelo = Physics.Raycast(controladorSuelo.position, Vector3.down, distancia);

        if (infoSuelo == false && moviendoDerecha)
        {
            Girar();
            transform.Translate(Vector3.forward * -velocidad * Time.deltaTime);
        }
        else if (infoSuelo == false && !moviendoDerecha)
        {
            Girar();
            transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorSuelo.transform.position, controladorSuelo.transform.position + Vector3.down * distancia);
    }
}
