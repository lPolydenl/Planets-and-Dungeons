using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowState : StateMachineBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask playerLayer;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Collider2D rangeCheck = Physics2D.OverlapCircle(animator.transform.position, maxDistance, playerLayer);

        if (rangeCheck != null)
        {
            Transform target = rangeCheck.transform;
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, target.transform.position, speed * Time.deltaTime);
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
