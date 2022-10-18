using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : StateMachineBehaviour
{
    private Transform meleeAttackPoint;
    private Enemy enemy;
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask player;
    [SerializeField] private int damage;
    [SerializeField] private int minState;
    [SerializeField] private int maxState;
    [SerializeField] private float XForce;
    [SerializeField] private float YForce;
    [SerializeField] private float stunTime = 0.2f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        meleeAttackPoint = enemy.meleeAttackPoint;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Collider2D collider = Physics2D.OverlapCircle(meleeAttackPoint.position, attackRadius, player);
        if(collider != null)
        {
            Health health = collider.GetComponent<Health>();
            health.TakeDamage(damage);
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            Vector2 force;
            Player player = collider.GetComponent<Player>();
            Stunned stunned = collider.gameObject.AddComponent<Stunned>();
            stunned.stunnedTime = stunTime;
            if (attackSound != null)
            {
                var ss = Instantiate(attackSound);
                ss.Play();
            }

            if (enemy.movingRight)
            {
                force = new Vector2(XForce, YForce);
            }
            else
            {
                force = new Vector2(-XForce, YForce);
            }
            rb.AddForce(force, ForceMode2D.Impulse);
            animator.SetInteger("Current State", Random.Range(minState, maxState));
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
