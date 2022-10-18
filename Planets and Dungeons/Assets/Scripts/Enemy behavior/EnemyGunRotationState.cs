using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunRotationState : StateMachineBehaviour
{
    private Vector2 direction;
    [SerializeField] private float offset;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float maxDistance;
    private Enemy enemy;
    private Transform gun;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        gun = enemy.gun.transform;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Collider2D rangeCheck = Physics2D.OverlapCircle(animator.transform.position, maxDistance, playerLayer);
        if(rangeCheck != null)
        {
            Transform target = rangeCheck.transform;
            Vector2 targetPos = target.transform.position;
            direction = targetPos - (Vector2)animator.transform.position;
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (enemy.movingRight)
            {
                gun.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
            }
            else
            {
                gun.transform.rotation = Quaternion.Euler(0f, 0f, rotZ - offset);
            }
            Vector3 gunScale = Vector3.one;
            if (rotZ > 90 || rotZ < -90)
            {
                gunScale.y = -1f;
                enemy.movingRight = false;
            }
            else
            {
                gunScale.y = 1f;
                enemy.movingRight = true;
            }
            gun.transform.localScale = gunScale;
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
