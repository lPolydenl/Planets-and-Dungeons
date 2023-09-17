using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSummonState : EnemySummonState
{
    Spawner spawner;

    public SpawnerSummonState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemySummonState stateData, Spawner spawner) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.spawner = spawner;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        stateMachine.ChangeState(spawner.IdleState);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        Summon(spawner.summonPoint);
        spawner.reloadTime = stateData.reloadTime;
    }
}
