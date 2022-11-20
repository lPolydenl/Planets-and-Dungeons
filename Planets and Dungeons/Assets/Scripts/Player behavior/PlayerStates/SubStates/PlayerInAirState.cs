using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded;
    private bool isOnPlatform;
    private int xInput;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool coyoteTime;
    private bool wallJumpCoyoteTime;
    private bool isJumping;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    private bool isTouchingLedge;
    public bool platformsDisabled;
    private float timeBeforeEnablingPlatfotms;
    private bool sitInput;

    private float startWallJumpCoyoteTime;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingWallBack = player.CheckIfTouchingWallBack();
        isTouchingLedge = player.CheckIfTouchingLedge();

        if(player.CurrentVelocity.y > -0.1f && player.CurrentVelocity.y < 0.1f && !platformsDisabled)
        {
            isOnPlatform = player.CheckIfOnPlatform();
        }
        else
        {
            isOnPlatform = false;
        }

        if(isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }

        if(!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void Enter()
    {
        base.Enter();

        player.RB.sharedMaterial = playerData.noFriction;
        timeBeforeEnablingPlatfotms = 0f;
        player.head.SetActive(true);
        player.arms.SetActive(true);
        player.guns.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();
        EnablePlatforms();
        player.RollState.ReloadRoll();

        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        sitInput = player.InputHandler.SitInput;

        CheckJumpingMultiplier();

        if (sitInput && playerData.dashAvailable && xInput == player.FacingDirection)
        {
            stateMachine.ChangeState(player.DashState);
        }

        else if ((isGrounded || isOnPlatform) && player.CurrentVelocity.y < 0.1f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (isTouchingWall && !isTouchingLedge && xInput == player.FacingDirection)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
        else if((isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime) && jumpInput)
        {
            StopWallJumpCoyoteTime();
            isTouchingWall = player.CheckIfTouchingWall();
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if(jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if(isTouchingWall && xInput == player.FacingDirection && player.CurrentVelocity.y <= 0)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        else
        {
            player.Flip();
            player.SetVelocityX(playerData.movementVelocity * xInput);

            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
        }
    }

    private void CheckJumpingMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetVelocityY(player.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime()
    {
        if(coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    private void CheckWallJumpCoyoteTime()
    {
        if(wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }
    public void StartCoyoteTime() => coyoteTime = true;

    public void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }

    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;

    public void SetIsJumping() => isJumping = true;
    private void EnablePlatforms()
    {
        timeBeforeEnablingPlatfotms += Time.deltaTime;
        if(timeBeforeEnablingPlatfotms >= playerData.platformDisableTime)
        {
            platformsDisabled = false;
        }
    }
}
