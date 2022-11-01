using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChracterController : MonoBehaviour
{

    Rigidbody rb; 
    public Transform plateTransform, hedef;
    public GameObject plate,platerb;
    float x = 0f;
    [HideInInspector] 
    public GameObject Clone;
    public int engelGuc = 10 , Score = 0, moveSpeed = 5, k=0;
    
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
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
        if(other.gameObject.tag== "preis")
        {
            Destroy(other.gameObject);
            Score++;
            x = x + .01f;
            Debug.Log(Score);
            //Instantiate(plate,plateTransform);
            
            

        }


       
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "alan")
        {
            StartCoroutine(wait());
            if (k == engelGuc) 
            {
                Destroy(other.gameObject);

            }
            



        }
    }
    public IEnumerator wait()
    {

        for (int i = 0; i < engelGuc; i++)
        {
            if (Score > 0)
            {
                Clone = Instantiate(platerb, plateTransform.position, Quaternion.identity);
                Clone.GetComponent<Rigidbody>().AddForce(transform.forward * 2000f);
                k++;
                Score--;
                Debug.Log(Score);
                yield return new WaitForSecondsRealtime(.5f);
                
                

            }




        }
        if (k < engelGuc)
        {
            Debug.Log("Game Over");
        }




    }



}
