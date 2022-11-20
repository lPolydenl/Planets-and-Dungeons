using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocityX(0f);
        playerData.dashCdLeft = 0f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.head.SetActive(false);
        player.arms.SetActive(true);
        player.guns.SetActive(true);

        player.SetVelocityX(playerData.dashVelocity * player.FacingDirection);
        player.SetVelocityY(0f);

        if(Time.time > startTime + playerData.dashTime)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
