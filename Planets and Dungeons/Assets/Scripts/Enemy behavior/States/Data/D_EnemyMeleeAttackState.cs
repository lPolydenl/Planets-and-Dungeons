using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyMeleeAttackStateData", menuName = "Data/Enemy State Data/Melee Attack State")]
public class D_EnemyMeleeAttackState : ScriptableObject
{
    public LayerMask playerLayer;
    public float attackRadius = 0.35f;
    public float stunTime = 0.25f;
    public int damage = 5;
    public float XForce = 10f;
    public float YForce = 10f;
    public AudioSource attackSound;
    public bool makeInvincible = true;
    public bool takeDamageAnyway;
}
