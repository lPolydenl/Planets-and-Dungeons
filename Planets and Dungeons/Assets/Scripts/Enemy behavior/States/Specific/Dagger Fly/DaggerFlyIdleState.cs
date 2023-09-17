using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerFlyIdleState : EnemyIdleState
{
    DaggerFly daggerFly;
    public DaggerFlyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyIdleState stateData, DaggerFly daggerFly) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.daggerFly = daggerFly;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time > startTime + stateData.stateTime)
        {
            stateMachine.ChangeState(daggerFly.MoveState);
        }
    }
}
