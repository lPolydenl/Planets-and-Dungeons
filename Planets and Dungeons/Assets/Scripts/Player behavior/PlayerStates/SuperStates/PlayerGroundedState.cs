using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected bool isOnSlope;
    protected Vector2 slopeNormalPerp;
    protected float slopeSideAngle;
    private bool jumpInput;
    private bool isGrounded;
    private bool isOnPlatform;
    protected bool sitInput;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        SlopeCheck();
        isGrounded = player.CheckIfGrounded();
        isOnPlatform = player.CheckIfOnPlatform();
    }

    public override void Enter()
    {
        base.Enter();
        player.head.SetActive(true);
        player.arms.SetActive(true);
        player.guns.SetActive(true);
        player.JumpState.ResetAmountOfJumpsLeft();
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
        sitInput = player.InputHandler.SitInput;
        if(sitInput && xInput == 0)
        {
            stateMachine.ChangeState(player.SitState);
        }

        if(jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        if (!isGrounded && !isOnPlatform)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        if(isOnPlatform && Input.GetKeyDown(KeyCode.S))
        {
            player.InAirState.platformsDisabled = true;
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!isOnSlope)
        {
            player.SetVelocityX(playerData.movementVelocity * xInput);
        }
        else if(xInput != 0)
        {
            player.SetVelocityX(playerData.movementVelocity * slopeNormalPerp.x * -xInput);
            player.SetVelocityY(playerData.movementVelocity * slopeNormalPerp.y * -xInput);
        }
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = player.transform.position - new Vector3(0.0f, player.CC.size.y / 2);

        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, player.transform.right, playerData.slopeCheckDistance, playerData.whatIsGround + playerData.whatIsPlatform);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -player.transform.right, playerData.slopeCheckDistance, playerData.whatIsGround + playerData.whatIsPlatform);

        if (slopeHitFront && !slopeHitBack)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if (slopeHitBack && !slopeHitFront)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }

        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, playerData.slopeCheckDistance, playerData.whatIsGround + playerData.whatIsPlatform);

        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal);
        }

        if (isOnSlope && xInput == 0)
        {
            player.RB.sharedMaterial = playerData.fullFriction;
        }
        else
        {
            player.RB.sharedMaterial = playerData.noFriction;
        }
    }
}
