using UnityEngine;
using System.Collections;
public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }
    public bool canMove;
    public bool isGrounded;
    public bool isOnPlatform;
    public int FacingDirection;
    public SpriteRenderer[] armSprites;
    public SpriteRenderer[] allSprites;

    private Vector3 mousePosition;
    [SerializeField] private Camera cam;

    [HideInInspector] public float stunTime;
    [HideInInspector] public Vector2 stunVelocity;

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerSitState SitState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerRollState RollState { get; private set; }
    public PlayerStunnedState StunnedState { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public CapsuleCollider2D CC { get; private set; }
    public GameObject arms;
    public GameObject head;
    public GameObject guns;

    public Vector2 CurrentVelocity { get; private set; }


    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;

    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }


    [SerializeField]
    private PlayerData playerData;

    private Vector2 workspace;

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "climbLedge");
        SitState = new PlayerSitState(this, StateMachine, playerData, "sit");
        RollState = new PlayerRollState(this, StateMachine, playerData, "roll");
        DashState = new PlayerDashState(this, StateMachine, playerData, "dash");
        StunnedState = new PlayerStunnedState(this, StateMachine, playerData, "stunned");
    }
    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        CC = GetComponent<CapsuleCollider2D>();

        cam = FindObjectOfType<Camera>();

        StateMachine.Initialize(IdleState);
    }
    private void Update()
    {
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void SetVelocityZero()
    {
        RB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsWall);
        float xDist = xHit.distance;
        workspace.Set((xDist + 0.015f) * FacingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workspace), Vector2.down, ledgeCheck.position.y - wallCheck.position.y + 0.015f, playerData.whatIsWall);
        float yDist = yHit.distance;

        workspace.Set(wallCheck.position.x + (xDist * FacingDirection), ledgeCheck.position.y - yDist);
        return workspace;
    }
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public void Flip()
    {
        mousePosition = cam.WorldToScreenPoint(transform.position);
        if(Input.mousePosition.x < mousePosition.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            FacingDirection = -1;
        }
        if (Input.mousePosition.x > mousePosition.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            FacingDirection = 1;
        }
    }
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(transform.position - new Vector3(0.0f, CC.size.y / 2), playerData.groundCheckRadius, playerData.whatIsGround);
    }
    public bool CheckIfOnPlatform()
    {
        return Physics2D.OverlapCircle(transform.position - new Vector3(0.0f, CC.size.y / 2), playerData.groundCheckRadius, playerData.whatIsPlatform);
    }

    public bool CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsWall);
    }
    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsWall);
    }
    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, playerData.wallCheckDistance, playerData.whatIsWall);
    }
}
