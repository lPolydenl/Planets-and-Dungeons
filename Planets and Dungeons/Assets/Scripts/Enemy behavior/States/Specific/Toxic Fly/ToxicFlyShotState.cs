using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicFlyShotState : EnemyShotState
{
    ToxicFly toxicFly;
    public ToxicFlyShotState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyShotState stateData, ToxicFly toxicFly) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.toxicFly = toxicFly;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        stateMachine.ChangeState(toxicFly.IdleState);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        toxicFly.RotateGun();
        Shot(toxicFly.shotPoint);
    }
}
