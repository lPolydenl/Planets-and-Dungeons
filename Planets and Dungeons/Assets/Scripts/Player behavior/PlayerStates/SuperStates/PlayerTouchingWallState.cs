using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected bool isTouchingWall;
    protected bool isGrounded;
    protected int xInput;
    protected bool jumpInput;
    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
    }

    public override void Enter()
    {
        base.Enter();
        player.head.SetActive(true);
        player.arms.SetActive(false);
        player.guns.SetActive(false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.RollState.ReloadRoll();
        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;

        if(jumpInput)
        {
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if(isGrounded)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if(!isTouchingWall || xInput != player.FacingDirection)
        {
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
