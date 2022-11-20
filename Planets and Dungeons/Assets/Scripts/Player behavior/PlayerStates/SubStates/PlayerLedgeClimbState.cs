using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 detectedPos;
    private Vector2 cornerPos;
    private Vector2 startPos;
    private Vector2 stopPos;
    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        player.Anim.SetBool("climbLedge", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        isAnimationFinished = false;
        player.head.SetActive(false);
        player.arms.SetActive(false);
        player.guns.SetActive(false);

        player.SetVelocityZero();
        player.transform.position = detectedPos;
        cornerPos = player.DetermineCornerPosition();

        startPos.Set(cornerPos.x - (player.FacingDirection * playerData.startOffset.x), cornerPos.y - playerData.startOffset.y);
        stopPos.Set(cornerPos.x + (player.FacingDirection * playerData.stopOffset.x), cornerPos.y + playerData.stopOffset.y);

        player.transform.position = startPos;
    }

    public override void Exit()
    {
        base.Exit();

        player.transform.position = stopPos;
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.RollState.ReloadRoll();

        if (isAnimationFinished)
        {
            player.Anim.SetBool("idle", true);
            stateMachine.ChangeState(player.IdleState);
        }
        else
        {
            player.SetVelocityZero();
            player.transform.position = startPos;
        }
    }

    public void SetDetectedPosition(Vector2 pos) => detectedPos = pos;
}
