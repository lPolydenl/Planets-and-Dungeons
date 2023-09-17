using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerShotState : EnemyShotState
{
    Gunner gunner;
    public GunnerShotState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyShotState stateData, Gunner gunner) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.gunner = gunner;
    }

    public override void Enter()
    {
        base.Enter();

        gunner.gun.SetBool("shot", true);
    }

    public override void Exit()
    {
        base.Exit();

        gunner.gun.SetBool("shot", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!gunner.CheckPlayer())
        {
            stateMachine.ChangeState(gunner.MoveState);
        }
        else
        {
            gunner.RotateGun();
        }
        if(gunner.reloadTime <= 0)
        {
            Shot(gunner.shotPoint);
            gunner.reloadTime = stateData.reloadTime;
        }
    }
}
