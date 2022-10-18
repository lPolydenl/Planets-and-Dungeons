using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontalSightState : StateMachineBehaviour
{
    private Transform[] sightPoints;
    public float sightLenght;
    public LayerMask whatIsSolid;
    private Enemy enemy;
    [HideInInspector] public bool isSpotted;
    [SerializeField] private int minState;
    [SerializeField] private int maxState;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        sightPoints = enemy.sightPoints;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (Transform sightPoint in sightPoints)
        {
            if (enemy.movingRight)
            {
                isSpotted = Physics2D.Raycast(sightPoint.position, Vector2.right, sightLenght, whatIsSolid).collider.gameObject.CompareTag("Player");
            }
            else
            {
                isSpotted = Physics2D.Raycast(sightPoint.position, Vector2.left, sightLenght, whatIsSolid).collider.gameObject.CompareTag("Player");
            }
            if (isSpotted)
            {
                animator.SetInteger("Current State", Random.Range(minState, maxState + 1));
                break;
            }
            else
            {
                continue;
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
