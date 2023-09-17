using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunnedState : PlayerState
{
    private bool isGrounded;
    private bool isOnPlatform;
    public PlayerStunnedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.RB.sharedMaterial = playerData.noFriction;
        player.arms.SetActive(false);
        player.guns.SetActive(false);
        player.head.SetActive(false);
        player.RB.AddForce(player.stunVelocity);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if ((isGrounded || isOnPlatform) && player.stunTime <= 0f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        player.stunTime -= Time.deltaTime;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isOnPlatform = player.CheckIfOnPlatform();
        isGrounded = player.CheckIfGrounded();
    }
}
