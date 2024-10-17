using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttackSO", menuName = "TopDownController/Attacks/Enemy", order = 0)]
public class EnemyAttackSO : ScriptableObject
{
    [Header("Base Attack Info")]
    public float size;
    public float delay;
    public float power;
    public float speed;
    public LayerMask target;

    [Header("Attack Type Info")]
    public Angle angle;
    public float angleDeg;
    public int amount;

    public enum Angle
    {
        STRAIGHT,
        ANGLED
    }
}
