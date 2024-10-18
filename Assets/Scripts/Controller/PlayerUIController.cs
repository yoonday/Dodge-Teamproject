using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerUIController : DodgeUIController
{

    private HealthSystem health;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private Image playerSkillimage;
    [SerializeField] private GameObject[] playerHealth;


    protected override void Awake()
    {
        base.Awake();

        health = GetComponent<HealthSystem>();

        playerName = GameObject.Find($"{gameObject.name}UI").GetComponent<TextMeshProUGUI>();
        playerSkillimage = GameObject.Find($"{gameObject.name}SkillIcon").GetComponent<Image>();
        playerHealth = GameObject.Find($"{gameObject.name}Health").GetComponentsInChildren<GameObject>();
        
    }


    protected override void Start()
    {
     
        healthSystem.OnDamage += PlayerLostHealth;
        healthSystem.OnHeal += PlayerGetHealth;
        healthSystem.OnDeath += result;

    }

    private void PlayerGetHealth()
    {
        //플레이어 health 이미지 활성화
        for (int i = 0; i < health.CurrentHealth; i++)
            playerHealth[i].SetActive(true);

    }

    private void PlayerLostHealth()
    {

        //플레이어 health 이미지 비활성화 
        for (int i = 0; i < health.CurrentHealth; i++)
            playerHealth[i].SetActive(true);

    }


    private void result()
    {

        resultScreen.SetActive(true);

        if (currentScore > InGameBestScore)
        {

            InGameBestScore = currentScore;
            PlayerPrefs.SetInt("이름 미정", InGameBestScore);

        }


    }

}
