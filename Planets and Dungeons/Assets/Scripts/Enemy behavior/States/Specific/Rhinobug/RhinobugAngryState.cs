using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinobugAngryState : EnemyIdleState
{
    Rhinobug rhinobug;
    public RhinobugAngryState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyIdleState stateData, Rhinobug rhinobug) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.rhinobug = rhinobug;
    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        stateMachine.ChangeState(rhinobug.RunState);
    }
}
