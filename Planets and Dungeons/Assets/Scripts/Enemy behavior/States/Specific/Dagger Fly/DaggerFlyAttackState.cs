using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerFlyAttackState : EnemyMeleeAttackState
{
    DaggerFly daggerFly;
    public DaggerFlyAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyMeleeAttackState stateData, DaggerFly daggerFly) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.daggerFly = daggerFly;
    }
    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        Attack(daggerFly.attackPoint);
    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        stateMachine.ChangeState(daggerFly.IdleState);
    }
    public override void Enter()
    {
        base.Enter();

        daggerFly.SetVelocityX(0f);
        daggerFly.SetVelocityY(0f);
    }
}
