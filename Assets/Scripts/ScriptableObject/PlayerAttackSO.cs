using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttackSO", menuName = "TopDownController/Attacks/Player", order = 0)]
public class PlayerAttackSO : ScriptableObject
{
    [Header("Attack Info")]
    public float size; 
    public float delay; 
    public float power; 
    public float speed; 
    public LayerMask target; 
}
