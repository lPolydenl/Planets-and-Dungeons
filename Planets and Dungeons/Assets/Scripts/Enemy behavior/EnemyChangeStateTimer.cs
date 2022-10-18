using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChangeStateTimer : StateMachineBehaviour
{
    [SerializeField] private float timeLeft;
    private float currentTimeLeft;
    [SerializeField] private int minState;
    [SerializeField] private int maxState;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentTimeLeft = timeLeft;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(currentTimeLeft <= 0)
        {
            animator.SetInteger("Current State", Random.Range(minState, maxState + 1));
        }
        else
        {
            currentTimeLeft -= Time.deltaTime;
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
