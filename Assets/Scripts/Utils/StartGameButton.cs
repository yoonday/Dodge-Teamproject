using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGameButton : MonoBehaviour
{



    public void StartGame()
    {
        SceneManager.LoadScene("Dodge_Project_Main");
    }

    public void ReturnStartMain()
    {
        SceneManager.LoadScene("Dodge_Project_Start");
    }

    public void PlayerCount(int player)
    {

        GameObject.Find("GameManager").GetComponent<GameManager>().playerNum = player;

    }




}
