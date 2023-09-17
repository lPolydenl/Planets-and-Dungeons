using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyState : EnemyState
{
    protected D_EnemyFlyState stateData;
    public EnemyFlyState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyFlyState stateData) : base(enemy, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(stateData.moveSineX)
        {
            enemy.SetVelocityX(Mathf.Sin((Time.time + stateData.xOffset) * stateData.xFrequency) * stateData.xAmplitude);
        }
        else
        {
            enemy.SetVelocityX(stateData.xFrequency);
        }
        if (stateData.moveSineY)
        {
            enemy.SetVelocityY(Mathf.Sin((Time.time + stateData.yOffset) * stateData.yFrequency) * stateData.yAmplitude);
        }
        else
        {
            enemy.SetVelocityY(stateData.yFrequency);
        }
    }
    public override void Exit()
    {
        base.Exit();

        enemy.SetVelocityX(0f);
        enemy.SetVelocityY(0f);
    }
}
