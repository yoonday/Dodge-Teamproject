using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIController : MonoBehaviour
{

    private DodgeController controller;
    public PlayerShieldSystem playerShieldSystem;

    public Image playerSkillimage;
    public Text itemTimesText;
    public Image hideSkillImage;
    public float skillTimes;
    public bool isHideSkill = false;
    private int itemTimes;
    private float getSkillTimes;


    protected virtual void Awake()
    {
        controller = GetComponent<DodgeController>();
        playerShieldSystem = GetComponent<PlayerShieldSystem>();

        playerSkillimage = GameObject.Find($"{gameObject.name}SkillIcon").transform.GetChild(0).GetComponent<Image>();
        hideSkillImage = GameObject.Find($"{gameObject.name}SkillIcon").transform.GetChild(1).GetComponent<Image>();
        itemTimesText = GameObject.Find($"{gameObject.name}SkillIcon").transform.GetChild(2).GetComponent<Text>();

    }

    private void Update()
    {
        HideSkillChk();

    }


    public void HideSkillSetting()
    {
        hideSkillImage.gameObject.SetActive(true);
        itemTimesText.gameObject.SetActive(true);
        getSkillTimes = skillTimes;  
       
        isHideSkill = true;

    }

    private void HideSkillChk()
    {

        if (isHideSkill)
        {
            StartCoroutine(SkillTimeChk());
        
        }
    
    }


    IEnumerator SkillTimeChk()
    { 
    
        yield return null;

        if(getSkillTimes > 0)
        { 
            getSkillTimes -=Time.deltaTime;


            if (getSkillTimes < 0)
            {
                playerSkillimage.sprite = null;
                hideSkillImage.gameObject.SetActive(false);
                itemTimesText.gameObject.SetActive(false);
                isHideSkill = false;

            }

            itemTimesText.text = getSkillTimes.ToString("0");

            float time = getSkillTimes / skillTimes;
            hideSkillImage.fillAmount = time;
        }
    
    }


    void Start()
    {
        controller.OnItemEvent += OnUse;
    }


    private void OnUse()
    {

        if (playerShieldSystem.canActivateShield)
        {
            playerShieldSystem.ActivateShield();
            HideSkillSetting();

        }
    }

}
