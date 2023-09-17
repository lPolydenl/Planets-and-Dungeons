using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankIdleState : EnemyIdleState
{
    Tank tank;
    public TankIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyIdleState stateData, Tank tank) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.tank = tank;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if(tank.reloadTime <= 0)
        {
            stateMachine.ChangeState(tank.ShotState);
        }
        else if(!tank.CheckPlayer())
        {
            stateMachine.ChangeState(tank.MoveState);
        }
    }
}
