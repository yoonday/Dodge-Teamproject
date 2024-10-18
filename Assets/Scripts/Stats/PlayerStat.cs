using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerStat 
{
    [Range(1, 100)] public int maxHealth;  
    [Range(1f, 20f)] public float speed; // 이동 속도
    public PlayerAttackSO playerSO;
}
