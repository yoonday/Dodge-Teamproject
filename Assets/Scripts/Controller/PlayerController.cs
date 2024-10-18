using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{


    [SerializeField] private Vector3[] playerSpawnPoint;
    [SerializeField] private GameObject[] playerPrefab;


    private void OnSceneLoaed(Scene scene, LoadSceneMode mode)
    {

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {

            InitializeGame();

        }

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaed;

    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaed;

    }


    private void InitializeGame()
    {


        SetSpanwPoint();
        SpawnPlayer();

    }
    

    void SetSpanwPoint()
    {

        playerSpawnPoint = new Vector3[]{

            new Vector3(-5,-14,0),
            new Vector3(5,-14,0),
            new Vector3(0,-14,0)

        };

    }

    void SpawnPlayer()
    {

        for (int i = 0; i < GameManager.Instance.playerNum; i++)
        {

            if (GameManager.Instance.playerNum == 1)
                Instantiate(playerPrefab[i], playerSpawnPoint[2], Quaternion.identity);

            else
                Instantiate(playerPrefab[i], playerSpawnPoint[i], Quaternion.identity);
        }
   
    }




}
