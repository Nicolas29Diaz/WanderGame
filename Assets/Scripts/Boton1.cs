using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boton1 : MonoBehaviour
{
    public void Jugar() {
        SceneManager.LoadScene("NivelTest");
    }
    public void Cambiar()
    {
        SceneManager.LoadScene("Instrucciones");
    }
    public void Salir()
    {
        SceneManager.LoadScene("Interfaz1");
    }
}
