using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttackSO", menuName = "TopDownController/Attacks/Enemy", order = 0)]
public class EnemyAttackSO : PlayerAttackSO
{
    [Header("Attack Type Info (Amount Should be Bigger than 2)")]
    public Angle angle;
    public float angleDeg;
    public int amount;
    public int attackCountAtOnce;

    [Header("Enemy Stat")]
    public int enemyHealth;

    public enum Angle
    {
        STRAIGHT,
        ANGLED
    }
}
