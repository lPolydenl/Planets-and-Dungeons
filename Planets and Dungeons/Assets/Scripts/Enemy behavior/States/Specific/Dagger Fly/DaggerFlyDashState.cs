using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerFlyDashState : EnemyDashState
{
    DaggerFly daggerFly;
    public DaggerFlyDashState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyDashState stateData, DaggerFly daggerFly) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.daggerFly = daggerFly;
    }

    public override void Enter()
    {
        base.Enter();

        if(rangeCheck != null)
        {
            if(Physics2D.OverlapCircle(enemy.transform.position, stateData.checkObstaclesRadius, stateData.playerLayer))
            {
                stateMachine.ChangeState(daggerFly.AttackState);
            }
            else if (Physics2D.OverlapCircle(enemy.transform.position, stateData.checkObstaclesRadius, stateData.obstacles))
            {
                stateMachine.ChangeState(daggerFly.ChaseState);
            }
        }
        else
        {
            stateMachine.ChangeState(daggerFly.ChaseState);
        }
    }
    public override void LogicUpdate()
    {
        if (Physics2D.OverlapCircle(enemy.transform.position, stateData.checkObstaclesRadius, stateData.obstacles + stateData.playerLayer))
        {
            stateMachine.ChangeState(daggerFly.AttackState);
        }
        else
        {
            base.LogicUpdate();
        }
        if (Time.time > startTime + stateData.stateTime)
        {
            stateMachine.ChangeState(daggerFly.IdleState);
        }
    }
}
