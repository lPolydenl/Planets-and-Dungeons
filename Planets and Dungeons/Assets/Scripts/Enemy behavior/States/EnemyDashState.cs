using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDashState : EnemyState
{
    protected D_EnemyDashState stateData;
    protected Vector2 target;
    protected Collider2D rangeCheck;


    public EnemyDashState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyDashState stateData) : base(enemy, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        rangeCheck = Physics2D.OverlapCircle(enemy.transform.position, stateData.maxDistance, stateData.playerLayer);
        if (rangeCheck != null)
        {
            target = rangeCheck.transform.position * 2 - enemy.transform.position;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, target, stateData.speed * Time.deltaTime);
        
    }
}
