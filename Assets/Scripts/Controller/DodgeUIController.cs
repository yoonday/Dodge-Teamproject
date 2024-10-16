using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class DodgeUIController : MonoBehaviour
{
    //1P, 2P 플레이어 이름 가져오기, 

    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private Image playerSkillimage;
    [SerializeField] private Image[] playerHealth;
    [SerializeField] private TextMeshProUGUI score;

    private HealthSystem healthSystem;


    private int currentScore = 0;

    private void Awake()
    {

        playerName = GameObject.Find($"{gameObject.name}UI").GetComponent<TextMeshProUGUI>();
        playerSkillimage = GameObject.Find($"{gameObject.name}SkillIcon").GetComponent<Image>();
        playerHealth = GameObject.Find($"{gameObject.name}Health").GetComponentsInChildren<Image>();
        score = GameObject.Find("GameScore").GetComponent<TextMeshProUGUI>();


        healthSystem = GetComponent<HealthSystem>();
    }


    private void Start()
    {

        healthSystem.OnDamage += PlayerLostHealth;
        healthSystem.OnHeal += PlayerGetHealth;

    }

    private void PlayerGetHealth()
    {
      

    }

    private void PlayerLostHealth()
    {

        //플레이어 health 이미지 비활성화 



    }



}
