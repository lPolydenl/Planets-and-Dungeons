using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySummonState : EnemyState
{
    protected D_EnemySummonState stateData;
    public EnemySummonState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemySummonState stateData) : base(enemy, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public void Summon(Transform summonPoint)
    {
        Enemy newMinion = GameObject.Instantiate(stateData.minionTypes[Random.Range(0, stateData.minionTypes.Length)], summonPoint.position, enemy.transform.rotation);
        newMinion.facingDirection = enemy.facingDirection;
    }
}
