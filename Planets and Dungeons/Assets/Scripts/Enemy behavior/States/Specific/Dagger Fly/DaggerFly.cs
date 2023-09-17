using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerFly : Enemy
{
    public DaggerFlyIdleState IdleState { get; private set; }
    public DaggerFlyAttackState AttackState { get; private set; }
    public DaggerFlyChaseState ChaseState { get; private set; }
    public DaggerFlyDashState DashState { get; private set; }
    public DaggerFlyMoveState MoveState { get; private set; }

    [SerializeField] private D_EnemyIdleState IdleStateData;
    [SerializeField] private D_EnemyFlyState MoveStateData;
    [SerializeField] private D_EnemyChaseState ChaseStateData;
    [SerializeField] private D_EnemyDashState DashStateData;
    [SerializeField] private D_EnemyMeleeAttackState AttackStateData;

    [SerializeField] private float angle;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] private LayerMask playerLayer;
    public float bigSightLenght;
    public float smallSightLenght;

    public Transform[] wallChecks;

    public Transform attackPoint;

    public override void Start()
    {
        base.Start();

        IdleState = new DaggerFlyIdleState(this, stateMachine, "move", IdleStateData, this);
        MoveState = new DaggerFlyMoveState(this, stateMachine, "move", MoveStateData, this);
        AttackState = new DaggerFlyAttackState(this, stateMachine, "attack", AttackStateData, this);
        DashState = new DaggerFlyDashState(this, stateMachine, "move", DashStateData, this);
        ChaseState = new DaggerFlyChaseState(this, stateMachine, "move", ChaseStateData, this);

        stateMachine.Initialize(MoveState);
    }

    public bool CheckPlayer(float sightLenght)
    {
        bool canSeePlayer = false;
        Vector2 direction;

        Collider2D rangeCheck = Physics2D.OverlapCircle(transform.position, sightLenght, playerLayer);

        if (rangeCheck != null)
        {
            Transform target = rangeCheck.transform;
            direction = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.right, direction) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, direction, distanceToTarget, obstacles))
                {
                    canSeePlayer = true;
                    if (((transform.position.x < target.position.x) && facingDirection == -1) || ((transform.position.x > target.position.x) && facingDirection == 1))
                    {
                        Flip();
                    }
                }
                else canSeePlayer = false;
            }
            else canSeePlayer = false;
        }
        else
        {
            canSeePlayer = false;
        }
        return canSeePlayer;
    }
    public virtual bool CheckWallMulti()
    {
        foreach (Transform wallCheck in wallChecks)
        {
            if (Physics2D.Raycast(wallCheck.position, transform.right, enemyData.wallCheckDistance, enemyData.whatIsGround))
            {
                return true;
            }
        }
        return false;
    }
}
