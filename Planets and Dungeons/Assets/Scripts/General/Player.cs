using UnityEngine;
using System.Collections;
public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private bool canShot;
    private bool canFlip = true;

    [Header("Movement")]
    public float speed;
    private float moveInput;
    private Vector3 pos;
    private Camera main;
    private bool isFacingRight;
    [HideInInspector] public bool canMove = true;

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
    private float timeBtwCheckGround;

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

    private bool isSitting;
    private bool isRolling;
    [SerializeField] private float startTimeBtwRolls;
    private float timeBtwRolls;
    [SerializeField] private float startRollingTime;
    private float rollingTime;
    [SerializeField] private float rollingForce;

    private bool isDashing;
    [SerializeField] private float startDashTime;
    private float dashTime;
    [SerializeField] private float dashForce;

    private bool isVelocityLocked;
    private float minVelX;
    private float minVelY;
    private float maxVelX;
    private float maxVelY;

    [SerializeField] private float slowingMultiplier;
    private int isSlowing;

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
    public SpriteRenderer[] armSprites;
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
        if(!isVelocityLocked)
        {
            moveInput = Input.GetAxis("Horizontal");
        }
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
            if(canFlip)
            {
                Flip();
            }
            if (isGrounded || isOnPlatform)
            {
                anim.SetBool("isGrounded", true);
            }
            else
            {
                anim.SetBool("isGrounded", false);
                timeBtwCheckGround -= Time.deltaTime;
            }
            if ((isGrounded || isOnPlatform) && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
                timeBtwCheckGround = 0.15f;
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
                canShot = false;
            }
            else
            {
                canShot = true;
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
            if(isVelocityLocked)
            {
                LockVelocity();
            }
            CheckLedgeClimb();
            CheckGun();
            CheckSit();
            CheckRoll();
            CheckDash();
            Slow();
        }
    }
    private void CheckGun()
    {
        if(!canShot)
        {
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
    private void CheckSit()
    {
        if ((isGrounded || isOnPlatform) && Input.GetKey(KeyCode.LeftShift) && isRolling == false)
        {
            if (Input.GetAxis("Horizontal") == 0 || timeBtwRolls <= 0)
            {
                anim.SetBool("isSitting", true);
                canMove = false;
                rb.velocity = new Vector2(0, rb.velocity.y);
                isSitting = true;
            }
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("isSitting", false);
            canMove = true;
            isSitting = false;
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
        if(ledgeDetected && !canClimbLedge && !isRolling && !isDashing)
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
            canMove = false;
            anim.SetBool("CanClimbLedge", true);
            canShot = false;
            canFlip = false;
        }
    }
    private void FinishLedgeClimb()
    {
        canClimbLedge = false;
        transform.position = ledgePos2;
        ledgeDetected = false;
        canMove = true;
        canShot = true;
        canFlip = true;
        anim.SetBool("CanClimbLedge", false);
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

    }
    private void CheckObstacles()
    {

        isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, checkRadius, whatIsWall);
        Debug.DrawRay(ledgeCheck.position, transform.right, Color.cyan);
        isTouchingFront = Physics2D.Raycast(frontCheck.position, transform.right, checkRadius, whatIsWall);
        Debug.DrawRay(frontCheck.position, transform.right, Color.yellow);

        if (isTouchingFront && !isTouchingLedge && !ledgeDetected && Input.GetAxis("Horizontal") != 0 && Input.GetKey(KeyCode.Space))
        {
            ledgeDetected = true;
            ledgePosBot = frontCheck.position;
        }
    }
    private void CheckRoll()
    {
        if(timeBtwRolls <= 0)
        {
            if(isSitting)
            {
                if(isFacingRight && moveInput > 0)
                {
                    StartRolling(1);
                }
                else if (!isFacingRight && moveInput < 0)
                {
                    StartRolling(-1);
                }
            }
        }
        else
        {
            timeBtwRolls -= Time.deltaTime;
        }
    }
    private void StartRolling(int direction)
    {
        anim.SetBool("isRolling", true);
        isRolling = true;
        timeBtwRolls = startTimeBtwRolls;
        rollingTime = startRollingTime;
        canFlip = false;
        canMove = false;
        Debug.Log("Yebashit normalno");
        StartCoroutine(Roll(direction));
    }
    private IEnumerator Roll(int direction)
    {
        rb.velocity = new Vector2(speed * rollingForce * direction, 0f);
        if(direction > 0)
        {
            minVelX = 0f;
            maxVelX = speed * rollingForce * direction;
        }
        else
        {
            minVelX = speed * rollingForce * direction;
            maxVelX = 0f;
        }
        minVelY = -Mathf.Infinity;
        maxVelY = Mathf.Infinity;
        isVelocityLocked = true;
        isSlowing = direction;
        yield return new WaitForSeconds(rollingTime);
        isVelocityLocked = false;
        isSlowing = 0;
        canFlip = true;
        if (!isSitting)
        {
            canMove = true;
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        isRolling = false;
        anim.SetBool("isRolling", false);
    }
    private void Slow()
    {
        rb.velocity = new Vector2(rb.velocity.x - Time.deltaTime * slowingMultiplier * isSlowing, rb.velocity.y);
    }
    private void CheckDash()
    {
        if (timeBtwRolls <= 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) && !isGrounded && !isOnPlatform && !isDashing)
            {
                if (isFacingRight && moveInput > 0)
                {
                    StartDash(1);
                }
                else if (!isFacingRight && moveInput < 0)
                {
                    StartDash(-1);
                }
            }
        }
    }
    private void StartDash(int direction)
    {
        anim.SetBool("isRolling", true);
        isDashing = true;
        timeBtwRolls = startTimeBtwRolls;
        dashTime = startDashTime;
        canFlip = false;
        canMove = false;
        Debug.Log("Zaebis");
        StartCoroutine(Dash(direction));
    }
    private IEnumerator Dash(int direction)
    {
        //rb.AddForce(new Vector2(speed * dashForce * direction, 0f));
        rb.velocity = new Vector2(speed * dashForce * direction, 0f);
        minVelX = speed * dashForce * direction;
        maxVelX = speed * dashForce * direction;
        minVelY = 0f;
        maxVelY = 0f;
        isVelocityLocked = true;
        moveInput = direction;
        yield return new WaitForSeconds(dashTime);
        isVelocityLocked = false;
        canFlip = true;
        if (!isSitting)
        {
            canMove = true;
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        isDashing = false;
        anim.SetBool("isRolling", false);
    }
    private void LockVelocity()
    {
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, minVelX, maxVelX),
            Mathf.Clamp(rb.velocity.y, minVelY, maxVelY));
    }
    private void FixedUpdate()
    {
        if(timeBtwCheckGround <= 0)
        {
            CheckGround();
        }
        else
        {
            isGrounded = false;
            isOnPlatform = false;
        }
        CheckObstacles();
        SlopeCheck();
        if (!wallJumping && canMove && !isVelocityLocked)
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
    private void ApplyMove()
    {
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
}
