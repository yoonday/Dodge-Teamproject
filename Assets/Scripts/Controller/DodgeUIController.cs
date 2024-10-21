using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DodgeUIController : MonoBehaviour
{


    protected HealthSystem healthSystem;

    // Text�� �Ұ���, TextMeshPro�� �Ұ���
    [SerializeField] protected  Text score;
    [SerializeField] protected Text bestScore;
    [SerializeField] protected GameObject resultScreen;


    protected virtual void Start()
    {
   
        // �̸� ���� �ʿ�
        resultScreen = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        score = GameObject.Find("Score").GetComponent<Text>();
        bestScore = resultScreen.transform.Find("BestScore").GetComponent<Text>();

    }

    protected virtual void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();

    }






}