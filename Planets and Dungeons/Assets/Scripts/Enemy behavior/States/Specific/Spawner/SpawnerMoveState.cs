using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMoveState : EnemyMoveState
{
    Spawner spawner;

    public SpawnerMoveState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyMoveState stateData, Spawner spawner) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.spawner = spawner;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (spawner.CheckPlayer())
        {
            stateMachine.ChangeState(spawner.IdleState);
        }
    }
}
