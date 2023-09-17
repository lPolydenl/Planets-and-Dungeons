using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerMoveState : EnemyMoveState
{
    Gunner gunner;
    public GunnerMoveState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyMoveState stateData, Gunner gunner) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.gunner = gunner;
    }

    public override void Enter()
    {
        base.Enter();

        gunner.gun.SetBool("idle", true);
    }

    public override void Exit()
    {
        base.Exit();

        gunner.gun.SetBool("idle", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(gunner.CheckPlayer())
        {
            stateMachine.ChangeState(gunner.IdleState);
        }
    }
}
