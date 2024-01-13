using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.Flip();

        if(xInput == 0 && !isExitingState)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        if(sitInput && playerData.dashAvailable && xInput == player.FacingDirection)
        {
            stateMachine.ChangeState(player.RollState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (isOnSlope && !isOnPlatform)
        {
            player.SetVelocityY(playerData.movementVelocity * slopeNormalPerp.y * -xInput);
            player.SetVelocityX(playerData.movementVelocity * slopeNormalPerp.x * -xInput);
        }
        else
        {
            
            player.SetVelocityX(playerData.movementVelocity * xInput);
        }
    }
}