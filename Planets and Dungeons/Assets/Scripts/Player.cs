using UnityEngine;
using System.Collections;
public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Movement")]
    public float speed;
    private float moveInput;
    private Vector3 pos;
    private Camera main;
    private bool isFacingRight;

    [Header("Jump")]
    public float jumpForce;
    public Transform feetPos;
    public float checkRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlatforms;
    public bool isGrounded = false;
    public bool isOnPlatform;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    [Header("Wall slide")]
    private bool isTouchingFront;
    public Transform frontCheck;
    private bool wallSliding;
    public float wallSlidingSpeed;
    public LayerMask whatIsWall;

    [Header("Wall jump")]
    private bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    [Header("Slopes")]
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

    [Header("Climbing")]
    [SerializeField] private Transform ledgeCheck;
    private bool isTouchingLedge;
    private bool canClimbLedge;
    private bool ledgeDetected;
    private Vector2 ledgePosBot;
    private Vector2 ledgePos1;
    private Vector2 ledgePos2;
    [SerializeField] private float ledgeClimbXOffset1 = 0f;
    [SerializeField] private float ledgeClimbYOffset1 = 0f;
    [SerializeField] private float ledgeClimbXOffset2 = 0f;
    [SerializeField] private float ledgeClimbYOffset2 = 0f;


    [Header("Other")]
    public GameObject[] guns;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        main = FindObjectOfType<Camera>();
        cc = GetComponent<CapsuleCollider2D>();
        colliderSize = cc.size;
    }
    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        if (moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }
        if (Time.timeScale > 0f)
        {
            pos = main.WorldToScreenPoint(transform.position);
            Flip();
            if (isGrounded || isOnPlatform)
            {
                anim.SetBool("isGrounded", true);
            }
            else
            {
                anim.SetBool("isGrounded", false);
            }
            if ((isGrounded || isOnPlatform) && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            if (Input.GetKey(KeyCode.Space) && isJumping)
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    jumpTimeCounter -= Time.deltaTime;
                }
                else isJumping = false;
                anim.SetBool("isJumping", false);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumping = false;
                anim.SetBool("isJumping", false);
            }
            isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsWall);
            if (isTouchingFront && (!isGrounded && !isOnPlatform) && moveInput != 0 && !canClimbLedge)
            {
                wallSliding = true;
                anim.SetBool("isSliding", true);
            }
            else
            {
                wallSliding = false;
                anim.SetBool("isSliding", false);
            }
            if (wallSliding)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
                foreach (GameObject gun in guns)
                {
                    gun.SetActive(false);
                }
            }
            else
            {
                foreach (GameObject gun in guns)
                {
                    gun.SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) && wallSliding && !isGrounded && !isOnPlatform)
            {
                wallJumping = true;
                anim.SetTrigger("Jump");
                Invoke("SetWallJumpingToFalse", wallJumpTime);
            }
            if (wallJumping)
            {
                rb.velocity = new Vector2(xWallForce * -moveInput, yWallForce);

            }
            //CheckLedgeClimb();
        }
    }
    public void Flip()
    {
        if (Input.mousePosition.x < pos.x)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            isFacingRight = false;
        }
        if (Input.mousePosition.x > pos.x)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            isFacingRight = true;
}
    }
    private void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }
    private void Jump()
    {
        isJumping = true;
        anim.SetTrigger("Jump");
        anim.SetBool("isJumping", true);
        jumpTimeCounter = jumpTime;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    private void CheckLedgeClimb()
    {
        if(ledgeDetected && !canClimbLedge)
        {
            canClimbLedge = true;

            if (isFacingRight)
            {
                ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + checkRadius) - ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + checkRadius) + ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }
            else
            {
                ledgePos1 = new Vector2(Mathf.Ceil(ledgePosBot.x - checkRadius) + ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Ceil(ledgePosBot.x - checkRadius) - ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }
        }

        if(canClimbLedge)
        {
            transform.position = ledgePos1;
        }
    }
    private void FinishLedgeClimb()
    {
        canClimbLedge = false;
        transform.position = ledgePos2;
        ledgeDetected = false;
    }
    private void CheckGround()
    {
        if (!isJumping)
        {
            isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
            if(rb.velocity.y <= 0.01)
            {
                isOnPlatform = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsPlatforms);
            }
        }
        else
        {
            isGrounded = false;
            isOnPlatform = false;
        }

        //isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, checkRadius, whatIsWall);

        //if(wallSliding && !isTouchingLedge && !ledgeDetected)
        //{
            //ledgeDetected = true;
        //}
    }
    private void FixedUpdate()
    {
        CheckGround();
        SlopeCheck();
        if (!wallJumping)
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
        if (slopeHitFront && (isGrounded || isOnPlatform))
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if (slopeHitBack && (isGrounded || isOnPlatform))
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

            if(slopeDownAngle != slopeDownAngleOld)
            {
                //isOnSlope = true;
            }

            slopeDownAngleOld = slopeDownAngle;

            Debug.DrawRay(hit.point, hit.normal, Color.green);
            Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
        }

        if(isOnSlope && moveInput == 0)
        {
            rb.sharedMaterial = fullFriction;
        }
        else
        {
            rb.sharedMaterial = noFriction;
        }
    }
    private IEnumerator SetGrounded()
    {
        yield return new WaitForSeconds(0.2f);
        isGrounded = true;
    }
        
    private void ApplyMove()
    {
        //rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if(isGrounded && !isOnSlope)
        {
            rb.velocity = new Vector2(speed * moveInput, 0f);
        }
        else if (isGrounded && isOnSlope)
        {
            rb.velocity = new Vector2(speed * slopeNormalPerp.x * -moveInput, speed * slopeNormalPerp.y * -moveInput);
        }
        else if (!isGrounded)
        {
            rb.velocity = new Vector2(speed * moveInput, rb.velocity.y);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(feetPos.position, checkRadius);
    }

}
