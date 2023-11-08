using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : Enemy
{
    public GunnerIdleState IdleState { get; private set; }
    public GunnerMoveState MoveState { get; private set; }
    public GunnerShotState ShotState { get; private set; }

    [SerializeField] private D_EnemyIdleState IdleStateData;
    [SerializeField] private D_EnemyMoveState MoveStateData;
    [SerializeField] private D_EnemyShotState ShotStateData;

    public Transform shotPoint;

    [HideInInspector] public float reloadTime;

    [SerializeField] private float angle;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float sightLenght;

    public Animator gun;
    public Transform gunPoint;
    [SerializeField] private float offset;

    public override void Start()
    {
        base.Start();

        IdleState = new GunnerIdleState(this, stateMachine, "idle", IdleStateData, this);
        MoveState = new GunnerMoveState(this, stateMachine, "move", MoveStateData, this);
        ShotState = new GunnerShotState(this, stateMachine, "idle", ShotStateData, this);

        stateMachine.Initialize(MoveState);
    }

    public override void Update()
    {
        base.Update();

        if (reloadTime > 0)
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

    public void RotateGun()
    {
        Collider2D rangeCheck = Physics2D.OverlapCircle(transform.position, sightLenght, playerLayer);
        if (rangeCheck != null)
        {

            Transform target = rangeCheck.transform;
            Vector2 targetPos = target.transform.position;
            Vector2 direction = targetPos - (Vector2)transform.position;
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            gunPoint.rotation = Quaternion.Euler(0f, 0f, rotZ + offset * facingDirection);
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
            gunPoint.localScale = gunScale;
        }
    }
}
