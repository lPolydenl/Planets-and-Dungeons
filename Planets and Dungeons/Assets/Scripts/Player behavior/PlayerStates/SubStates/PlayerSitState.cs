using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSitState : PlayerState
{
    private bool sitInput;
    private int xInput;
    private bool isGrounded;
    private bool isOnPlatform;
    public PlayerSitState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        player.Anim.SetBool("stand", false);
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
        isOnPlatform = player.CheckIfOnPlatform();
    }

    public override void Enter()
    {
        base.Enter();
        isAnimationFinished = false;
        player.SetVelocityX(0f);
        player.Anim.SetBool("stand", false);
        player.head.SetActive(true);
        player.arms.SetActive(true);
        player.guns.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        isAnimationFinished = true;
        player.Anim.SetBool("stand", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.RollState.ReloadRoll();
        player.Flip();
        sitInput = player.InputHandler.SitInput;
        xInput = player.InputHandler.NormInputX;
        if (sitInput && playerData.dashAvailable && xInput == player.FacingDirection)
        {
            stateMachine.ChangeState(player.RollState);
        }
        else if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (sitInput == false)
        {
            player.Anim.SetBool("stand", true);
        }
    }
}
