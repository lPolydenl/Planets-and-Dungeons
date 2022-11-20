using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/ Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;
    public float slopeCheckDistance;
    public PhysicsMaterial2D noFriction;
    public PhysicsMaterial2D fullFriction;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 2f;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("Ledge Climb State")]
    public Vector2 startOffset;
    public Vector2 stopOffset;

    [Header("Dash And Roll State")]
    public float dashTime = 0.3f;
    public float dashVelocity = 20f;
    public float rollTime = 0.7f;
    public float rollVelocity = 17f;
    public float rollAcceleration = 2f;
    public float dashCooldown = 2.5f;
    [HideInInspector] public float dashCdLeft;
    [HideInInspector] public bool dashAvailable;

    [Header("Check Variables")]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlatform;
    public float platformDisableTime = 0.3f;
    public float groundCheckRadius;
    public float wallCheckDistance = 0.3f;
}
