using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerFlyChaseState : EnemyChaseState
{
    DaggerFly daggerFly;
    public DaggerFlyChaseState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyChaseState stateData, DaggerFly daggerFly) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.daggerFly = daggerFly;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(!daggerFly.CheckPlayer(daggerFly.bigSightLenght))
        {
            stateMachine.ChangeState(daggerFly.MoveState);
        }
        if(daggerFly.CheckPlayer(daggerFly.smallSightLenght))
        {
            stateMachine.ChangeState(daggerFly.DashState);
        }
    }
}    

