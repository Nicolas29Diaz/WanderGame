using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorAtaque : MonoBehaviour
{
    public int melee;
    public Animator animator;
    public GameObject Boss;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            melee = Random.Range(0, 3);
            switch (melee)
            {
                case 0:
                    Boss.transform.position = Boss.transform.position;
                    animator.SetFloat("Skills", 0);
                    break;
                case 1:
                    Boss.transform.position = Boss.transform.position;
                    animator.SetFloat("Skills", 0.5f);
                    break;
                case 2:
                    Boss.transform.position = Boss.transform.position;
                    animator.SetFloat("Skills", 1);
                    break;
            }
        }
    }
}
