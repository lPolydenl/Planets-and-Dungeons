using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicFly : Enemy
{
    public ToxicFlyIdleState IdleState { get; private set; }
    public ToxicFlyShotState ShotState { get; private set; }
    public ToxicFlyChaseState ChaseState { get; private set; }
    public ToxicFlyMoveState MoveState { get; private set; }

    [SerializeField] private D_EnemyIdleState IdleStateData;
    [SerializeField] private D_EnemyFlyState MoveStateData;
    [SerializeField] private D_EnemyChaseState ChaseStateData;
    [SerializeField] private D_EnemyShotState ShotStateData;

    [SerializeField] private float angle;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] private LayerMask playerLayer;
    public float bigSightLenght;
    public float smallSightLenght;

    [SerializeField] private float offset;

    public Transform shotPoint;

    public override void Start()
    {
        base.Start();

        IdleState = new ToxicFlyIdleState(this, stateMachine, "move", IdleStateData, this);
        MoveState = new ToxicFlyMoveState(this, stateMachine, "move", MoveStateData, this);
        ShotState = new ToxicFlyShotState(this, stateMachine, "shot", ShotStateData, this);
        ChaseState = new ToxicFlyChaseState(this, stateMachine, "move", ChaseStateData, this);

        stateMachine.Initialize(MoveState);
    }
    public override void Update()
    {
        base.Update();
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
    public void RotateGun()
    {
        Collider2D rangeCheck = Physics2D.OverlapCircle(transform.position, bigSightLenght, playerLayer);
        if (rangeCheck != null)
        {

            Transform target = rangeCheck.transform;
            Vector2 targetPos = target.transform.position;
            Vector2 direction = targetPos - (Vector2)transform.position;
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            shotPoint.rotation = Quaternion.Euler(0f, 0f, rotZ + offset * facingDirection);
            Vector3 gunScale = Vector3.one;
            if (rotZ > 90 || rotZ < -90)
            {
                gunScale.y = -1f;
            }
            else
            {
                gunScale.y = 1f;
            }
            if (facingDirection != gunScale.y)
            {
                Flip();
            }
            shotPoint.localScale = gunScale;
        }
    }
}
