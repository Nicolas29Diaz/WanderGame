using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pies : MonoBehaviour
{
    public MovementPlayer movimiento;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {

        movimiento.isGrounded = true;


    }
    private void OnTriggerExit(Collider other)
    {

        movimiento.isGrounded = false;


    }
}
