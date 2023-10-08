using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStateMachine stateMachine;
    public D_Enemy enemyData;
    [HideInInspector] public int facingDirection = 1;
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }

    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private RectTransform healthBar;


    protected Health health;
    

    [HideInInspector] public AddRoom room;
    [HideInInspector] public PlayerStats playerStats;
    [SerializeField] private int scoreAward;

    [SerializeField] private GameObject deathEffect;
    [SerializeField] private AudioSource deathSoundEffect;

    [HideInInspector] public float stunTime;

    private Vector2 workspace;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();
        stateMachine = new EnemyStateMachine();
    }

    public virtual void Update()
    {
        if(health.health <= 0)
        {
            OnDestroyGameObject();
        }
        if (stunTime > 0)
        {
            stunTime -= Time.deltaTime;
            return;
        }
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        if (stunTime > 0)
        {
            SetVelocityX(0);
            SetVelocityY(0);
            return;
        }
        stateMachine.currentState.PhysicsUpdate();
    }
    public virtual void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public virtual void SetVelocityX(float velocity)
    {
        workspace.Set(velocity * facingDirection, rb.velocity.y);
        rb.velocity = workspace;
    }
    public virtual void SetVelocityY(float velocity)
    {
        workspace.Set(rb.velocity.x, velocity);
        rb.velocity = workspace;
    }
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, transform.right, enemyData.wallCheckDistance, enemyData.whatIsGround);
    }
    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, enemyData.ledgeCheckDistance, enemyData.whatIsGround);
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
        if(healthBar)
        {
            workspace.Set(facingDirection * transform.localScale.x, 1);
            healthBar.localScale = workspace;
        }
    }

    public void OnDestroyGameObject()
    {
        if (room)
        {
            room.enemies.Remove(gameObject);
            Debug.Log("FUCK NIGGERS");
        }
        if (TryGetComponent(out Loot loot))
        {
            loot.Drop(transform);
            Debug.Log("KILL NIGGERS");
        }
        if (playerStats != null)
        {
            playerStats.scoreAmount += scoreAward;
            Debug.Log("HATE NIGGERS");
        }
        if(deathEffect)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        if(deathSoundEffect)
        {
            AudioSource soundEffect = Instantiate(deathSoundEffect, transform.position, Quaternion.identity);
            soundEffect.Play();
        }
        Debug.Log("KILL STUPID BLACK MONKEYS STUDIOS");
        Destroy(gameObject);
    }
}
