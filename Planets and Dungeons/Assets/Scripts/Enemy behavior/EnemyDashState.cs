using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDashState : StateMachineBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] private float checkObstaclesRadius;
    private Collider2D rangeCheck;
    private Vector2 target;
    [SerializeField] private int minState;
    [SerializeField] private int maxState;
    [SerializeField] private int minStateBack;
    [SerializeField] private int maxStateBack;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rangeCheck = Physics2D.OverlapCircle(animator.transform.position, maxDistance, playerLayer);
        if (rangeCheck != null)
        {
            target = rangeCheck.transform.position - (animator.transform.position - rangeCheck.transform.position);
        }
        else
        {
            animator.SetInteger("Current State", Random.Range(minStateBack, maxStateBack + 1));
        }
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(rangeCheck != null)
        {
            if ((animator.transform.position.x < target.x + 1 && animator.transform.position.x > target.x - 1 && animator.transform.position.y < target.y + 1 && animator.transform.position.y > target.y - 1) || Physics2D.OverlapCircle(animator.transform.position, checkObstaclesRadius, obstacles))
            {
                animator.SetInteger("Current State", Random.Range(minState, maxState + 1));
            }
            else
            {
                animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);
            }
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
