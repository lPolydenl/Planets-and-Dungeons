using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerIdleState : EnemyIdleState
{
    Gunner gunner;
    public GunnerIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyIdleState stateData, Gunner gunner) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.gunner = gunner;
    }

    public override void Enter()
    {
        base.Enter();

        gunner.gun.SetBool("promote", true);
    }

    public override void Exit()
    {
        base.Exit();

        gunner.gun.SetBool("promote", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time > startTime + stateData.stateTime)
        {
            stateMachine.ChangeState(gunner.ShotState);
        }
        else if(!gunner.CheckPlayer())
        {
            stateMachine.ChangeState(gunner.MoveState);
        }
        else
        {
            gunner.RotateGun();
        }
    }
}
