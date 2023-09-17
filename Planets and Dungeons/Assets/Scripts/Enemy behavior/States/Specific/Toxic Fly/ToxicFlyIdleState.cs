using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicFlyIdleState : EnemyIdleState
{
    ToxicFly toxicFly;

    public ToxicFlyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyIdleState stateData, ToxicFly toxicFly) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.toxicFly = toxicFly;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time > startTime + stateData.stateTime)
        {
            stateMachine.ChangeState(toxicFly.MoveState);
        }
    }
}
