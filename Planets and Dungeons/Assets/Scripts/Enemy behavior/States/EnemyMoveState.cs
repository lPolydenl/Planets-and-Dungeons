using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    protected D_EnemyMoveState stateData;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    public EnemyMoveState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyMoveState stateData) : base(enemy, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocityX(stateData.movementSpeed);

        isDetectingLedge = enemy.CheckLedge();
        isDetectingWall = enemy.CheckWall();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        isDetectingLedge = enemy.CheckLedge();
        isDetectingWall = enemy.CheckWall();

        if (!isDetectingLedge || isDetectingWall || enemy.rb.velocity == Vector2.zero)
        {
            enemy.Flip();
        }
        enemy.SetVelocityX(stateData.movementSpeed);
    }
}
