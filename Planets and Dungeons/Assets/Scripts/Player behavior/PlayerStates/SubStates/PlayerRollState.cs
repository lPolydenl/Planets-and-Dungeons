using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : PlayerState
{
    private float currentVelocity;
    public PlayerRollState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityX(playerData.rollVelocity);
        currentVelocity = playerData.rollVelocity;
        player.head.SetActive(false);
        player.arms.SetActive(true);
        player.guns.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        playerData.dashCdLeft = 0f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        currentVelocity -= Time.deltaTime * playerData.rollAcceleration;
        player.SetVelocityX(currentVelocity * player.FacingDirection);
        if(Time.time > startTime + playerData.rollTime)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public void ReloadRoll()
    {
        if(playerData.dashCdLeft < playerData.dashCooldown)
        {
            playerData.dashCdLeft += Time.deltaTime;
            playerData.dashAvailable = false;
        }
        else
        {
            playerData.dashAvailable = true;
        }
    }

}
