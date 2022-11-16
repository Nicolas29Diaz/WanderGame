using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoMiniBoss : MonoBehaviour
{
    public Animator animator;
    public MiniBoss miniBoss;
    public int melee;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            melee = Random.Range(0, 4);
            switch (melee)
            {
                case 0:
                    animator.SetFloat("Skills", 0);
                    miniBoss.hit_Select = 0;
                    break;
                case 1:
                    animator.SetFloat("Skills", 0.5f);
                    miniBoss.hit_Select = 1;
                    break;
                case 2:
                    animator.SetFloat("Skills", 1);
                    miniBoss.hit_Select = 2;
                    break;
            }

            animator.SetBool("Walk", false);
            animator.SetBool("Attack", true);
            miniBoss.atacando = true;
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
