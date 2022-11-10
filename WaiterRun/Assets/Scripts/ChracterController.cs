using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChracterController : MonoBehaviour
{

    Animator anim,objAnim;
    Rigidbody rb;
    public Transform plateTransform;
    public GameObject platerb;
    [HideInInspector] public int Score = 0, k = 0, engelGuc;
    [HideInInspector] public GameObject Clone;
    public int moveSpeed = 10;
    public bool yasiyor;



    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        engelGuc = GameObject.FindWithTag("gucluengel").GetComponent<EngelGuc>().guc;
        yasiyor = true;
        
    }

    private void Update()
    {
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


    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "engel" || collision.gameObject.tag== "gucluengel")
        {
            anim.SetBool("Dusme", true);
            moveSpeed = 0;
            yasiyor=false;
            
        }

        
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag== "preis") //ödül toplama
        {
            Destroy(other.gameObject);
            Score++;
            Debug.Log(Score);        }

        if(other.gameObject.tag == "bonus")// zýplayarak bonus alana geçiþ
        {
            Destroy(other.gameObject);
            rb.AddForce(new Vector3(0,80,0),ForceMode.Impulse);
            
        }

        if (other.gameObject.tag == "alan") //güçlü engellere nesne fýrlatma
        {

            StartCoroutine(wait());
            if (k == engelGuc)
            {
                Destroy(other.gameObject);
                anim.SetBool("Firlat", false);

            }

        }
        if(other.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }


    }
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "alanBitis") // fýrlatýlan nesnelerin yok edilmesi
        {
            GameObject[] Clones = GameObject.FindGameObjectsWithTag("clone");
            foreach (GameObject item in Clones)
            {
                Destroy(item);
            }
        }

        if (other.gameObject.tag == "hizalan" && yasiyor==true) // hýzlanma alaný 
        {
            moveSpeed = 20;
        }
        if (other.gameObject.tag == "alan") //güçlü engellere nesne fýrlatma
        {
            if (k == engelGuc || Score ==0 )
            {
                Destroy(other.gameObject);
                anim.SetBool("Firlat", false);

            }

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
            if (Score>0 && k!=engelGuc)
            {
                
                yield return new WaitForSeconds(.1f);
                anim.SetBool("Firlat", true);
                Clone = Instantiate(platerb, plateTransform.position, Quaternion.identity) as GameObject;
                //Clone.GetComponent<Rigidbody>().AddForce(transform.forward * 3000f);
                Clone.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 100), ForceMode.Impulse);
                k++;
                Score--;
                Debug.Log(Score);






            }

            


        }





    }



}
