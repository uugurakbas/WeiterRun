using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int timesc = 1;
    public GameObject restartMenu,GameOverMenu;
    public bool yasiyor;
    public void Start()
    {
        
        timesc = 1;
    }
    public void Update()
    {
        yasiyor = GameObject.FindWithTag("Player").GetComponent<ChracterController>().yasiyor;
        if (yasiyor == false)
        {
            GameOver();
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        restartMenu.SetActive(false);
        GameOverMenu.SetActive(false);
    }

    public void Stop()
    {
        timesc = 0;
        restartMenu.SetActive(true);
    }

    public void Continue()
    {
        timesc = 1;
        restartMenu.SetActive(false);
    }

    public void GameOver()
    {
        GameOverMenu.SetActive(true);
    }

}
