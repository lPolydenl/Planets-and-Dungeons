using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhinobug : Enemy
{
    public RhinobugSleepState SleepState { get; private set; }
    public RhinobugAngryState AngryState { get; private set; }
    public RhinobugAttackState AttackState { get; private set; }
    public RhinobugMoveState MoveState { get; private set; }
    public RhinobugRunState RunState { get; private set; }

    [SerializeField] private D_EnemyIdleState SleepStateData;
    [SerializeField] private D_EnemyIdleState AngryStateData;
    [SerializeField] private D_EnemyMoveState RunStateData;
    [SerializeField] private D_EnemyMoveState MoveStateData;
    [SerializeField] private D_EnemyMeleeAttackState AttackStateData;

    [SerializeField] private Transform[] sightPoints;
    [SerializeField] LayerMask whatIsSolid;
    [SerializeField] float sightLenght;

    [SerializeField] private float checkRadius = 0.35f;
    [SerializeField] private LayerMask playerLayer;

    public Transform attackPoint;
    public override void Start()
    {
        base.Start();

        SleepState = new RhinobugSleepState(this, stateMachine, "sleep", SleepStateData, this);
        AngryState = new RhinobugAngryState(this, stateMachine, "angry", AngryStateData, this);
        RunState = new RhinobugRunState(this, stateMachine, "run", RunStateData, this);
        MoveState = new RhinobugMoveState(this, stateMachine, "move", MoveStateData, this);
        AttackState = new RhinobugAttackState(this, stateMachine, "attack", AttackStateData, this);

        stateMachine.Initialize(MoveState);
    }

    public bool CheckPlayerHorizontal()
    {
        bool isSpotted = false;
        foreach (Transform sightPoint in sightPoints)
        {
            isSpotted = Physics2D.Raycast(sightPoint.position, Vector2.right * facingDirection, sightLenght, whatIsSolid).collider.gameObject.CompareTag("Player");
            if (isSpotted)
            {
                break;
            }
            else
            {
                continue;
            }
        }
        return isSpotted;
    }
    public bool CheckPlayerInRadius()
    {
        Collider2D collider = Physics2D.OverlapCircle(attackPoint.position, checkRadius, playerLayer);
        if (collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
