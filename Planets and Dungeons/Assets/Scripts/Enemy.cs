using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool movingRight;
    public bool canMove;
    public Animator anim;
    [HideInInspector] public AddRoom room;
    private Health health;
    public GameObject gun;
    public Animator gunAnim;
    [HideInInspector] public PlayerStats playerStats;
    [SerializeField] private int scoreAward;
    [SerializeField] GameObject healthBar;
    public Transform groundDetector;
    public Transform[] sightPoints;
    public Transform meleeAttackPoint;
    public Transform shotPoint;
    public void Start()
    {
        room = GetComponentInParent<AddRoom>();
        health = GetComponent<Health>();
        Physics2D.IgnoreLayerCollision(9, 9);
    }

    private void Update()
    {
        if (health.health <= 0)
        {
            if (!TryGetComponent(out Kamikaze kamikaze))
            {
                OnDestroyGameObject();
            }
        }
        if(!TryGetComponent(out FlyingEnemy flying))
        {
            if (movingRight)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                if(healthBar != null)
                {
                    healthBar.transform.localRotation = Quaternion.Euler(0, 180, 0);
                }
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                if (healthBar != null)
                {
                    healthBar.transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }

    }
    public void OnDestroyGameObject()
    {
        room.enemies.Remove(gameObject);
        if(TryGetComponent(out Loot loot))
        {
            loot.Drop();
        }
        if(playerStats != null)
        {
            playerStats.scoreAmount += scoreAward;
        }
        Destroy(gameObject);
    }
    public void EnableMove()
    {
        canMove = true;
        anim.SetBool("IsMoving", true);
        if(TryGetComponent(out EnemyShot shot))
        {
            if(gun != null)
            {
                gunAnim.SetBool("IsShooting", false);
            }
            else
            {
                anim.SetBool("IsShooting", false);
            }
        }
        if(TryGetComponent(out Spawn spawn))
        {
            anim.SetBool("IsSpawning", false);
        }
    }


}
