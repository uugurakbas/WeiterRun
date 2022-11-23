using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject Player;
    Vector3 Dif;
    public int Speed;
    public bool bonus,bonusalanda;
    private void Start()
    {

        Dif = transform.position - Player.transform.position;
        
    }
    void Update()
    {
        Speed = Player.GetComponent<ChracterController>().moveSpeed;
        bonus = Player.GetComponent<ChracterController>().bonus;
        bonusalanda = Player.GetComponent<ChracterController>().bonusalanda;

        if(bonus == false || bonusalanda== false)
        {
            transform.position += Player.transform.forward * Speed * Time.deltaTime;  
        }
        if (bonus == true || bonusalanda == true) 
        {
            transform.position = Player.transform.position + Dif;
        }





    }

    
}
