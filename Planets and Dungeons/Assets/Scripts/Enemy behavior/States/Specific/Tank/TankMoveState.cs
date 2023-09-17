using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMoveState : EnemyMoveState
{
    Tank tank;
    public TankMoveState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyMoveState stateData, Tank tank) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.tank = tank;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (tank.CheckPlayer())
        {
            stateMachine.ChangeState(tank.IdleState);
        }
    }
}
