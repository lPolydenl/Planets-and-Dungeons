using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CapsuleCollider2D cc;
    private Vector2 colliderSize;
    [SerializeField] private float slopeCheckDistance;
    private float slopeDownAngle;
    private float slopeDownAngleOld;
    private float slopeSideAngle;
    private Vector2 slopeNormalPerp;
    [SerializeField] private bool isOnSlope;
    [SerializeField] private PhysicsMaterial2D noFriction;
    [SerializeField] private PhysicsMaterial2D fullFriction;
    [SerializeField] private LayerMask whatIsSlopes;
    [SerializeField] private PlayerManager pm;
    private float moveInput;
    void Start()
    {

    }
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        SlopeCheck();
        if (pm.canMove)
        {
            ApplyMove();
        }
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = cc.bounds.center - new Vector3(0, colliderSize.y / 2);
        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }
    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, whatIsSlopes);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, whatIsSlopes);
        Debug.DrawRay(checkPos, transform.right, Color.magenta);
        Debug.DrawRay(checkPos, -transform.right, Color.magenta);
        if (slopeHitFront && (pm.isGrounded || pm.isOnPlatform))
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if (slopeHitBack && (pm.isGrounded || pm.isOnPlatform))
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0f;
            isOnSlope = false;
        }
    }
    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, whatIsSlopes);
        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle != slopeDownAngleOld)
            {
                //isOnSlope = true;
            }

            slopeDownAngleOld = slopeDownAngle;

            Debug.DrawRay(hit.point, hit.normal, Color.green);
            Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
        }

        if (isOnSlope && moveInput == 0)
        {
            pm.rb.sharedMaterial = fullFriction;
        }
        else
        {
            pm.rb.sharedMaterial = noFriction;
        }
    }
    private void ApplyMove()
    {
        if (pm.isGrounded && !isOnSlope)
        {
            pm.rb.velocity = new Vector2(pm.speed * moveInput, 0f);
        }
        else if (pm.isGrounded && isOnSlope)
        {
            pm.rb.velocity = new Vector2(pm.speed * slopeNormalPerp.x * -moveInput, pm.speed * slopeNormalPerp.y * -moveInput);
        }
        else if (!pm.isGrounded)
        {
            pm.rb.velocity = new Vector2(pm.speed * moveInput, pm.rb.velocity.y);
        }
    }
}
