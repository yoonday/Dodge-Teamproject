using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerUIController : DodgeUIController
{

    private HealthSystem health;
    public int MaxHealth;

    [SerializeField] private Image playerSkillimage;
    [SerializeField] private Transform playerHealth;
    [SerializeField] private GameObject[] playerHealthIcon;


    protected override void Awake()
    {
        base.Awake();

        rename();

        GameObject.Find("UI").transform.Find($"{gameObject.name}UI").gameObject.SetActive(true);
        playerSkillimage = GameObject.Find($"{gameObject.name}SkillIcon").GetComponent<Image>();
        playerHealth = GameObject.Find($"{gameObject.name}Health").GetComponent<Transform>();
        playerHealthIcon = new GameObject[playerHealth.childCount];

        HealthUIAssignment();
    }


    private void rename()
    {
        int index = gameObject.name.IndexOf("(Clone)");
        if (index > 0)
            gameObject.name = gameObject.name.Substring(0, index);

    }

    protected override void Start()
    {
        base.Start();

        health = GetComponent<HealthSystem>();
        MaxHealth = (int)health.CurrentHealth;

        healthSystem.OnDamage += PlayerLostHealth;
        healthSystem.OnHeal += PlayerGetHealth;
        healthSystem.OnDeath += PlayerLostHealth;
        healthSystem.OnDeath += result;

    }


    private void HealthUIAssignment()
    {

        for (int i = 0; i < playerHealth.transform.childCount; i++)
        {
            playerHealthIcon[i] = playerHealth.transform.GetChild(i).gameObject;
        }

    }

    private void PlayerGetHealth()
    {

        //플레이어 health 이미지 활성화
       if(MaxHealth > health.CurrentHealth)
            playerHealthIcon[(int)health.CurrentHealth].SetActive(true);

    }

    private void PlayerLostHealth()
    {

        //플레이어 health 이미지 비활성화 
        if (health.CurrentHealth >= 0)
            playerHealthIcon[(int)health.CurrentHealth].SetActive(false);
       
    }


    private void result()
    {

        GameManager.Instance.playerNum -= 1;

        if (GameManager.Instance.playerNum != 0)
        {
            gameObject.SetActive(false);
            return;
        }
  
        resultScreen.gameObject.SetActive(true);

        if (GameManager.Instance.currentScore > GameManager.Instance.bestScore)
        {

            GameManager.Instance.bestScore = GameManager.Instance.currentScore;
            PlayerPrefs.SetInt("BestScore", GameManager.Instance.bestScore);

        }

        bestScore.text = $"Best Score : {GameManager.Instance.bestScore.ToString()}";
        gameObject.SetActive(false);

        Time.timeScale = 0;

    }

}
