using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : StateMachineBehaviour
{
    public float speed;
    [SerializeField] private float distanceX;
    [SerializeField] private float distanceY;
    private Transform groundDetection;
    private RaycastHit2D groundInfo;
    private RaycastHit2D wallInfo;
    public LayerMask whatIsGround;
    private Enemy enemy;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject.GetComponent<Enemy>();
        groundDetection = enemy.groundDetector;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.Translate(Vector2.right * speed * Time.deltaTime);
        groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distanceY, whatIsGround);
        if (enemy.movingRight == true)
        {
            wallInfo = Physics2D.Raycast(groundDetection.position, Vector2.right, distanceX, whatIsGround);
        }
        else
        {
            wallInfo = Physics2D.Raycast(groundDetection.position, Vector2.left, distanceX, whatIsGround);
        }
        if (groundInfo.collider == false || wallInfo == true)
        {
            if (enemy.movingRight == true)
            {
                enemy.movingRight = false;
            }
            else
            {
                enemy.movingRight = true;
            }
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
