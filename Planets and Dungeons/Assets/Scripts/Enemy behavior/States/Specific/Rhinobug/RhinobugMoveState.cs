using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinobugMoveState : EnemyMoveState
{
    Rhinobug rhinobug;
    public RhinobugMoveState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyMoveState stateData, Rhinobug rhinobug) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.rhinobug = rhinobug;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(rhinobug.CheckPlayerHorizontal())
        {
            stateMachine.ChangeState(rhinobug.AngryState);
        }
    }
}
