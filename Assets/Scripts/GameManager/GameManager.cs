using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{


    public int playerNum;
    public int currentScore = 0;
    public int bestScore;
   

    public PoolManager ObjectPool { get; private set; }


    public override void Awake()
    {
        base.Awake();

        if (PlayerPrefs.HasKey("BestScore"))
            bestScore = PlayerPrefs.GetInt("BestScore");
        else
            bestScore = 0;
    }

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

        currentScore = 0;
        bestScore = 0;

        ObjectPool = FindObjectOfType<PoolManager>();

        Time.timeScale = 1;

    }


    private void BgmControl()
    {

        AudioManager.Instance.PlayBgm(true);

    }

}
