using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShotState : EnemyShotState
{
    Tank tank;
    public TankShotState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyShotState stateData, Tank tank) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.tank = tank;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        stateMachine.ChangeState(tank.IdleState);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        Shot(tank.shotPoint);
        tank.reloadTime = stateData.reloadTime;
    }
}
