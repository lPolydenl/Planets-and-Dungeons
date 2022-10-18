using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAdvancedSightState : StateMachineBehaviour
{
    [SerializeField] private float angle;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] private LayerMask playerLayer;
    private bool canSeePlayer;
    private Vector2 direction;
    [SerializeField] private float sightLenght;
    [SerializeField] private int minState;
    [SerializeField] private int maxState;
    [SerializeField] private bool inverted;
    [SerializeField] private Enemy enemy;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Collider2D rangeCheck = Physics2D.OverlapCircle(animator.transform.position, sightLenght, playerLayer);

        if (rangeCheck != null)
        {
            Transform target = rangeCheck.transform;
            direction = (target.position - animator.transform.position).normalized;

            if (Vector3.Angle(animator.transform.right, direction) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(animator.transform.position, target.position);

                if (!Physics2D.Raycast(animator.transform.position, direction, distanceToTarget, obstacles))
                {
                    canSeePlayer = true;
                    if (animator.transform.position.x < target.position.x)
                    {
                        enemy.movingRight = true;
                    }
                    else enemy.movingRight = false;
                }
                else canSeePlayer = false;
            }
            else canSeePlayer = false;
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
        if ((canSeePlayer && !inverted) || (!canSeePlayer && inverted))
        {
            animator.SetInteger("Current State", Random.Range(minState, maxState + 1));
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
