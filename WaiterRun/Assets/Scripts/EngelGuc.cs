using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EngelGuc : MonoBehaviour
{
    public int guc = 0,carpismaSayisi = 0;
    Rigidbody rb;
    public bool esit = false;
    public TextMeshPro gucYazi;
    Animator animator;
     
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GameObject.FindWithTag("Player").GetComponent<Animator>(); 
    }

    
    void Update()
    {
        gucYazi.text = (guc - carpismaSayisi).ToString(); 
        if (carpismaSayisi == guc)
        {
            Destroy(this.gameObject);
            Destroy(GameObject.FindWithTag("alan"));
            esit = true;
            animator.SetBool("Firlat", false);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "clone")
        {
            carpismaSayisi++;
            Destroy(collision.gameObject);
        }

    }
}
