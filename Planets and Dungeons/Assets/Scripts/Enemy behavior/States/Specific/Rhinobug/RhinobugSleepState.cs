using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinobugSleepState : EnemyIdleState
{
    Rhinobug rhinobug;
    public RhinobugSleepState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyIdleState stateData, Rhinobug rhinobug) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.rhinobug = rhinobug;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time > startTime + stateData.stateTime)
        {
            stateMachine.ChangeState(rhinobug.MoveState);
        }
    }
}
