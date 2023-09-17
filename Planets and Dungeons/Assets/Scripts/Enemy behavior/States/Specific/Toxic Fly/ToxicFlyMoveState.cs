using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicFlyMoveState : EnemyFlyState
{
    ToxicFly toxicFly;
    public ToxicFlyMoveState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyFlyState stateData, ToxicFly toxicFly) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.toxicFly = toxicFly;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(toxicFly.CheckPlayer(toxicFly.bigSightLenght))
        {
            stateMachine.ChangeState(toxicFly.ChaseState);
        }
    }
}
