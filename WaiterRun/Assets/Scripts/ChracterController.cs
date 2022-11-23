using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ChracterController : MonoBehaviour
{
    public int moveSpeed = 10;
    Animator anim;
    Rigidbody rb;
    public Transform plateTransform, plateTransform2;
    public GameObject platerb,birikenTabak;
    [HideInInspector] public int Score = 0,  engelGuc, timesc;
    [HideInInspector] public GameObject Clone, birikenClone;
    public float bounds = 3;
    public bool yasiyor, esit, durma, bonus, bonusalanda;
    




    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
        esit = GameObject.FindWithTag("gucluengel").GetComponent<EngelGuc>().esit;



        bonusalanda = false;
        yasiyor = true;
        durma = false;
        bonus = false;
        
        
    }

    private void Update()
    {

        timesc = GameObject.Find("Canvas").GetComponent<GameManager>().timesc;
        Time.timeScale = timesc;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -bounds, bounds), transform.position.y, transform.position.z);
        transform.position += transform.forward *Time.deltaTime * moveSpeed;


        if (Input.GetKey("left"))
        {
            transform.position += -transform.right * Time.deltaTime * moveSpeed;
            anim.SetBool("SolKos", true);
        }
        else
        {
            anim.SetBool("SolKos", false);
        }

        if (Input.GetKey("right"))
        {
            transform.position += transform.right * Time.deltaTime * moveSpeed;
            anim.SetBool("SagKos",true);
        }
        else
        {
            anim.SetBool("SagKos", false);
        }

         if(timesc == 0)
        {
            durma = true;
        }
        else { durma = false; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "engel" || collision.gameObject.tag== "gucluengel")
        {
            anim.SetBool("Dusme", true);
            moveSpeed = 0;
            yasiyor=false;
            //collider.enabled = false;
 
        }

        
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag== "preis") //ödül toplama
        {
            Destroy(other.gameObject);
            Score++;
            Debug.Log(Score);


            birikenClone = Instantiate(birikenTabak,plateTransform2) as GameObject;
            birikenClone.transform.position += transform.forward * Time.deltaTime * moveSpeed;


        }

        if(other.gameObject.tag == "bonus")// zýplayarak bonus alana geçiþ
        {
            Destroy(other.gameObject);
            rb.AddForce(new Vector3(0,1200,0),ForceMode.Impulse);
            StartCoroutine(Bonus());
            
        }
        else
        {
            bonus = false;
        }

        if (other.gameObject.tag == "alan") //güçlü engellere nesne fýrlatma
        {
            engelGuc = other.gameObject.GetComponentInParent<EngelGuc>().guc;
            StartCoroutine(wait());
            

        }
        if(other.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }


    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "bonusalan")
        {
            bonusalanda = true;
            moveSpeed = 20;

        }
        else
        {
            bonusalanda = false;
            moveSpeed = 10;
        }

        if (other.gameObject.tag == "hizalan" && yasiyor==true) // hýzlanma alaný 
        {
            moveSpeed = 20;
        }

        if (other.gameObject.tag == "alan" && Score!=0) //güçlü engellere nesne fýrlatma
        {
            anim.SetBool("Firlat", true);
            



        }
        else if (other.gameObject.tag != "alan" || Score == 0)
        {
            anim.SetBool("Firlat", false);
        }


    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag == "hizalan") // hýzlanma alaný bitiþi
        {
            moveSpeed = 10; 
        }


    }
    public IEnumerator wait()
    {

        for (int i = 0; i < engelGuc; i++)
        {
            if (Score>0 )
            {
                

                
                yield return new WaitForSeconds(.324f);
                Clone = Instantiate(platerb, plateTransform.position,Quaternion.Euler(0,90,0)) as GameObject;
                Clone.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 80), ForceMode.Impulse);
                
                Score--;
                Debug.Log(Score);
                Destroy(GameObject.Find("birikenTabak(Clone)"));
                yield return new WaitForSeconds(.263f);







            }

            


        }





    }
    public IEnumerator Bonus()
    {
        bonus = true;
        yield return new WaitForSeconds(2);
        bonus = false;
    }



}
