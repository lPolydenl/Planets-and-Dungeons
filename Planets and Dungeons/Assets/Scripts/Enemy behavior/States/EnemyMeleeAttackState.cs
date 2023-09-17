using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : EnemyState
{
    protected D_EnemyMeleeAttackState stateData;
    public EnemyMeleeAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyMeleeAttackState stateData) : base(enemy, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }


    protected void Attack(Transform attackPoint)
    {
        Collider2D collider = Physics2D.OverlapCircle(attackPoint.position, stateData.attackRadius, stateData.playerLayer);
        if (stateData.attackSound != null)
        {
            var ss = GameObject.Instantiate(stateData.attackSound);
            ss.Play();
        }
        if (collider != null)
        {
            Health health = collider.GetComponent<Health>();
            health.TakeDamage(stateData.damage, stateData.makeInvincible, stateData.takeDamageAnyway);
            Vector2 force;
            Player player = collider.GetComponent<Player>();
            player.stunTime = stateData.stunTime;
            force = new Vector2(stateData.XForce * enemy.facingDirection, stateData.YForce);
            player.stunVelocity = force;
        }
    }
}
