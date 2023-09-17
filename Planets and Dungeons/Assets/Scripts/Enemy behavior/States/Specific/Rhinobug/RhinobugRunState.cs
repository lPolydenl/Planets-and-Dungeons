using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinobugRunState : EnemyMoveState
{
    Rhinobug rhinobug;
    public RhinobugRunState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyMoveState stateData, Rhinobug rhinobug) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.rhinobug = rhinobug;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time > startTime + stateData.stateTime)
        {
            stateMachine.ChangeState(rhinobug.SleepState);
        }
        if(rhinobug.CheckPlayerInRadius())
        {
            stateMachine.ChangeState(rhinobug.AttackState);
        }
    }
}
