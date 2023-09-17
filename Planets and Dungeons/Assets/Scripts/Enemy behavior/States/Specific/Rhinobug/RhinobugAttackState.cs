using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinobugAttackState : EnemyMeleeAttackState
{
    Rhinobug rhinobug;
    public RhinobugAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyMeleeAttackState stateData, Rhinobug rhinobug) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.rhinobug = rhinobug;
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        Attack(rhinobug.attackPoint);
    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        stateMachine.ChangeState(rhinobug.SleepState);
    }
    public override void Enter()
    {
        base.Enter();

        rhinobug.SetVelocityX(0);
    }
}
