using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerFlyMoveState : EnemyFlyState
{
    DaggerFly daggerFly;
    public DaggerFlyMoveState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyFlyState stateData, DaggerFly daggerFly) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.daggerFly = daggerFly;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (daggerFly.CheckWallMulti())
        {
            daggerFly.Flip();
        }
        if (daggerFly.CheckPlayer(daggerFly.bigSightLenght))
        {
            stateMachine.ChangeState(daggerFly.ChaseState);
        }
    }


}
