using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{


    public int playerNum;
    public PoolManager ObjectPool { get; private set; }


    public void MultiplayCheck(int player)
    {
        playerNum = player;

    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaed;

    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaed;

    }

    private void OnSceneLoaed(Scene scene, LoadSceneMode mode)
    {

        BgmControl();

    }


    private void BgmControl()
    {

        AudioManager.Instance.PlayBgm(true);

    }

}
