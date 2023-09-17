using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    protected D_EnemyChaseState stateData;
    protected Collider2D rangeCheck;
    public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyChaseState stateData) : base(enemy, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
    public override void Enter()
    {
        base.Enter();

        rangeCheck = Physics2D.OverlapCircle(enemy.transform.position, stateData.maxDistance, stateData.playerLayer);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (rangeCheck != null)
        {
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, rangeCheck.transform.position, stateData.speed * Time.deltaTime);
        }
    }
}
