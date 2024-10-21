using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DodgeUIController : MonoBehaviour
{


    protected HealthSystem healthSystem;

    // Text로 할건지, TextMeshPro로 할건지
    [SerializeField] protected  Text score;
    [SerializeField] protected Text bestScore;
    [SerializeField] protected GameObject resultScreen;


    protected virtual void Start()
    {
   
        // 이름 수정 필요
        resultScreen = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        score = GameObject.Find("Score").GetComponent<Text>();
        bestScore = resultScreen.transform.Find("BestScore").GetComponent<Text>();

    }

    protected virtual void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();

    }






}