using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIController : MonoBehaviour
{

    private PlayerUIController playerUIController;
    public GameObject hideItemIcon;
    public GameObject showItemIcon;
    public Text itemTimesText;
    public Image hideSkillImage;
    public float skillTimes;
    private bool isHideSkill;
    private int itemTimes;
    private float getSkillTimes;


    public void Start()
    {

        playerUIController = GetComponent<PlayerUIController>();
        
        

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

                hideItemIcon.SetActive(false);
            }

            itemTimesText.text = getSkillTimes.ToString("00");

            float time = getSkillTimes / skillTimes;
            hideSkillImage.fillAmount = time;
        }
    
    }


}
