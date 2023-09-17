using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Enemy
{
    public SpawnerIdleState IdleState { get; private set; }
    public SpawnerMoveState MoveState { get; private set; }
    public SpawnerSummonState SummonState { get; private set; }

    [SerializeField] private D_EnemyIdleState IdleStateData;
    [SerializeField] private D_EnemyMoveState MoveStateData;
    [SerializeField] private D_EnemySummonState SummonStateData;

    public Transform summonPoint;

    [HideInInspector] public float reloadTime;


    [SerializeField] private float angle;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float sightLenght;

    public override void Start()
    {
        base.Start();

        IdleState = new SpawnerIdleState(this, stateMachine, "idle", IdleStateData, this);
        MoveState = new SpawnerMoveState(this, stateMachine, "move", MoveStateData, this);
        SummonState = new SpawnerSummonState(this, stateMachine, "summon", SummonStateData, this);

        stateMachine.Initialize(MoveState);
    }

    public override void Update()
    {
        base.Update();

        if(reloadTime > 0)
        {
            reloadTime -= Time.deltaTime;
        }
    }

    public bool CheckPlayer()
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
}
