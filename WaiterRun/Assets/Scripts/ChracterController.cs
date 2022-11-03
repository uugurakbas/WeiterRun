using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChracterController : MonoBehaviour
{
 
    Rigidbody rb;
    public Transform plateTransform;
    public GameObject plate,platerb;
    float x = 0f;
    [HideInInspector] public int Score = 0, k = 0, engelGuc;
    [HideInInspector] public GameObject Clone;
    public int moveSpeed = 8;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        engelGuc = GameObject.FindWithTag("gucluengel").GetComponent<EngelGuc>().guc;

        
    }

    private void Update()
    {
        transform.position += transform.forward *Time.deltaTime * moveSpeed;

        if (Input.GetKey("left"))
        {
            transform.position += -transform.right * Time.deltaTime * moveSpeed;
        }

        if (Input.GetKey("right"))
        {
            transform.position += transform.right * Time.deltaTime * moveSpeed;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "engel")
        {
            Debug.Log("game over");
        }
        
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag== "preis") //�d�l toplama
        {
            Destroy(other.gameObject);
            Score++;
            x = x + .01f;
            Debug.Log(Score);
            //Instantiate(plate,plateTransform);
            
            

        }

        if(other.gameObject.tag == "bonus")// z�playarak bonus alana ge�i�
        {
            Destroy(other.gameObject);
            rb.transform.position += transform.up * Time.deltaTime * 500f;
        }


       
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "alan") //g��l� engellere nesne f�rlatma
        {
            StartCoroutine(wait());
            if (k == engelGuc) 
            {     
                Destroy(other.gameObject);

            }
            
        }
        if (other.gameObject.tag == "alanBitis") // f�rlat�lan nesnelerin yok edilmesi
        {
            GameObject[] Clones = GameObject.FindGameObjectsWithTag("clone");
            foreach (GameObject item in Clones)
            {
                Destroy(item);
            }
        }

        if (other.gameObject.tag == "hizalan") // h�zlanma alan� 
        {
            moveSpeed = 20;
        }


    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag == "hizalan") // h�zlanma alan� biti�i
        {
            moveSpeed = 8; 
        }


    }
    public IEnumerator wait()
    {

        for (int i = 0; i < engelGuc; i++)
        {
            if (Score > 0)
            {
                Clone = Instantiate(platerb, plateTransform.position, Quaternion.identity) as GameObject;
                Clone.GetComponent<Rigidbody>().AddForce(transform.forward * 3000f);
                k++;
                Score--;
                Debug.Log(Score);
                yield return new WaitForSecondsRealtime(.7f);
                
                

            }




        }
        if (k < engelGuc)
        {
            Debug.Log("Game Over");

        }




    }



}
