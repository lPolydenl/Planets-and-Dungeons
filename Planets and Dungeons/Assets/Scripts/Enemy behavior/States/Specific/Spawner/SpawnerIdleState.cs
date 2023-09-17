using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerIdleState : EnemyIdleState
{
    Spawner spawner;
    public SpawnerIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyIdleState stateData, Spawner spawner) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.spawner = spawner;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (spawner.reloadTime <= 0)
        {
            stateMachine.ChangeState(spawner.SummonState);
        }
        else if (!spawner.CheckPlayer())
        {
            stateMachine.ChangeState(spawner.MoveState);
        }
    }
}
