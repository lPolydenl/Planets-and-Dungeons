using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected bool isOnSlope = false;
    protected Vector2 slopeNormalPerp;
    protected float slopeSideAngle;
    private bool jumpInput;
    private bool isGrounded;
    protected bool isOnPlatform;
    protected bool sitInput;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
        isOnPlatform = player.CheckIfOnPlatform();
        SlopeCheck();
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
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = player.transform.position - new Vector3(0.0f, player.CC.size.y / 2 + 0.1f);

        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, player.transform.right, playerData.slopeCheckDistance, playerData.whatIsGround + playerData.whatIsPlatform);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -player.transform.right, playerData.slopeCheckDistance, playerData.whatIsGround + playerData.whatIsPlatform);

        if (slopeHitFront == true)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
            Debug.DrawLine(checkPos, slopeHitFront.point, Color.blue);
            Debug.Log("front slope");
        }
        else if (slopeHitBack == true)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
            Debug.DrawLine(checkPos, slopeHitBack.point, Color.blue);
            Debug.Log("back slope");
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }

        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, playerData.slopeCheckDistance, playerData.whatIsGround + playerData.whatIsPlatform);

        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if(slopeDownAngle != slopeDownAngleOld && !isOnPlatform)
            {
                //isOnSlope = true;
                Debug.DrawLine(checkPos, hit.point, Color.blue);
                Debug.Log("don slope");
            }
            slopeDownAngleOld = slopeDownAngle;
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
