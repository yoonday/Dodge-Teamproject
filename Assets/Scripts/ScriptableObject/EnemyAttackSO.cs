using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttackSO", menuName = "TopDownController/Attacks/Enemy", order = 0)]
public class EnemyAttackSO : PlayerAttackSO
{
    [Header("Attack Type Info (Amount Should be Bigger than 2)")]
    public AttackType angle;
    public float angleDeg;
    public int amount;
    public int attackCountAtOnce;
    public float spreadDelay;

    [Header("Enemy Stat")]
    public int enemyHealth;
    public bool isBoss;

    [Header("Enemy Sprite")]
    public Sprite enemySprite;
    public RuntimeAnimatorController enemyAnimatorController;

    public enum AttackType
    {
        STRAIGHT,
        ANGLED,
        SPREAD,
        STAR
    }
}
