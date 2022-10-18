using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotState : StateMachineBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private float instanceOffset;
    [SerializeField] private float offset;
    [SerializeField] private AudioSource shotSound;
    [SerializeField] private float delay;
    private float currentDelay;
    private Transform shotPoint;
    private Enemy enemy;
    private bool canShot;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        shotPoint = enemy.shotPoint;
        currentDelay = delay;
        canShot = true;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(currentDelay >= 0)
        {
            currentDelay -= Time.deltaTime;
        }
        else if (canShot)
        {
            Shot();
            canShot = false;
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    private void Shot()
    {
        if (shotSound != null)
        {
            var ss = Instantiate(shotSound);
            ss.Play();
        }
        Bullet newBullet = Instantiate(bullet, shotPoint.position + new Vector3(instanceOffset, 0f, 0f), shotPoint.rotation);
        newBullet.transform.Rotate(0f, 0f, Random.Range(-offset, offset));
        newBullet.team = "Enemy";
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
