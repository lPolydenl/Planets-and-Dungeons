using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicFlyChaseState : EnemyChaseState
{
    ToxicFly toxicFly;
    public ToxicFlyChaseState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyChaseState stateData, ToxicFly toxicFly) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.toxicFly = toxicFly;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(toxicFly.CheckPlayer(toxicFly.smallSightLenght))
        {
            stateMachine.ChangeState(toxicFly.ShotState);
        }
        if(!toxicFly.CheckPlayer(toxicFly.bigSightLenght))
        {
            stateMachine.ChangeState(toxicFly.MoveState);
        }
    }
}
