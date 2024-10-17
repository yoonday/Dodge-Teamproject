using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DodgeUIController : MonoBehaviour
{


    protected HealthSystem healthSystem;

    protected int currentScore = 0;
    protected int InGameBestScore = 0;

    [SerializeField] protected  TextMeshProUGUI score;
    [SerializeField] protected TextMeshProUGUI bestScore;
    [SerializeField] protected GameObject resultScreen;


    protected virtual void Start()
    {

        // 이름 수정 필요
        resultScreen = GameObject.Find("ResultUI").GetComponent<GameObject>();
        score = GameObject.Find("GameScore").GetComponent<TextMeshProUGUI>();
        bestScore = GameObject.Find("GameBestScore").GetComponent<TextMeshProUGUI>();

    }

    protected virtual void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();


        if (PlayerPrefs.HasKey("이름 미정"))
            InGameBestScore = PlayerPrefs.GetInt("이름 미정");

    }






}