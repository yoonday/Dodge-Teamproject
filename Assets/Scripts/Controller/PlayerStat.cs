using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatsChangeType
{
    Add, // 0
    Multiple, // 1
    Override // 2
}

[System.Serializable]
public class PlayerStat 
{
    public StatsChangeType statsChangeType; 
    [Range(1, 100)] public int maxHealth;  
    [Range(1f, 20f)] public float speed;
    public PlayerAttackSO playerSO;
}
